using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using System;
using System.Threading.Tasks;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces
{
    public interface IUserAccountService 
    {
        Task<GetIdentityDetailsResponse> GetIdentityDetailsByCitizenIdAsync(Guid citizenId);
    }
}
