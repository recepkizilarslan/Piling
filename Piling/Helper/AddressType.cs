using System.ComponentModel.DataAnnotations;

namespace Piling.Helper
{
    /// <summary>
    /// Type of target address
    /// </summary>
    public enum AddressType
    {
        /// <summary>
        /// Ip address
        /// </summary>
        [Display(Name = "Ip Address")]
        Ip,

        /// <summary>
        /// Domain
        /// </summary>
        [Display(Name = "Domain")]
        Domain,

        /// <summary>
        /// Not Scanned
        /// </summary>
        [Display(Name = "Undefined")]
        Undefined
    }
}
