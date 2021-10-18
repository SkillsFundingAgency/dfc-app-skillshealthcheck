using System.ComponentModel.DataAnnotations;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums
{
    public enum ReportType
    {
        [Display(Name = "Full report")]
        FullReport,
        [Display(Name = "Summary report")]
        SummaryReport
    }

    public enum DownloadType
    {
        [Display(Name = "PDF")]
        Pdf,
        [Display(Name = "Microsoft Word document")]
        Word
    }

    public enum AssessementAction
    {
        Start,
        Continue,
        Completed
    }

    public enum SkillsDocumentExpiry
    {
        None,
        Logical,
        Physical
    }

    /// <summary>
    /// Question alignment type enumeration
    /// </summary>
    public enum AnswerButtonGroupType
    {
        /// <summary>
        /// The horizontal
        /// </summary>
        PossibleResponses = 1,

        /// <summary>
        /// The vertical
        /// </summary>
        AssessmentHeaders = 2,

        /// <summary>
        /// The tabular checkbox
        /// </summary>
        TabularCheckbox = 3
    }
    /// <summary>
    /// Level enumeration
    /// </summary>
    public enum Level
    {
        /// <summary>
        /// The level1
        /// </summary>
        Level1 = 1,

        /// <summary>
        /// The level2
        /// </summary>
        Level2 = 2,

        /// <summary>
        /// The level3
        /// </summary>
        Level3 = 3
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
