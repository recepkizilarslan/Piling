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
        /// Validate ip address.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns><c>true</c> or <c>false</c></returns>
        public bool IsValidIP(string ipAddress)
        {
            return Regex.IsMatch(ipAddress, RegexIp);
        }


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
                Console.WriteLine(e);
                return OutputFormat.Undefined;
            }
        }
    }
}
