using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages
{
    public class GetIdentityDetailsResponse : GenericResponse
    {
        public CitizenIdentity Identity { get; set; }
    }
}
