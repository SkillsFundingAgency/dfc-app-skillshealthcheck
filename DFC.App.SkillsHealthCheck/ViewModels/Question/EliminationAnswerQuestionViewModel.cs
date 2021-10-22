using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels.Question
{
    [ExcludeFromCodeCoverage]
    public class EliminationAnswerQuestionViewModel : AssessmentQuestionViewModel
    {
        public EliminationAnswerQuestionViewModel()
        {
            AlreadySelected = -1;
        }

        public int AlreadySelected { get; set; }
    }
}
