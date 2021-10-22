using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels.Question
{
    [ExcludeFromCodeCoverage]
    public class SkillsHealthCheckValidationErrors
    {
        public string ErrorCannotSelectBothTypes { get; set; }

        public string ErrorChooseAnswer { get; set; }

        public string ErrorMaxJobsSelection { get; set; }
    }
}
