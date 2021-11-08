using System;
using Piling.Helper;

namespace Piling.Model
{
    /// <summary>
    /// Scan configuration model
    /// </summary>
    public class ScanOptions
    {
        /// <summary>
        /// Default const
        /// </summary>
        public ScanOptions()
        {
                
        }

        /// <summary>
        /// Constructor with arguments
        /// </summary>
        /// <param name="address"></param>
        /// <param name="portStartRange"></param>
        /// <param name="portFinishRange"></param>
        /// <param name="outputPath"></param>
        /// <param name="outputFormat"></param>
        /// <param name="threadCount"></param>
        /// <param name="reportOptions"></param>
        public ScanOptions(string address,int portStartRange,int portFinishRange, string outputPath, OutputFormat outputFormat, int threadCount=10, ReportOptions reportOptions = ReportOptions.All)
        {
            Address = address;
            PortStartRange = portStartRange;
            PortFinishRange = portFinishRange;
            ThreadCount = threadCount;
            OutputPath = outputPath;
            OutputFormat = outputFormat;
        }

        /// <summary>
        /// Get or set address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Get or set port start range 
        /// </summary>
        public int PortStartRange  { get; set; }

        /// <summary>
        /// Get or set port finish range 
        /// </summary>
        public int PortFinishRange  { get; set; }

        /// <summary>
        /// Get or set output path
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        /// Get or set output format
        /// </summary>
        public OutputFormat OutputFormat { get; set; }

        /// <summary>
        /// Get or set thread count
        /// </summary>
        public int ThreadCount { get; set; }

        /// <summary>
        /// Get or set report options
        /// </summary>
        public ReportOptions ReportOptions { get; set; }

    }
}
