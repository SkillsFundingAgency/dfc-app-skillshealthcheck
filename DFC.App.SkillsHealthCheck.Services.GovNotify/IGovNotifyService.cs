using System.Threading.Tasks;

namespace DFC.App.SkillsHealthCheck.Services.GovNotify
{
    public interface IGovNotifyService
    {
        Task<NotifyResponse> SendEmailAsync(string domain, string emailAddress, string sessionId);

        Task<NotifyResponse> SendSmsAsync(string domain, string mobileNumber, string sessionId);
    }
}