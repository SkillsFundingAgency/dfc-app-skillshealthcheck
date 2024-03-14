namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    // Used for checking type for processing the checking result.
    public enum CheckingType
    {
        SimpleNumbers       = 2,
        FinancialFigures    = 4,
        AbstractCodes       = 1,
        DifferentFormats    = 8
    }

    // Used for checking Condition type for processing the checking result.
    public enum CheckingCondType
    {
        // When none is true
        NoType = 0,
        // Simple Numbers
        Type1 = 1,
        // Financial Figures
        Type2 = 2,
        // Abstract Codes
        Type3 = 3,
        // Different Formats
        Type4 = 4,
        // SimpleNumbers and FinancialFigures
        Type5 = 5,
        // SimpleNumbers and AbstractCodes
        Type6 = 6,
        // SimpleNumbers and DifferentFormats
        Type7 = 7,
        // FinancialFigures and AbstractCodes
        Type8 = 8,
        // FinancialFigures and DifferentFormats
        Type9 = 9,
        // AbstractCodes and DifferentFormats
        Type10 = 10,
        // More than two categories
        Type11 = 11
    }

    // Used define which section to show in Job suggestions.
    public enum JobSuggestionSummary
    {
        OnlySkills                      = 1,
        AllLittelAndNoInterest          = 2,
        OneVeryorModeratleyInterest     = 3,
        MoreVeryorModeratleyInterest    = 4
    }

    // Used define which template to show in Job suggestions.
    public enum JobSuggestionBody
    {        
        SkillsAndInterests  = 1,
        OnlySkills          = 2
    }

    // Enum to define available section in skilss health check report
    public enum SHCReportSection
    {
        SkillAreas,
        Interests,
        PersonalStyle,
        Motivation,
        Verbal,
        Checking,
        Mechanical,
        Shapes,
        Abstract,
        JobSugession
    }
}
