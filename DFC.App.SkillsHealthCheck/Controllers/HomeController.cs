using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Data.Models.ContentModels;
using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
using DFC.App.SkillsHealthCheck.ViewModels;
using DFC.App.SkillsHealthCheck.ViewModels.Home;
using DFC.Compui.Cosmos.Contracts;
using DFC.Compui.Sessionstate;
using DFC.Content.Pkg.Netcore.Data.Models.ClientOptions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    [ExcludeFromCodeCoverage]
    public class HomeController : BaseController<HomeController>
    {
        private readonly ILogger<HomeController> logger;
        private readonly IDocumentService<SharedContentItemModel> sharedContentItemDocumentService;
        private readonly CmsApiClientOptions cmsApiClientOptions;
        private readonly IYourAssessmentsService yourAssessmentsService;
        private readonly ISkillsHealthCheckService skillsHealthCheckService;

        public HomeController(
            ILogger<HomeController> logger,
            ISessionStateService<SessionDataModel> sessionStateService,
            IDocumentService<SharedContentItemModel> sharedContentItemDocumentService,
            CmsApiClientOptions cmsApiClientOptions,
            ISkillsHealthCheckService skillsHealthCheckService,
            IYourAssessmentsService yourAssesmentsService)
        : base(logger, sessionStateService)
        {
            this.logger = logger;
            this.sharedContentItemDocumentService = sharedContentItemDocumentService;
            this.cmsApiClientOptions = cmsApiClientOptions;
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
            if (await CheckValidSession())
            {
                return Redirect(YourAssessmentsURL);
            }

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
                var sessionStateDataModel = new SessionDataModel
                {
                    DocumentId = apiResult.DocumentId,
                    AssessmentQuestionsOverViews = new Dictionary<string, AssessmentQuestionsOverView>(),
                };
                await SetSessionStateAsync(sessionStateDataModel);
                return Redirect(YourAssessmentsURL);
            }

            var bodyViewModel = await GetHomeBodyViewModel();

            return this.NegotiateContentResult(bodyViewModel);
        }

        [HttpGet]
        [Route("skills-health-check/home/htmlhead")]
        [HttpGet("skills-health-check/home/reload/htmlhead")]
        [Route("skills-health-check/return-to-assessment/htmlhead")]
        [Route("skills-health-check/{article}/htmlhead")]
        [Route("skills-health-check/htmlhead")]
        public IActionResult HtmlHead()
        {
            var viewModel = GetHtmlHeadViewModel(string.Empty);

            logger.LogInformation($"{nameof(HtmlHead)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        [Route("skills-health-check/home/breadcrumb")]
        [HttpGet("skills-health-check/home/reload/breadcrumb")]
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
            return this.NegotiateContentResult(viewModel);
        }

        [HttpGet("skills-health-check/home/reload")]
        [HttpGet("skills-health-check/home/reload/body")]
        public async Task<ActionResult> Reload(string sessionId)
        {
            var response = skillsHealthCheckService.GetSkillsDocumentByIdentifier(sessionId);
            if (response.Success && response.DocumentId > 0)
            {
                var sessionStateModel = await GetSessionDataModel() ?? new SessionDataModel();
                sessionStateModel.DocumentId = response.DocumentId;
                await SetSessionStateAsync(sessionStateModel);
                return Redirect(YourAssessmentsURL);
            }

            logger.LogError(response.ErrorMessage);
            return Redirect("/alerts/500?errorcode=saveProgressResponse");
        }

        [HttpPost]
        [Route("skills-health-check/return-to-assessment/body")]
        public async Task<IActionResult> ReturnToAssessment(ReturnToAssessmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var sessionStateModel = await GetSessionDataModel() ?? new SessionDataModel();
                var referenceFound = await yourAssessmentsService.GetSkillsDocumentIDByReferenceAndStore(sessionStateModel, viewModel.ReferenceId);
                if (referenceFound)
                {
                    await SetSessionStateAsync(sessionStateModel);
                    return Redirect(YourAssessmentsURL);
                }

                ModelState.AddModelError(nameof(ReturnToAssessmentViewModel.ReferenceId), Constants.SkillsHealthCheck.ReferenceCouldNotBeFoundMessage);
            }

            var bodyViewModel = await GetHomeBodyViewModel();
            viewModel.HasError = true;
            bodyViewModel.RightBarViewModel.ReturnToAssessmentViewModel = viewModel;
            return this.NegotiateContentResult(bodyViewModel);
        }

        [HttpPost]
        [Route("skills-health-check/return-to-assessment")]
        public async Task<IActionResult> ReturnToAssessmentDocument(ReturnToAssessmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var sessionStateModel = await GetSessionDataModel() ?? new SessionDataModel();
                var referenceFound = await yourAssessmentsService.GetSkillsDocumentIDByReferenceAndStore(sessionStateModel, viewModel.ReferenceId);
                if (referenceFound)
                {
                    await SetSessionStateAsync(sessionStateModel);
                    return Redirect(YourAssessmentsURL);
                }

                ModelState.AddModelError(nameof(ReturnToAssessmentViewModel.ReferenceId), Constants.SkillsHealthCheck.ReferenceCouldNotBeFoundMessage);
            }

            var bodyViewModel = await GetHomeBodyViewModel();
            viewModel.HasError = true;
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

            SharedContentItemModel? speakToAnAdviser = null;
            if (!string.IsNullOrWhiteSpace(cmsApiClientOptions.ContentIds))
            {
                speakToAnAdviser = await sharedContentItemDocumentService
                      .GetByIdAsync(new Guid(cmsApiClientOptions.ContentIds));
            }

            if (speakToAnAdviser != null)
            {
                viewModel.RightBarViewModel.SpeakToAnAdviser = speakToAnAdviser;
            }

            return viewModel;
        }
    }
}
