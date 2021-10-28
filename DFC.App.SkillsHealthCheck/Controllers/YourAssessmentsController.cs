using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Data.Models.ContentModels;
using DFC.App.SkillsHealthCheck.Enums;
using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.Interfaces;
using DFC.App.SkillsHealthCheck.ViewModels;
using DFC.App.SkillsHealthCheck.ViewModels.YourAssessments;
using DFC.Compui.Cosmos.Contracts;
using DFC.Compui.Sessionstate;
using DFC.Content.Pkg.Netcore.Data.Models.ClientOptions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using static DFC.App.SkillsHealthCheck.Constants;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    [ExcludeFromCodeCoverage]
    public class YourAssessmentsController : BaseController<YourAssessmentsController>
    {
        public const string PageTitle = "Your assessments";
        public const string PagePart = "your-assessments";
        private readonly ILogger<YourAssessmentsController> logger;
        private readonly IDocumentService<SharedContentItemModel> sharedContentItemDocumentService;
        private readonly CmsApiClientOptions cmsApiClientOptions;
        private readonly IYourAssessmentsService yourAssessmentsService;

        public YourAssessmentsController(
            ILogger<YourAssessmentsController> logger,
            ISessionStateService<SessionDataModel> sessionStateService,
            IDocumentService<SharedContentItemModel> sharedContentItemDocumentService,
            CmsApiClientOptions cmsApiClientOptions,
            IYourAssessmentsService yourAssessmentsService)
        :base(logger, sessionStateService)
        {
            this.logger = logger;
            this.sharedContentItemDocumentService = sharedContentItemDocumentService;
            this.cmsApiClientOptions = cmsApiClientOptions;
            this.yourAssessmentsService = yourAssessmentsService;
        }

        [HttpGet]
        [Route("skills-health-check/your-assessments/document")]
        [Route("skills-health-check/your-assessments")]
        public async Task<IActionResult> Document()
        {
            var htmlHeadViewModel = GetHtmlHeadViewModel(PageTitle);
            var breadcrumbViewModel = BuildBreadcrumb();
            var bodyViewModel = await GetBodyViewModel();

            return this.NegotiateContentResult(new DocumentViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                BodyViewModel = bodyViewModel,
            });
        }

        [HttpGet]
        [Route("skills-health-check/your-assessments/htmlhead")]
        public IActionResult HtmlHead()
        {
            var viewModel = GetHtmlHeadViewModel(PageTitle);

            logger.LogInformation($"{nameof(HtmlHead)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        [Route("skills-health-check/your-assessments/breadcrumb")]
        public IActionResult Breadcrumb()
        {
            var viewModel = BuildBreadcrumb();

            logger.LogInformation($"{nameof(Breadcrumb)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        [HttpGet]
        [Route("skills-health-check/your-assessments/body")]
        public async Task<IActionResult> Body()
        {
            var viewModel = await GetBodyViewModel();
            return this.NegotiateContentResult(viewModel);
        }

        private async Task<BodyViewModel> GetBodyViewModel()
        {
            var sessionDataModel = await GetSessionDataModel();
            long documentId = 0;
            if (sessionDataModel == null || sessionDataModel.DocumentId == 0)
            {
                Response.Redirect(HomeURL);
            }
            else
            {
                documentId = sessionDataModel.DocumentId;
            }


            SharedContentItemModel? speakToAnAdviser = null;
            if (!string.IsNullOrWhiteSpace(cmsApiClientOptions.ContentIds))
            {
                speakToAnAdviser = await sharedContentItemDocumentService
                    .GetByIdAsync(new Guid(cmsApiClientOptions.ContentIds));
            }

            var rightBarViewModel = new RightBarViewModel();
            if (speakToAnAdviser != null)
            {
                rightBarViewModel.SpeakToAnAdviser = speakToAnAdviser;
            }

            var bodyViewModel = yourAssessmentsService.GetAssessmentListViewModel(documentId);
            bodyViewModel.RightBarViewModel = rightBarViewModel;

            return bodyViewModel;
        }

        // TODO: all this below should be moved to a separate service once the SHC service layer has been implemented
        private static List<AssessmentOverview> GetAssessmentList()
        {
            return new List<AssessmentOverview>
                {
                    new AssessmentOverview
                    {
                        Action = Assessments.Skills.Action,
                        AssessmentName = Assessments.Skills.Title,
                        AssessmentCategory = Assessments.Skills.Category,
                        Description = Assessments.Skills.Description,
                        AssessmentDuration = Assessments.Skills.TimeToComplete,
                        AssessmentType = AssessmentType.SkillAreas,
                        PersonalAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = Assessments.Interests.Action,
                        AssessmentName = Assessments.Interests.Title,
                        AssessmentCategory = Assessments.Interests.Category,
                        Description = Assessments.Interests.Description,
                        AssessmentDuration = Assessments.Interests.TimeToComplete,
                        AssessmentType = AssessmentType.Interest,
                        PersonalAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = Assessments.Personal.Action,
                        AssessmentName = Assessments.Personal.Title,
                        AssessmentCategory = Assessments.Personal.Category,
                        Description = Assessments.Personal.Description,
                        AssessmentDuration = Assessments.Personal.TimeToComplete,
                        AssessmentType = AssessmentType.Personal,
                        PersonalAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = Assessments.Motivation.Action,
                        AssessmentName = Assessments.Motivation.Title,
                        AssessmentCategory = Assessments.Motivation.Category,
                        Description = Assessments.Motivation.Description,
                        AssessmentDuration = Assessments.Motivation.TimeToComplete,
                        AssessmentType = AssessmentType.Motivation,
                        PersonalAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = Assessments.Numeric.Action,
                        AssessmentName = Assessments.Numeric.Title,
                        AssessmentCategory = Assessments.Numeric.Category,
                        Description = Assessments.Numeric.Description,
                        AssessmentDuration = Assessments.Numeric.TimeToComplete,
                        AssessmentType = AssessmentType.Numeric,
                        ActivityAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = Assessments.Verbal.Action,
                        AssessmentName = Assessments.Verbal.Title,
                        AssessmentCategory = Assessments.Verbal.Category,
                        Description = Assessments.Verbal.Description,
                        AssessmentDuration = Assessments.Verbal.TimeToComplete,
                        AssessmentType = AssessmentType.Verbal,
                        ActivityAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = Assessments.Checking.Action,
                        AssessmentName = Assessments.Checking.Title,
                        AssessmentCategory = Assessments.Checking.Category,
                        Description = Assessments.Checking.Description,
                        AssessmentDuration = Assessments.Checking.TimeToComplete,
                        AssessmentType = AssessmentType.Checking,
                        ActivityAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = Assessments.Mechanical.Action,
                        AssessmentName = Assessments.Mechanical.Title,
                        AssessmentCategory = Assessments.Mechanical.Category,
                        Description = Assessments.Mechanical.Description,
                        AssessmentDuration = Assessments.Mechanical.TimeToComplete,
                        AssessmentType = AssessmentType.Mechanical,
                        ActivityAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = Assessments.Spatial.Action,
                        AssessmentName = Assessments.Spatial.Title,
                        AssessmentCategory = Assessments.Spatial.Category,
                        Description = Assessments.Spatial.Description,
                        AssessmentDuration = Assessments.Spatial.TimeToComplete,
                        AssessmentType = AssessmentType.Spatial,
                        ActivityAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = Assessments.Abstract.Action,
                        AssessmentName = Assessments.Abstract.Title,
                        AssessmentCategory = Assessments.Abstract.Category,
                        Description = Assessments.Abstract.Description,
                        AssessmentDuration = Assessments.Abstract.TimeToComplete,
                        AssessmentType = AssessmentType.Abstract,
                        ActivityAssessment = true,
                    },
                };
        }
    }
}
