using CommandLine;

namespace Piling.Helper
{
    /// <summary>
    /// User parameters
    /// </summary>
    public class Parameters
    {
        [Option(longName: "scan", Required = true, HelpText = "Scan start command. Ex usage : 'scan to google.com'")]
        public string Address { get; set; }

        [Option(longName: "from", Required = true, HelpText = "Port start range. Ex usage: 'from 12'")]
        public int PortStartRange { get; set; }

        [Option(longName: "to", Required = true, HelpText = "Port finish range. Ex usage: 'to 443'")]
        public int PortFinishRange { get; set; }

        [Option(longName: "save", Required = true, HelpText = "Location for result report. Ex usage: 'to C:\\Desktop\\report.txt'")]
        public string Path { get; set; }

        [Option(longName: "just", Required = false, HelpText = "Define the status to be reported after scanning..  just [Opened|Closed]")]
        public string ReportOptions { get; set; }
    }

}
