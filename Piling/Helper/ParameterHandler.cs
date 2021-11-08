using CommandLine;
using Piling.Model;
using System;
using System.Linq;
using System.Net;

namespace Piling.Helper
{
    /// <summary>
    /// Handle user parameters class
    /// </summary>
    public class ParameterHandler
    {
        /// <summary>
        /// Use validator class
        /// </summary>
        private readonly ParameterValidator _validator = new ParameterValidator();

        /// <summary>
        /// user parameter handler
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public ScanOptions Handle(string[] args)
        {
            var scanOptions = new ScanOptions();
            var result = Parser.Default.ParseArguments<Parameters>(args)
                .WithParsed(options =>
                {
                    var addressType = _validator.IsValidAddress(options.Address);

                    switch (addressType)
                    {
                        case AddressType.Ip:
                            scanOptions.Address = options.Address;
                            break;
                        case AddressType.Domain:
                            scanOptions.Address = ResolveIpAddressOfDomain(options.Address);
                            if (scanOptions.Address is null)
                            {
                                throw new PilingException("Domain Address cannot resolved", true);
                            }
                            break;
                        case AddressType.Undefined:
                            throw new PilingException("Unsupported address type", true);
                        default:
                            throw new PilingException("Unsupported address type", true);
                    }

                    if (options.PortStartRange < 12)
                    {
                        throw new PilingException("Port start range is invalid Start min 12", true);
                    }

                    scanOptions.PortStartRange = options.PortStartRange;

                    if (options.PortFinishRange < 13 || options.PortFinishRange > 65535)
                    {
                        throw new PilingException("Port start range is invalid. The range must be 13-65535", true);
                    }

                    scanOptions.PortFinishRange = options.PortFinishRange;

                    var outputType = _validator.IsValidType(options.Path);
                    if (outputType == OutputFormat.Undefined)
                    {
                        throw new PilingException("Unsupported output type", true);
                    }

                    switch (options.ReportOptions)
                    {
                        case null:
                            scanOptions.ReportOptions = ReportOptions.All;
                            break;
                        case "opened":
                            scanOptions.ReportOptions = ReportOptions.Opened;
                            break;
                        case "closed":
                            scanOptions.ReportOptions = ReportOptions.Closed;
                            break;
                        default:
                            throw new PilingException("Unsupported report options", true);
                    }



                    scanOptions.OutputFormat = outputType;
                    scanOptions.OutputPath = options.Path;

                });

            return result.Tag == ParserResultType.NotParsed ? null : scanOptions;
        }

        /// <summary>
        /// Resolve ip address of the domain
        /// </summary>
        /// <param name="domainAddress"></param>
        /// <returns></returns>
        private string ResolveIpAddressOfDomain(string domainAddress)
        {
            try
            {
                return Dns.GetHostAddresses(domainAddress).FirstOrDefault()?.ToString();
            }
            catch (Exception e)
            {
                //noop
                return null;
            }
        }
    }
}
