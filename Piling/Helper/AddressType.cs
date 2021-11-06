using System.ComponentModel.DataAnnotations;

namespace Piling.Helper
{
    /// <summary>
    /// Type of target address
    /// </summary>
    public enum AddressType
    {
        /// <summary>
        /// Address address
        /// </summary>
        [Display(Name = "Address Address")]
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
