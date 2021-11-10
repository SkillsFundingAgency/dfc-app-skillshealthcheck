using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

namespace DFC.App.SkillsHealthCheck.Services.GovNotify
{
    public class GovNotifyService : IGovNotifyService
    {
        private readonly IGovUkNotifyClientProxy notifyClientProxy;
        private readonly GovNotifyOptions govNotifyOptions;

        public GovNotifyService(IGovUkNotifyClientProxy notifyClientProxy, IOptions<GovNotifyOptions> options)
        {
            this.notifyClientProxy = notifyClientProxy;
            govNotifyOptions = options.Value ?? throw new ArgumentNullException(nameof(GovNotifyOptions));
        }

        public async Task<NotifyResponse> SendEmailAsync(string domain, string emailAddress, string sessionId)
        {
            try
            {
                var personalisation = GetPersonalisation(domain, sessionId);
                var response = await notifyClientProxy.SendEmailAsync(emailAddress, govNotifyOptions.EmailTemplateId, personalisation);
                return new NotifyResponse() { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new NotifyResponse() { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<NotifyResponse> SendSmsAsync(string domain, string mobileNumber, string sessionId)
        {
            try
            {
                var personalisation = GetPersonalisation(domain, sessionId);
                var response = await notifyClientProxy.SendSmsAsync(mobileNumber, govNotifyOptions.SmsTemplateId, personalisation);
                return new NotifyResponse() { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new NotifyResponse() { IsSuccess = false, Message = ex.Message };
            }
        }

        private Dictionary<string, dynamic> GetPersonalisation(string domain, string sessionId) => new Dictionary<string, dynamic>
        {
            { "session_id", sessionId},
            { "assessment_date",DateTime.Now.ToString("dd MM yyyy") },
            { "reload_url",  $"{domain}/reload?sessionId={sessionId}" }
        };
    }
}
