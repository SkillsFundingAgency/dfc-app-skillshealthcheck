namespace DFC.App.SkillsHealthCheck.ViewModels.Question
{
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
