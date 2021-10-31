using System;
using System.Net;

namespace Piling.Model
{
    /// <summary>
    /// This class is main model of the tcp request.
    /// </summary>
    public class TcpRequest
    {
        /// <summary>
        /// Get or set IP Address 
        /// </summary>
        public IPAddress Ip { get; set; }

        /// <summary>
        /// Get or set port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Get or set status
        /// </summary>
        public PortStatus Status { get; set; }

        /// <summary>
        /// Get or set request time
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Request elapsed time
        /// </summary>
        public double ElapsedTime { get; set; }
    }
}
