using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages
{
    public class GetSingleQuestionRequest
    {
        public int QuestionNumber { get; set; }

        public AssessmentType AsessmentType { get; set; }

        public Accessibility Accessibility { get; set; }

        public Level Level { get; set; }
    }
}
