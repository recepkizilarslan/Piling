using System;
using System.Diagnostics;
using System.Net.Sockets;
using Piling.Core;
using Piling.Model;

namespace Piling.Implementation
{
    /// <summary>
    /// This class implementation of the tcp tcpRequest
    /// </summary>
    public class TcpRequester : IRequest<TcpRequest>
    {
        /// <summary>
        /// Use stopwatch for calculate sw data.
        /// </summary>
        private readonly Stopwatch _sw;

        /// <summary>
        /// TCP tcpRequest state
        /// </summary>
        private IAsyncResult _state;

        /// <summary>
        /// TcpRequester model
        /// </summary>
        private readonly TcpRequest _tcpRequest;

        /// <summary>
        /// Use tcp client 
        /// </summary>
        private readonly TcpClient _client = new();

        /// <summary>
        /// Default const.
        /// </summary>
        public TcpRequester(TcpRequest tcpRequest)
        {
            _tcpRequest = tcpRequest;
            _sw = new Stopwatch();
        }

        /// <summary>
        /// Send tcp tcpRequest
        /// </summary>
        /// <returns>TcpRequester</returns>
        public TcpRequest Send()
        {
            _sw.Start();
            try
            {
                _state = _client.BeginConnect(_tcpRequest.Ip, _tcpRequest.Port, null, null);
                _client.EndConnect(_state);
                _tcpRequest.Status = PortStatus.Open;
            }
            catch (Exception ex)
            {
                _tcpRequest.Status = PortStatus.Closed;
            }
            _sw.Stop();
            _tcpRequest.ElapsedTime = _sw.ElapsedMilliseconds;
            _tcpRequest.DateTime=DateTime.Now;
            return _tcpRequest;
        }

        /// <summary>
        /// Dispose tcpRequest object.
        /// </summary>
        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
