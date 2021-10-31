using System;
using System.Drawing;
using System.Linq;
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
            
            if (options is null)
            {
                Console.ReadLine();
            }

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
        /// <param name="informationDto"></param>
        static void WriteStatus(RequestDto informationDto)
        {
            var color = informationDto.Status == "Open" ? Color.Green : Color.Red;
            Console.WriteLine($"{informationDto.No} IP : {informationDto.Ip} Port : {informationDto.Port} - {informationDto.Status}", color);
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
