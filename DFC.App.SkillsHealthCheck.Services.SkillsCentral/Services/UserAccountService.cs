using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SFA.Careers.Citizen.Data.CitizenModel;
using SFA.Careers.Citizen.Data.IdentityModel;
using SFA.Careers.Identity;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Services
{
    public class UserAccountService : IUserAccountService
    {

        public UserAccountService()
        {
        }

        public async Task<GetIdentityDetailsResponse> GetIdentityDetailsByCitizenIdAsync(Guid citizenId)
        {
            if (citizenId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(citizenId));
            }
            var result = new GetIdentityDetailsResponse();
            try
            {
                var current = await GetAccountDetailsByCitizenIdPrivateAsync(citizenId);

                if (current != null)
                {
                    result.Identity = current.GetCitizenIdentity();
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        private async Task<Identity> GetAccountDetailsByCitizenIdPrivateAsync(Guid citizenId)
        {
            var citizen = new Citizen(citizenId, CitizenAccessContext.Self);

            return await citizen.GetIdentityAsync();
        }

    }
}
