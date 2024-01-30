using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class RightBarViewModel
    {
        public string AssessmentType { get; set; }

        public string SpeakToAnAdviser { get; set; }

        public ReturnToAssessmentViewModel ReturnToAssessmentViewModel { get; set; }
    }
}
