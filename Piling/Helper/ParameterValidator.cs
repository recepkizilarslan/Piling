using System;
using System.IO;
using System.Text.RegularExpressions;
using Piling.Model;

namespace Piling.Helper
{
    /// <summary>
    /// This class is ip(s) validator.
    /// </summary>
    public class ParameterValidator
    {
        /// <summary>
        /// For one ip validate
        /// </summary>
        private const string RegexIp = @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

        /// <summary>
        /// Check address type is valid
        /// </summary>
        /// <param name="address"></param>
        /// <returns>AddressType</returns>
        public AddressType IsValidAddress(string address)
        {
            try
            {
                if (IsValidIp(address))
                {
                    return AddressType.Ip;
                }

                return IsValidDomain(address) ? AddressType.Domain : AddressType.Undefined;
            }
            catch (Exception e)
            {
               //noop
               return AddressType.Undefined;
            }
        }


        /// <summary>
        /// Validate ip address.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns><c>true</c> or <c>false</c></returns>
        private bool IsValidIp(string ipAddress)
        {
            return Regex.IsMatch(ipAddress, RegexIp);
        }

        /// <summary>
        /// Validate domain
        /// </summary>
        /// <param name="domainAddress"></param>
        /// <returns></returns>
        private bool IsValidDomain(string domainAddress)
        {
            try
            {
                return Uri.CheckHostName(domainAddress) != UriHostNameType.Unknown;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Check output format
        /// </summary>
        /// <param name="path"></param>
        /// <returns>OutputFormat</returns>
        public OutputFormat IsValidType(string path)
        {
            try
            {
                var extension = (OutputFormat)Enum.Parse(
                    typeof(OutputFormat),
                    Path.GetExtension(path).TrimStart('.'),
                    true);
                return extension;
            }
            catch (Exception e)
            {
                return OutputFormat.Undefined;
            }
        }
    }
}
