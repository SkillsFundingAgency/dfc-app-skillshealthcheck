using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages
{
    public class SaveQuestionAnswerRequest
    {
        public SkillsDocument SkillsDocument { get; set; }

        public long DocumentId { get; set; }
    }
}
