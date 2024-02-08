using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels.Question
{
    [ExcludeFromCodeCoverage]
    public class MultipleAnswerQuestionViewModel : AssessmentQuestionViewModel
    {
        public MultipleAnswerQuestionViewModel()
        {
            CurrentQuestion = 1;
        }

        public int CurrentQuestion { get; set; }

        public int SubQuestions { get; set; }
    }
}
