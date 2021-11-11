using System.Collections.Generic;
using System.Threading.Tasks;

using Notify.Models.Responses;

namespace DFC.App.SkillsHealthCheck.Services.GovNotify
{
    public interface IGovUkNotifyClientProxy
    {
        Task<SmsNotificationResponse> SendSmsAsync(string phoneNumber, string templateId, Dictionary<string, dynamic> personalisation);

        Task<EmailNotificationResponse> SendEmailAsync(string emailAddress, string templateId, Dictionary<string, dynamic> personalisation);
    }
}