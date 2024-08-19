using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels.Question
{
    [ExcludeFromCodeCoverage]
    public class TabularAnswerQuestionViewModel : AssessmentQuestionViewModel
    {
        public TabularAnswerQuestionViewModel()
        {
            CurrentQuestion = 1;
            CurrentRow = 1;
        }

        public string? QuestionAnswer { get; set; }


        public int CurrentQuestion { get; set; }

        public int CurrentRow { get; set; }

        public int SubQuestions { get; set; }

        public IEnumerable<string>? AnswerSelection { get; set; }
    }
}
