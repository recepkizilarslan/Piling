using System;
using System.Drawing;
using Console = Colorful.Console;

namespace Piling
{
    /// <summary>
    /// Simple custom exception class
    /// </summary>
    public class PilingException:Exception
    {
        /// <summary>
        /// Default constructor with param
        /// </summary>
        /// <param name="message"></param>
        /// <param name="shutdown"></param>
        public PilingException(string message,bool shutdown)
        {
              Console.WriteLine(message, Color.Red);

              if (!shutdown) return;

              Console.WriteLine("Please restart the application");
              Console.ReadKey();
              Environment.Exit(0);
        }
    }
}
