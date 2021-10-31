using System.ComponentModel.DataAnnotations;

namespace Piling.Model
{
    /// <summary>
    /// ort status enum information model
    /// </summary>
    public enum OutputFormat
    {
        /// <summary>
        /// Txt
        /// </summary>
        [Display(Name = "txt")]
        Txt=0,

        /// <summary>
        /// Opened
        /// </summary>
        [Display(Name = "csv")]
        Csv=1,

        /// <summary>
        /// Closed
        /// </summary>
        [Display(Name = "json")]
        Json=2,

        /// <summary>
        /// Unsupported type
        /// </summary>
        [Display(Name = "undefined")]
        Undefined = 3
    }
}
