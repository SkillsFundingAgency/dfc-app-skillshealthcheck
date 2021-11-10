using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using Microsoft.Extensions.Options;

using Notify.Client;
using Notify.Models.Responses;

namespace DFC.App.SkillsHealthCheck.Services.GovNotify
{
    [ExcludeFromCodeCoverage]
    public class GovUkNotifyClientProxy : IGovUkNotifyClientProxy
    {
        private readonly NotificationClient client;

        public GovUkNotifyClientProxy(IOptions<GovNotifyOptions> govNotifyOptions)
        {
            _ = govNotifyOptions.Value?.APIKey ?? throw new ArgumentNullException(nameof(govNotifyOptions), "GovNotify API key is missing.");
            client = new NotificationClient(govNotifyOptions.Value.APIKey);
        }

        public async Task<EmailNotificationResponse> SendEmailAsync(string emailAddress, string templateId, Dictionary<string, dynamic> personalisation)
        {
            return await client.SendEmailAsync(emailAddress, templateId, personalisation);
        }

        public async Task<SmsNotificationResponse> SendSmsAsync(string phoneNumber, string templateId, Dictionary<string, dynamic> personalisation)
        {
            return await client.SendSmsAsync(phoneNumber, templateId, personalisation);
        }
    }
}
