using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
using DFC.App.SkillsHealthCheck.ViewModels;
using DFC.Compui.Sessionstate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    public abstract class BaseController<TController> : Controller
        where TController: Controller
    {
        private ISessionStateService<SessionDataModel> _sessionStateService;
        private ILogger<TController> _logger;

        public const string RegistrationPath = "skills-health-check";
        public const string DefaultPageTitleSuffix = "Skills Health Check | National Careers Service";

        public readonly string HomeURL = $"/{RegistrationPath}/home";
        public readonly string YourAssessmentsURL = $"/{RegistrationPath}/your-assessments";

        protected BaseController(ILogger<TController> logger, ISessionStateService<SessionDataModel> sessionStateService)
        {
            _logger = logger;
            _sessionStateService = sessionStateService;
        }

        protected HtmlHeadViewModel GetHtmlHeadViewModel(string pageTitle)
        {
            return new HtmlHeadViewModel
            {
                CanonicalUrl = new Uri($"{Request.GetBaseAddress()}/skills-health-check", UriKind.RelativeOrAbsolute),
                Title = $"{pageTitle} | {DefaultPageTitleSuffix}",
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
                _logger.LogInformation($"Getting the session state - compositeSessionId = {compositeSessionId}");

                return await _sessionStateService.GetAsync(compositeSessionId.Value);
            }

            _logger.LogError($"Error getting the session state - compositeSessionId = {compositeSessionId}");

            return default;
        }

        protected async Task<bool> SetSessionStateAsync(SessionDataModel sessionDataModel)
        {
            var compositeSessionId = Request.CompositeSessionId();
            if (compositeSessionId.HasValue)
            {
                _logger.LogInformation($"Getting the session state - compositeSessionId = {compositeSessionId}");

                var sessionStateModel = await _sessionStateService.GetAsync(compositeSessionId.Value);
                sessionStateModel.Ttl = 1800;
                sessionStateModel.State = sessionDataModel;

                _logger.LogInformation($"Saving the session state - compositeSessionId = {compositeSessionId}");

                var result = await _sessionStateService.SaveAsync(sessionStateModel);

                return result == HttpStatusCode.OK || result == HttpStatusCode.Created;
            }

            _logger.LogError($"Error saving the session state - compositeSessionId = {compositeSessionId}");

            return false;
        }

        protected async Task<bool> DeleteSessionStateAsync()
        {
            var compositeSessionId = Request.CompositeSessionId();
            if (compositeSessionId.HasValue)
            {
                _logger.LogInformation($"Deleting the session state - compositeSessionId = {compositeSessionId}");

                return await _sessionStateService.DeleteAsync(compositeSessionId.Value);
            }

            _logger.LogError($"Error deleting the session state - compositeSessionId = {compositeSessionId}");

            return false;
        }

    }
}
