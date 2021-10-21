namespace DFC.App.SkillsHealthCheck.ViewModels.Question
{
    public class EliminationAnswerQuestionViewModel : AssessmentQuestionViewModel
    {
        public EliminationAnswerQuestionViewModel()
        {
            AlreadySelected = -1;
        }

        public int AlreadySelected { get; set; }
    }
}
