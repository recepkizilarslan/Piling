using System.ComponentModel.DataAnnotations;

namespace Piling.Helper
{
    /// <summary>
    /// port options for reporting
    /// </summary>
    public enum ReportOptions
    {
        /// <summary>
        /// Not Scanned
        /// </summary>
        [Display(Name = "All")]
        All,

        /// <summary>
        /// Opened
        /// </summary>
        [Display(Name = "Opened")]
        Opened,

        /// <summary>
        /// Closed
        /// </summary>
        [Display(Name = "Closed")]
        Closed
    }
}
