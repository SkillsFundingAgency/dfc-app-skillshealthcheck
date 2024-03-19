using System.ComponentModel.DataAnnotations;

namespace DFC.App.SkillsHealthCheck.Enums
{
    public enum DownloadType
    {
        [Display(Name = "PDF")]
        Pdf,
        [Display(Name = "Microsoft Word document")]
        Word
    }

    /// <summary>
    /// Accessibility enumeration
    /// </summary>
    public enum Accessibility
    {
        /// <summary>
        /// The full
        /// </summary>
        Full,

        /// <summary>
        /// The accessible
        /// </summary>
        Accessible
    }

    public enum AssessmentType
    {
        /// <summary>
        /// The abstract question set
        /// </summary>
        Abstract,

        /// <summary>
        /// The checking question set
        /// </summary>
        Checking,

        /// <summary>
        /// The interests question set
        /// </summary>
        Interest,

        /// <summary>
        /// The mechanical question set
        /// </summary>
        Mechanical,

        /// <summary>
        /// The motivation question set
        /// </summary>
        Motivation,

        /// <summary>
        /// The numerical question set
        /// </summary>
        Numeric,

        /// <summary>
        /// The personal question set
        /// </summary>
        Personal,

        /// <summary>
        /// The skill areas question set
        /// </summary>
        SkillAreas,

        /// <summary>
        /// The spatial question set
        /// </summary>
        Spatial,

        /// <summary>
        /// The verbal question set
        /// </summary>
        Verbal
    }


}
