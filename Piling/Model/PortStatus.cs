using System.ComponentModel.DataAnnotations;

namespace Piling.Model
{
    /// <summary>
    /// ort status enum information model
    /// </summary>
    public enum PortStatus
    {
        /// <summary>
        /// Not Scanned
        /// </summary>
        [Display(Name = "Not Scanned")]
        NotScanned,

        /// <summary>
        /// Opened
        /// </summary>
        [Display(Name = "Opened")]
        Open,

        /// <summary>
        /// Closed
        /// </summary>
        [Display(Name = "Closed")]
        Closed
    }
}
