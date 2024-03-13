// -----------------------------------------------------------------------
// <copyright file="Enums.cs" company="tesl.com">
// Trinity Expert Systems
// </copyright>
// -----------------------------------------------------------------------
namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    #region | Enums |

    /// <summary>
    /// Used for checking type for processing the checking result.
    /// </summary>   
    public enum CheckingType
    {
        /// <summary>
        /// Simple Numbers
        /// </summary>
        SimpleNumbers = 2,

        /// <summary>
        /// Financial Figures
        /// </summary>
        FinancialFigures = 4,

        /// <summary>
        /// Abstract Codes
        /// </summary>
        AbstractCodes = 1,

        /// <summary>
        /// Different Formats
        /// </summary>
        DifferentFormats = 8
    }

    /// <summary>
    /// Used for checking Condition type for processing the checking result.
    /// </summary>
    public enum CheckingCondType
    {
        /// <summary>
        /// when none is true
        /// </summary>
        NoType = 0,

        /// <summary>
        /// Simple Numbers
        /// </summary>
        Type1 = 1,

        /// <summary>
        /// Financial Figures
        /// </summary>
        Type2 = 2,

        /// <summary>
        /// Abstract Codes
        /// </summary>
        Type3 = 3,

        /// <summary>
        /// Different Formats
        /// </summary>
        Type4 = 4,

        /// <summary>
        /// SimpleNumbers and FinancialFigures
        /// </summary>
        Type5 = 5,

        /// <summary>
        /// SimpleNumbers and AbstractCodes
        /// </summary>
        Type6 = 6,

        /// <summary>
        /// SimpleNumbers and DifferentFormats
        /// </summary>
        Type7 = 7,

        /// <summary>
        /// FinancialFigures and AbstractCodes
        /// </summary>
        Type8 = 8,

        /// <summary>
        /// FinancialFigures and DifferentFormats
        /// </summary>
        Type9 = 9,

        /// <summary>
        /// AbstractCodes and DifferentFormats
        /// </summary>
        Type10 = 10,

        /// <summary>
        /// If more than two categories
        /// </summary>
        Type11 = 11
    }

    /// <summary>
    /// Used define which section to show in Job suggestions.
    /// </summary>
    public enum JobSuggestionSummary
    {
        /// <summary>
        /// Only skills section
        /// </summary>
        OnlySkills = 1,

        /// <summary>
        /// All interest of little and no interest
        /// </summary>
        AllLittelAndNoInterest = 2,

        /// <summary>
        /// Atleast one interest is very or moderately interested
        /// </summary>
        OneVeryorModeratleyInterest = 3,

        /// <summary>
        /// More than one interest is very or moderately interested
        /// </summary>
        MoreVeryorModeratleyInterest = 4
    }

    /// <summary>
    /// Used define which template to show in Job suggestions.
    /// </summary>
    public enum JobSuggestionBody
    {        
        /// <summary>
        /// Skills and Interest Template
        /// </summary>
        SkillsAndInterests = 1,

        /// <summary>
        /// Skills Template
        /// </summary>
        OnlySkills = 2
    }

    /// <summary>
    /// Enum to define available section in skilss health check report
    /// </summary>
    public enum SHCReportSection
    {
        /// <summary>
        /// Skill Areas section
        /// </summary>
        SkillAreas,

        /// <summary>
        /// Interest section
        /// </summary>
        Interests,

        /// <summary>
        /// Personal style section
        /// </summary>
        PersonalStyle,

        /// <summary>
        /// Motivation section
        /// </summary>
        Motivation,

        /// <summary>
        /// Working with Numbers section
        /// </summary>
        Numbers,

        /// <summary>
        /// Working with Written information section
        /// </summary>
        Verbal,

        /// <summary>
        /// Checking information section
        /// </summary>
        Checking,

        /// <summary>
        /// Solving Mechanical problems
        /// </summary>
        Mechanical,

        /// <summary>
        /// Working with shapes
        /// </summary>
        Shapes,

        /// <summary>
        /// Solving abstract problems
        /// </summary>
        Abstract,

        /// <summary>
        /// Job sugesstion
        /// </summary>
        JobSugession
    }
    #endregion
}
