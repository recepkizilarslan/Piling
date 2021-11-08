using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Piling.Helper;
using Piling.Model;
using Console = Colorful.Console;

namespace Piling
{
    class Program
    {
        /// <summary>
        /// Use parameter validator for handle user params
        /// </summary>
        private static readonly ParameterHandler _parameterHandler = new();

        /// <summary>
        /// Use scan manager 
        /// </summary>
        private static ScanManager ScanManager;

        /// <summary>
        /// Stop run bool variable using for check stop status
        /// </summary>
        private static bool _stopRun = false;
       
        /// <summary>
        /// Stop run bool variable using for check stop status
        /// </summary>
        private static bool _paused = false;

        static void Main(string[] args)
        {
            WriteBanner();

            var options = _parameterHandler.Handle(args);
            

            ScanManager = new ScanManager(options);

            var mainOperation = new Thread(() =>
             {

                 ScanManager.Start();
             })
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true
            };

            mainOperation.Start();

            ScanManager.ProcessInformationAction += WriteStatus;

            var stateAction = new Thread(WaitStateAction)
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true
            };

            stateAction.Start();
            new Task(WaitStateAction).Start();
        }

        /// <summary>
        /// Write message to console
        /// </summary>
        /// <param name="information"></param>
        static void WriteStatus(Request information)
        {
            var color = information.Status == "Open" ? Color.Green : Color.Gray;
            Console.WriteLine($"{information.No} IP : {information.Ip} Port : {information.Port} - {information.Status}", color);
        }

        
        /// <summary>
        /// State action manager
        /// </summary>
        private static void WaitStateAction()
        {
            while (!_stopRun)
            {
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        new Thread(
                            () => { ScanManager.Stop(); }).Start();
                        _stopRun = true;
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.Spacebar:
                        if (!_paused)
                        {
                            new Thread(
                                () => { ScanManager.Pause(); }).Start();
                            _paused = true;
                            Console.WriteLine("------------------------------Paused------------------------------------------");
                            return;
                        }
                        new Thread(
                            () => { ScanManager.Resume(); }).Start();
                        _paused = false;
                        Console.WriteLine("------------------------------Resume------------------------------------------");
                        break;
                }
            }
        }

        /// <summary>
        /// Write welcome banner on the console.
        /// </summary>
        private static void WriteBanner()
        {
            Console.Title = "Piling Port Scanner";
            var da = 244;
            var v = 212;
            var id = 255;
            for (var i = 0; i < 1; i++)
            {
                Console.WriteAscii("Piling Port Scanner", Color.FromArgb(da, v, id));
                da -= 18;
                v -= 36;
            }
        }
    }
}
