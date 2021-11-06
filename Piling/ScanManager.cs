using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using NetTools;
using Piling.Core;
using Piling.Implementation;
using Piling.Model;
using Piling.Structure;
using TcpRequest = Piling.Model.TcpRequest;

namespace Piling
{
    /// <summary>
    /// This class manages the main components
    /// </summary>
    public class ScanManager
    {
        /// <summary>
        /// Results count
        /// </summary>
        static volatile int _resultCount = 1;

        /// <summary>
        /// Process information action. This property binds to ui method for output
        /// </summary>
        public Action<RequestDto> ProcessInformationAction;

        /// <summary>
        ///  use state manager class
        /// </summary>
        private readonly StateManager _stateManager = new();

        /// <summary>
        /// Use tcp request's custom structure. for waiting requests.
        /// </summary>
        private readonly TcpRequestConcurrentQueue _waitingRequests = new();

        /// <summary>
        /// Use tcp request's custom structure. for finishing requests.
        /// </summary>
        private readonly TcpRequestConcurrentQueue _finishedRequests = new();

        /// <summary>
        /// Use tcp request's custom structure. for finishing threads.
        /// </summary>
        private readonly ThreadConcurrentQueue _threadConcurrentQueue = new();

        /// <summary>
        /// Output writer
        /// </summary>
        private readonly IWriter _writer;

        /// <summary>
        /// Scan options
        /// </summary>
        private readonly ScanOptions _options;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="options">ScanOptions options</param>
        public ScanManager(ScanOptions options)
        {
            _waitingRequests.NewMemberAction += PrepareRequest;
            _threadConcurrentQueue.NewMemberAction += StartCheck;
            _finishedRequests.NewMemberAction += WriteResult;
            _options = options;

            _writer = options.OutputFormat switch
            {
                OutputFormat.Txt => new TxtWriter(),
                OutputFormat.Csv => new CsvWriter(),
                OutputFormat.Json => throw new ArgumentOutOfRangeException(),
                OutputFormat.Undefined => throw new ArgumentOutOfRangeException(),
                _ => throw new ArgumentOutOfRangeException()
            };
            _writer.Create(options.OutputPath);
        }

        /// <summary>
        /// This function is public start.
        /// </summary>
        public void Start()
        {
            _stateManager.Start(10);
            new Thread(
                () =>
                {
                    foreach (var ip in IPAddressRange.Parse(_options.Address))
                    {
                        for (var j = _options.PortStartRange; j <= _options.PortFinishRange; j++)
                        {
                            _stateManager.ListenState();
                            _waitingRequests?.Enqueue(new TcpRequest { Ip = ip, Port = j, Status = PortStatus.NotScanned });
                            Interlocked.Increment(ref _stateManager.ParsedIpAddress);
                        }
                    }
                }).Start();
        }

        /// <summary>
        /// Handle request result 
        /// </summary>
        public void WriteResult()
        {
            _stateManager.ListenState();

            var request = _finishedRequests?.Dequeue();

            if (request is null) return;
            Interlocked.Increment(ref _resultCount);
            Interlocked.Increment(ref _stateManager.SuccessResults);

            var message = new RequestDto
            {
                No = _resultCount,
                Ip = request.Ip.ToString(),
                Port = request.Port,
                Status = request.Status.ToString(),
                ElapsedTime = request.ElapsedTime,
                Time = request.DateTime.ToString(CultureInfo.InvariantCulture)
            };
            _writer?.Write(message);
            ProcessInformationAction?.Invoke(message);
        }


        /// <summary>
        /// Prepare requests.
        /// </summary>
        private void PrepareRequest()
        {
            _stateManager.ListenState();
            var request = _waitingRequests.Dequeue();

            var requestThread = new Thread(
                () =>
                {
                    Check(request);
                })
            {
                Name = $"{request.Ip}:{request.Port}-thread",
                Priority = ThreadPriority.AboveNormal
            };

            _threadConcurrentQueue?.Enqueue(requestThread);
            Interlocked.Increment(ref _stateManager.CreatedRequest);
        }

        /// <summary>
        /// Pause state
        /// </summary>
        public void Pause() => _stateManager.MainStateChangeAction.Invoke(State.Pause);

        /// <summary>
        /// Stop state
        /// </summary>
        public void Stop() => _stateManager.MainStateChangeAction.Invoke(State.Stop);

        /// <summary>
        /// Resume state
        /// </summary>
        public void Resume() => _stateManager.MainStateChangeAction.Invoke(State.Resume);

        /// <summary>
        /// Start checks
        /// </summary>
        private void StartCheck()
        {
            _stateManager.ListenState();
            _threadConcurrentQueue?.Dequeue().Start();
        }

        /// <summary>
        /// This function does port check with function initialization 
        /// </summary>
        /// <param name="request"></param>
        private void Check(TcpRequest request)
        {
            _stateManager.ListenState();
            try
            {
                new Thread(
                    () =>
                    {
                        using var tcpRequest = new TcpRequester(request);
                        var response = tcpRequest.Send();
                        Interlocked.Increment(ref _stateManager.SentRequests);
                        _finishedRequests?.Enqueue(response);

                    }).Start();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, Color.DarkRed);
            }
        }
    }
}


