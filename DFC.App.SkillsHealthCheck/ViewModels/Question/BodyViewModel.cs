using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels.Question
{
    [ExcludeFromCodeCoverage]
    public class BodyViewModel : IBodyPostback
    {
        public AssessmentQuestionViewModel AssessmentQuestionViewModel { get; set; }

        public RightBarViewModel RightBarViewModel { get; set; }

        public bool SessionTimedOut { get; set; }

    }
}
