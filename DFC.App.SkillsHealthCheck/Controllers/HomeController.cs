﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using DFC.SkillsCentral.Api.Domain.Models;
using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
using DFC.App.SkillsHealthCheck.ViewModels;
using DFC.App.SkillsHealthCheck.ViewModels.Home;
using DFC.Common.SharedContent.Pkg.Netcore.Interfaces;
using DFC.Common.SharedContent.Pkg.Netcore.Model.ContentItems.SharedHtml;
using DFC.Compui.Sessionstate;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AppConstants = DFC.Common.SharedContent.Pkg.Netcore.Constant.ApplicationKeys;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    [ExcludeFromCodeCoverage]
    public class HomeController : BaseController<HomeController>
    {
        private const string ExpiryAppSettings = "Cms:Expiry";
        private readonly ILogger<HomeController> logger;
        private readonly ISharedContentRedisInterface sharedContentRedis;
        private readonly IYourAssessmentsService yourAssessmentsService;
        private readonly ISkillsHealthCheckService skillsHealthCheckService;
        private readonly IConfiguration configuration;
        private string status;
        private double expiryInHours = 4;

        public HomeController(
            ILogger<HomeController> logger,
            ISessionStateService<SessionDataModel> sessionStateService,
            IOptions<SessionStateOptions> sessionStateOptions,
            ISharedContentRedisInterface sharedContentRedis,
            ISkillsHealthCheckService skillsHealthCheckService,
            IYourAssessmentsService yourAssessmentsService,
            IConfiguration configuration)
        : base(logger, sessionStateService, sessionStateOptions)
        {
            this.logger = logger;
            this.sharedContentRedis = sharedContentRedis;
            this.skillsHealthCheckService = skillsHealthCheckService;
            this.yourAssessmentsService = yourAssessmentsService;
            this.configuration = configuration;
            status = configuration.GetSection("contentMode:contentMode").Get<string>();
            if (this.configuration != null)
            {
                string expiryAppString = this.configuration.GetSection(ExpiryAppSettings).Get<string>();
                if (double.TryParse(expiryAppString, out var expiryAppStringParseResult))
                {
                    expiryInHours = expiryAppStringParseResult;
                }
            }
        }

        [HttpGet]
        [Route("skills-health-check/home/document")]
        [Route("skills-health-check/document")]
        [Route("skills-health-check/home")]
        [Route("skills-health-check")]
        public async Task<IActionResult> Document()
        {
            var htmlHeadViewModel = GetHtmlHeadViewModel(string.Empty);
            var breadcrumbViewModel = BuildBreadcrumb();
            var bodyViewModel = await GetHomeBodyViewModel();

            return this.NegotiateContentResult(new DocumentViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                BodyViewModel = bodyViewModel,
            });
        }

        [HttpPost]
        [Route("skills-health-check/start-skills-health-check")]
        [Route("skills-health-check/start-skills-health-check/body")]
        public async Task<IActionResult> StartSkillsHealthCheck(BodyViewModel viewModel)
        {
            logger.LogInformation($"{nameof(StartSkillsHealthCheck)} has been called");

            if (await CheckValidSession())
            {
                logger.LogInformation($"Found valid session, redirecting");
                return Redirect(YourAssessmentsURL);
            }

            logger.LogInformation($"creating new skills document request");

            var skillsDocument = new SkillsCentral.Api.Domain.Models.SkillsDocument
            {
                CreatedBy = Constants.SkillsHealthCheck.AnonymousUser,
                ReferenceCode = Guid.NewGuid().ToString(),
            };

            var apiResult = await skillsHealthCheckService.CreateSkillsDocument(skillsDocument);
            if (apiResult != null)
            {
                logger.LogInformation($" Created new Skills Document, redirectng");

                var sessionStateDataModel = new SessionDataModel
                {
                    DocumentId = (long)apiResult.Id,
                    AssessmentQuestionsOverViews = new Dictionary<string, AssessmentQuestionsOverView>(),
                };
                await SetSessionStateAsync(sessionStateDataModel);
                return Redirect(YourAssessmentsURL);
            }

            var bodyViewModel = await GetHomeBodyViewModel();

            logger.LogWarning($" Creating new Skills Document was not successful");

            return this.NegotiateContentResult(bodyViewModel);
        }

        [HttpGet]
        [Route("skills-health-check/home/htmlhead")]
        [Route("skills-health-check/home/reload/htmlhead")]
        [Route("skills-health-check/return-to-assessment/htmlhead")]
        [Route("skills-health-check/{article}/htmlhead")]
        [Route("skills-health-check/htmlhead")]
        public IActionResult HtmlHead()
        {
            var viewModel = GetHtmlHeadViewModel(string.Empty);

            logger.LogInformation($"{nameof(HtmlHead)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        [HttpGet]
        [Route("skills-health-check/home/reload/breadcrumb")]
        [Route("skills-health-check/home/breadcrumb")]
        [Route("skills-health-check/return-to-assessment/breadcrumb")]
        [Route("skills-health-check/{article}/breadcrumb")]
        [Route("skills-health-check/breadcrumb")]
        public IActionResult Breadcrumb()
        {
            var viewModel = BuildBreadcrumb();

            logger.LogInformation($"{nameof(Breadcrumb)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        [HttpGet]
        [Route("skills-health-check/home/body")]
        [Route("skills-health-check/{article}/body")]
        [Route("skills-health-check/body")]
        public async Task<IActionResult> Body()
        {
            var viewModel = await GetHomeBodyViewModel();

            logger.LogInformation($"{nameof(Body)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        [HttpGet]
        [Route("skills-health-check/home/reload")]
        [Route("skills-health-check/home/reload/body")]
        public async Task<ActionResult> Reload(string sessionId)
        {
            logger.LogInformation($"{nameof(Reload)} has been called");

            var response = await skillsHealthCheckService.GetSkillsDocumentByReferenceCode(sessionId);
            if (response.Id > 0)
            {
                var sessionStateModel = await GetSessionDataModel() ?? new SessionDataModel();
                sessionStateModel.DocumentId = (long)response.Id;
                await SetSessionStateAsync(sessionStateModel);
                logger.LogInformation($"{nameof(Reload)} was successful");
                return Redirect(YourAssessmentsURL);
            }

            logger.LogError($"{nameof(Reload)} failed for session id: {response.ReferenceCode}");
            return Redirect("/alerts/500?errorcode=saveProgressResponse");
        }

        [HttpPost]
        [Route("skills-health-check/return-to-assessment/body")]
        public async Task<IActionResult> ReturnToAssessment(ReturnToAssessmentViewModel viewModel)
        {
            logger.LogInformation($"{nameof(ReturnToAssessment)} has been called");

            if (ModelState.IsValid)
            {
                logger.LogInformation("ModelState is valid, looking for assessment reference");

                var sessionStateModel = await GetSessionDataModel() ?? new SessionDataModel();
                var referenceFound = await yourAssessmentsService.GetSkillsDocumentIDByReferenceAndStore(sessionStateModel, viewModel.ReferenceId);
                if (referenceFound)
                {
                    await SetSessionStateAsync(sessionStateModel);
                    logger.LogInformation($"Reference found, redirecting");
                    return Redirect(YourAssessmentsURL);
                }

                logger.LogWarning("Couldn't find valid reference, adding model error");

                ModelState.AddModelError(nameof(ReturnToAssessmentViewModel.ReferenceId), Constants.SkillsHealthCheck.ReferenceCouldNotBeFoundMessage);
            }

            var bodyViewModel = await GetHomeBodyViewModel();
            viewModel.HasError = true;
            logger.LogWarning($"Couldn't return to the assessment for viewModel: {viewModel}");
            bodyViewModel.RightBarViewModel.ReturnToAssessmentViewModel = viewModel;
            return this.NegotiateContentResult(bodyViewModel);
        }

        [HttpPost]
        [Route("skills-health-check/return-to-assessment")]
        public async Task<IActionResult> ReturnToAssessmentDocument(ReturnToAssessmentViewModel viewModel)
        {
            logger.LogInformation($"{nameof(ReturnToAssessmentDocument)} has been called");

            if (ModelState.IsValid)
            {
                logger.LogInformation("ModelState is valid, looking for assessment document reference");
                var sessionStateModel = await GetSessionDataModel() ?? new SessionDataModel();
                var referenceFound = await yourAssessmentsService.GetSkillsDocumentIDByReferenceAndStore(sessionStateModel, viewModel.ReferenceId);
                if (referenceFound)
                {
                    await SetSessionStateAsync(sessionStateModel);
                    logger.LogInformation($"Assessment document reference found, redirecting");
                    return Redirect(YourAssessmentsURL);
                }

                logger.LogWarning("Couldn't find valid reference, adding model error");

                ModelState.AddModelError(nameof(ReturnToAssessmentViewModel.ReferenceId), Constants.SkillsHealthCheck.ReferenceCouldNotBeFoundMessage);
            }

            var bodyViewModel = await GetHomeBodyViewModel();
            viewModel.HasError = true;
            logger.LogWarning($"Couldn't return to the assessment for viewModel: {viewModel}");
            bodyViewModel.RightBarViewModel.ReturnToAssessmentViewModel = viewModel;
            var htmlHeadViewModel = GetHtmlHeadViewModel(string.Empty);
            var breadcrumbViewModel = BuildBreadcrumb();
            return this.NegotiateContentResult(new DocumentViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                BodyViewModel = bodyViewModel,
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            logger.LogInformation($"{nameof(Error)} has been called");

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<BodyViewModel> GetHomeBodyViewModel()
        {
            if (await CheckValidSession())
            {
                Response.Redirect(YourAssessmentsURL);
            }

            var viewModel = new BodyViewModel
            {
                RightBarViewModel = new RightBarViewModel
                {
                    ReturnToAssessmentViewModel = new ReturnToAssessmentViewModel
                    {
                        ActionUrl = "/skills-health-check/return-to-assessment",
                    },
                },
            };

            try
            {
                if (string.IsNullOrEmpty(status))
                {
                    status = "PUBLISHED";
                }

                var speakToAnAdviser = await sharedContentRedis.GetDataAsyncWithExpiry<SharedHtml>(AppConstants.SpeakToAnAdviserSharedContent, status, expiryInHours);
                viewModel.RightBarViewModel.SpeakToAnAdviser = speakToAnAdviser.Html;
            }
            catch (Exception e)
            {
                viewModel.RightBarViewModel.SpeakToAnAdviser = "<h1> Error Retrieving Data from Redis<h1><p>" + e.ToString() + "</p>";
            }

            return viewModel;
        }
    }
}
