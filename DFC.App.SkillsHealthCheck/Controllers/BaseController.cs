using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.ViewModels;
using DFC.Compui.Sessionstate;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    public abstract class BaseController<TController> : Controller
        where TController : Controller
    {
        public const string RegistrationPath = "skills-health-check";
        public const string DefaultPageTitleSuffix = "Skills Health Check | National Careers Service";

        public static readonly string HomeURL = $"/{RegistrationPath}/home";
        public static readonly string YourAssessmentsURL = $"/{RegistrationPath}/your-assessments";
        public static readonly string QuestionURL = $"/{RegistrationPath}/question";
        public static readonly string SessionTimeoutURL = $"/{RegistrationPath}/session-timeout";
        private readonly ISessionStateService<SessionDataModel> sessionStateService;
        private readonly ILogger<TController> logger;

        protected BaseController(ILogger<TController> logger, ISessionStateService<SessionDataModel> sessionStateService)
        {
            this.logger = logger;
            this.sessionStateService = sessionStateService;
        }

        protected HtmlHeadViewModel GetHtmlHeadViewModel(string pageTitle)
        {
            return new HtmlHeadViewModel
            {
                CanonicalUrl = new Uri($"{Request.GetBaseAddress()}/{RegistrationPath}", UriKind.RelativeOrAbsolute),
                Title = !string.IsNullOrWhiteSpace(pageTitle) ? $"{pageTitle} | {DefaultPageTitleSuffix}" : DefaultPageTitleSuffix,
            };
        }

        protected static BreadcrumbViewModel BuildBreadcrumb()
        {
            return new BreadcrumbViewModel
            {
                Breadcrumbs = new List<BreadcrumbItemViewModel>
                {
                    new BreadcrumbItemViewModel
                    {
                        Route = "/",
                        Title = "Home",
                    },
                },
            };
        }

        protected async Task<bool> CheckValidSession()
        {
            var sessionStateModel = await GetSessionStateAsync();
            return sessionStateModel?.State != null && sessionStateModel.State.DocumentId != 0;
        }

        protected async Task<long?> GetDocumentId()
        {
            var sessionStateModel = await GetSessionStateAsync();
            return sessionStateModel?.State?.DocumentId;
        }

        protected async Task<SessionDataModel?> GetSessionDataModel()
        {
            var sessionStateModel = await GetSessionStateAsync();
            return sessionStateModel?.State;
        }

        protected async Task<SessionStateModel<SessionDataModel>?> GetSessionStateAsync()
        {
            var compositeSessionId = Request.CompositeSessionId();
            if (compositeSessionId.HasValue)
            {
                logger.LogInformation($"Getting the session state - compositeSessionId = {compositeSessionId}");

                return await sessionStateService.GetAsync(compositeSessionId.Value);
            }

            logger.LogError($"Error getting the session state - compositeSessionId = {compositeSessionId}");

            return default;
        }

        protected async Task<bool> SetSessionStateAsync(SessionDataModel sessionDataModel)
        {
            var compositeSessionId = Request.CompositeSessionId();
            if (compositeSessionId.HasValue)
            {
                logger.LogInformation($"Getting the session state - compositeSessionId = {compositeSessionId}");

                var sessionStateModel = await sessionStateService.GetAsync(compositeSessionId.Value);
                sessionStateModel.Ttl = 1800;
                sessionStateModel.State = sessionDataModel;

                logger.LogInformation($"Saving the session state - compositeSessionId = {compositeSessionId}");

                var result = await sessionStateService.SaveAsync(sessionStateModel);

                return result == HttpStatusCode.OK || result == HttpStatusCode.Created;
            }

            logger.LogError($"Error saving the session state - compositeSessionId = {compositeSessionId}");

            return false;
        }

        protected async Task<bool> DeleteSessionStateAsync()
        {
            var compositeSessionId = Request.CompositeSessionId();
            if (compositeSessionId.HasValue)
            {
                logger.LogInformation($"Deleting the session state - compositeSessionId = {compositeSessionId}");

                return await sessionStateService.DeleteAsync(compositeSessionId.Value);
            }

            logger.LogError($"Error deleting the session state - compositeSessionId = {compositeSessionId}");

            return false;
        }

        protected async Task SetAssessmentTypeAsync(string? type)
        {
            var sessionStateModel = await GetSessionStateAsync();
            if (sessionStateModel?.State != null)
            {
                sessionStateModel.State.AssessmentType = type;
                await SetSessionStateAsync(sessionStateModel.State);
            }
        }

        protected async Task<string?> GetAssessmentTypeAsync()
        {
            var sessionStateModel = await GetSessionStateAsync();
            return sessionStateModel?.State?.AssessmentType;
        }
    }
}
