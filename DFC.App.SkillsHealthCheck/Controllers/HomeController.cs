﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    [ExcludeFromCodeCoverage]
    public class HomeController : BaseController<HomeController>
    {
        private const string SharedContentStaxId = "2c9da1b3-3529-4834-afc9-9cd741e59788";
        private readonly ILogger<HomeController> logger;
        private readonly ISharedContentRedisInterface sharedContentRedis;
        private readonly IYourAssessmentsService yourAssessmentsService;
        private readonly ISkillsHealthCheckService skillsHealthCheckService;

        public HomeController(
            ILogger<HomeController> logger,
            ISessionStateService<SessionDataModel> sessionStateService,
            IOptions<SessionStateOptions> sessionStateOptions,
            ISharedContentRedisInterface sharedContentRedis,
            ISkillsHealthCheckService skillsHealthCheckService,
            IYourAssessmentsService yourAssesmentsService)
        : base(logger, sessionStateService, sessionStateOptions)
        {
            this.logger = logger;
            this.sharedContentRedis = sharedContentRedis;
            this.skillsHealthCheckService = skillsHealthCheckService;
            this.yourAssessmentsService = yourAssesmentsService;
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

            var apiRequest = new CreateSkillsDocumentRequest
            {
                SkillsDocument = new SkillsDocument
                {
                    SkillsDocumentTitle = Constants.SkillsHealthCheck.DefaultDocumentName,
                    SkillsDocumentType = Constants.SkillsHealthCheck.DocumentType,
                    CreatedBy = Constants.SkillsHealthCheck.AnonymousUser,
                    SkillsDocumentExpiry = SkillsDocumentExpiry.Physical,
                    ExpiresTimespan = new TimeSpan(0, Constants.SkillsHealthCheck.SkillsDocumentExpiryTime, 0, 0),
                },
            };

            if (!string.IsNullOrWhiteSpace(viewModel?.ListTypeFields))
            {
                var fieldList = viewModel.ListTypeFields.Split(',');
                foreach (var field in fieldList.Where(f => !f.Equals(Constants.SkillsHealthCheck.FieldName, StringComparison.InvariantCultureIgnoreCase)))
                {
                    var value = string.Empty;

                    if (field.Equals(Constants.SkillsHealthCheck.QualificationProperty, StringComparison.InvariantCultureIgnoreCase))
                    {
                        value = "1";
                    }
                    else if (field.Equals(Constants.SkillsHealthCheck.CandidateFullNameKeyName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        value = apiRequest.SkillsDocument.CreatedBy;
                    }

                    apiRequest.SkillsDocument.SkillsDocumentDataValues.Add(new SkillsDocumentDataValue
                    {
                        Title = field,
                        Value = value,
                    });
                }
            }

            apiRequest.SkillsDocument.SkillsDocumentIdentifiers.Add(new SkillsDocumentIdentifier
            {
                ServiceName = Constants.SkillsHealthCheck.DocumentSystemIdentifierName,
                Value = Guid.NewGuid().ToString(),
            });

            var apiResult = skillsHealthCheckService.CreateSkillsDocument(apiRequest);
            if (apiResult.Success)
            {
                logger.LogInformation($" Created new Skills Document, redirectng");

                var sessionStateDataModel = new SessionDataModel
                {
                    DocumentId = apiResult.DocumentId,
                    AssessmentQuestionsOverViews = new Dictionary<string, AssessmentQuestionsOverView>(),
                };
                await SetSessionStateAsync(sessionStateDataModel);
                return Redirect(YourAssessmentsURL);
            }

            var bodyViewModel = await GetHomeBodyViewModel();

            logger.LogWarning($" Creating new Skills Document wasnt successful");

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

            var response = skillsHealthCheckService.GetSkillsDocumentByIdentifier(sessionId);
            if (response.Success && response.DocumentId > 0)
            {
                var sessionStateModel = await GetSessionDataModel() ?? new SessionDataModel();
                sessionStateModel.DocumentId = response.DocumentId;
                await SetSessionStateAsync(sessionStateModel);
                logger.LogInformation($"{nameof(Reload)} was successful");
                return Redirect(YourAssessmentsURL);
            }

            logger.LogError($"{nameof(Reload)} failed with message: {response.ErrorMessage}");
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
            logger.LogWarning($"Couldn't return to the assesment for viewModel: {viewModel}");
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
            logger.LogWarning($"Couldn't return to the assesment for viewModel: {viewModel}");
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

            // TODO: do we really need to call and store this here, we can just get these on the your assessments page
            var apiResult = skillsHealthCheckService.GetListTypeFields(new GetListTypeFieldsRequest
            {
                DocumentType = Constants.SkillsHealthCheck.DocumentType,
            });

            if (apiResult.Success)
            {
                viewModel.ListTypeFields = string.Join(",", apiResult.TypeFields);
            }

            try
            {
                var speakToAnAdviser= await sharedContentRedis.GetDataAsync<SharedHtml>("SharedContent/" + SharedContentStaxId);
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
