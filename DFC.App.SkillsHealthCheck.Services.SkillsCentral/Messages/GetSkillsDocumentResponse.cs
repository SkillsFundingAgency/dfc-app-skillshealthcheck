using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages
{
    public class GetSkillsDocumentResponse : GenericResponse
    {
        public SkillsDocument SkillsDocument { get; set; }
    }
}
