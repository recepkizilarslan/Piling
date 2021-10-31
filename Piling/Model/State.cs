using System.ComponentModel.DataAnnotations;

namespace Piling.Model
{
    /// <summary>
    /// Running state of the application
    /// </summary>
    public enum State
    {
        /// <summary>
        /// Not Scanned
        /// </summary>
        [Display(Name = "Running")]
        Running=0,

        /// <summary>
        /// Not Scanned
        /// </summary>
        [Display(Name = "Resume")]
        Resume=1,

        /// <summary>
        /// Not Scanned
        /// </summary>
        [Display(Name = "Pause")]
        Pause=2,

        /// <summary>
        /// Not Scanned
        /// </summary>
        [Display(Name = "Stop")]
        Stop=3,
    }
}
