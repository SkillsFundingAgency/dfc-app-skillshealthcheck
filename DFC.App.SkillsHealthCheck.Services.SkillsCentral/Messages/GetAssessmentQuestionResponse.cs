
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages
{
    public class GetAssessmentQuestionResponse : GenericResponse
    {
        public Question Question { get; set; }
    }
}
