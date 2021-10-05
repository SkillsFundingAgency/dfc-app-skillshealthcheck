using System;
using System.Net;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Data.Enums;

namespace DFC.App.SkillsHealthCheck.Data.Contracts
{
    public interface IWebhooksService
    {
        Task<HttpStatusCode> ProcessMessageAsync(WebhookCacheOperation webhookCacheOperation, Guid eventId, Guid contentId, string apiEndpoint);
    }
}
