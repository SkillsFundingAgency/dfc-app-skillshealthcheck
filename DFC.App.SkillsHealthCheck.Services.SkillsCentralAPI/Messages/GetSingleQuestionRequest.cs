
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages
{
    public class GetSingleQuestionResponse : GenericResponse
    {
        public Question Question { get; set; }
    }
}
