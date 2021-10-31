using System;

namespace Piling.Model
{
    /// <summary>
    /// Request information data transfer object
    /// </summary>
    public class RequestDto
    {
        /// <summary>
        /// Get or set request no
        /// </summary>
        public int No { get; set; }

        /// <summary>
        /// Get or set Ip address
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// Get or set port number
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Get or set port status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Get or set elapsed time
        /// </summary>
        public double ElapsedTime { get; set; }

        /// <summary>
        /// Get or set time
        /// </summary>
        public string Time { get; set; }
    }
}
