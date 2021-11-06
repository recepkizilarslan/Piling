using System;
using System.Linq;
using System.Net;
using Piling.Model;
using Console = Colorful.Console;

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

            var getAddress = ParamValidator(ref args, "scan");

            if (!getAddress.Item1)
            {
                return null;
            }

            var addressType = _validator.IsValidAddress(getAddress.Item2);
            string address;

            switch (addressType)
            {
                case AddressType.Ip:
                    address = getAddress.Item2;
                    break;
                case AddressType.Domain:
                    address = ResolveIpAddressOfDomain(getAddress.Item2);
                    if (address is null)
                    {
                        Console.WriteLine("Domain Address cannot resolved");
                        return null;
                    }
                    break;
                case AddressType.Undefined:
                    Console.WriteLine("Address cannot resolved");
                    return null;
                default:
                    throw new ArgumentOutOfRangeException();
            }



            var getPortStartRange = ParamValidator(ref args, "from");

            if (!getPortStartRange.Item1)
            {
                return null;
            }

            var getPortFinishRange = ParamValidator(ref args, "to");

            if (!getPortFinishRange.Item1)
            {
                return null;
            }

            var getOutput = ParamValidator(ref args, "save");

            if (!getOutput.Item1)
            {
                return null;
            }

            var outputType = _validator.IsValidType(getOutput.Item2);
            if (outputType == OutputFormat.Undefined)
            {
                Colorful.Console.WriteLine("Unsupported output type");
                return null;
            }


            return new ScanOptions(
                address,
                Convert.ToInt32(getPortStartRange.Item2),
                Convert.ToInt32(getPortFinishRange.Item2),
                getOutput.Item2, outputType);
        }

        /// <summary>
        /// Validate user params
        /// </summary>
        /// <param name="args"></param>
        /// <param name="param"></param>
        /// <returns>Tuple<bool,string></returns>
        private (bool, string) ParamValidator(ref string[] args, string param)
        {
            var index = args.ToList().FindIndex(x => x.StartsWith(param));

            if (index is -1)
            {
                Console.WriteLine($"{param} param is required");
                return (false, null);
            }

            var paramIndex = index + 1;

            if (!string.IsNullOrEmpty(args[paramIndex]))
                return (true, args[paramIndex].Replace(param, ""));
            Console.WriteLine($"{param} mustn't be null!");
            return (false, null);
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
