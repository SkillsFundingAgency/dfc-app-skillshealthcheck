using DFC.App.SkillsHealthCheck.Data.Models.ContentModels;
using DFC.App.SkillsHealthCheck.Enums;
using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.ViewModels;
using DFC.App.SkillsHealthCheck.ViewModels.YourAssessments;
using DFC.Compui.Cosmos.Contracts;
using DFC.Content.Pkg.Netcore.Data.Models.ClientOptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    public class YourAssessmentsController : BaseController
    {
        public const string PageTitle = "Home";
        public const string PagePart = "your-assessments";
        private readonly ILogger<SkillsHealthCheckController> logger;
        private readonly IDocumentService<SharedContentItemModel> sharedContentItemDocumentService;
        private readonly CmsApiClientOptions cmsApiClientOptions;

        public YourAssessmentsController(
            ILogger<SkillsHealthCheckController> logger,
            IDocumentService<SharedContentItemModel> sharedContentItemDocumentService,
            CmsApiClientOptions cmsApiClientOptions)
        {
            this.logger = logger;
            this.sharedContentItemDocumentService = sharedContentItemDocumentService;
            this.cmsApiClientOptions = cmsApiClientOptions;
        }

        [HttpGet]
        [Route("skills-health-check/your-assessments/document")]
        [Route("skills-health-check/your-assessments/")]
        public async Task<IActionResult> Document()
        {
            var htmlHeadViewModel = GetHtmlHeadViewModel(PageTitle);
            var breadcrumbViewModel = BuildBreadcrumb();
            var bodyViewModel = await GetHomeBodyViewModel().ConfigureAwait(false);

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
            var viewModel = await GetHomeBodyViewModel().ConfigureAwait(false);
            return this.NegotiateContentResult(viewModel);
        }

        private async Task<BodyViewModel> GetHomeBodyViewModel()
        {
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

            var assessments = GetAssessmentList();
            return new BodyViewModel
            {
                DateAssessmentsCreated = DateTime.Now,
                RightBarViewModel = rightBarViewModel,
                AssessmentsActivity = assessments.Where(assess => assess.ActivityAssessment).ToList(),
                AssessmentsPersonal = assessments.Where(assess => assess.PersonalAssessment).ToList(),
                AssessmentsCompleted = new List<AssessmentOverview>(),
            };
        }

        // TODO: all this below should be moved to a separate service once the SHC service layer has been implemented

        /// <summary>
        /// The _skills assessment status
        /// </summary>
        public string _skillsAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetNotStartedAction;

        /// <summary>
        /// The _personal assessment status
        /// </summary>
        private string _personalAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetNotStartedAction;

        /// <summary>
        /// The _motivation assessment status
        /// </summary>
        private string _motivationAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetNotStartedAction;

        /// <summary>
        /// The _numweic assesment status
        /// </summary>
        private string _numericAssesmentStatus = Constants.SkillsHealthCheck.QuestionSetNotStartedAction;

        /// <summary>
        /// The _verbal assessment status
        /// </summary>
        private string _verbalAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetNotStartedAction;

        /// <summary>
        /// The _check activity assessment status
        /// </summary>
        private string _checkActivityAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetNotStartedAction;

        /// <summary>
        /// The _mechanical assessment status
        /// </summary>
        private string _mechanicalAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetNotStartedAction;

        /// <summary>
        /// The _interests assessment status
        /// </summary>
        private string _interestsAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetNotStartedAction;

        /// <summary>
        /// The _spatial activity assessment status
        /// </summary>
        private string _spatialActivityAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetNotStartedAction;

        /// <summary>
        /// The _abstract activity assessment status
        /// </summary>
        private string _abstractActivityAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetNotStartedAction;

        private List<AssessmentOverview> GetAssessmentList()
        {
            return new List<AssessmentOverview>
                {
                    new AssessmentOverview
                    {
                        Action = _skillsAssessmentStatus,
                        AssessmentName = Constants.SkillsHealthCheck.SkillsAssessmentTitle,
                        AssessmentCategory = Constants.SkillsHealthCheck.SkillsAssessmentCategory,
                        Description = Constants.SkillsHealthCheck.SkillsAssessmentDescription,
                        AssessmentDuration = Constants.SkillsHealthCheck.SkillsAssessmentTimeToComplete,
                        AssessmentType = AssessmentType.SkillAreas,
                        PersonalAssessment = true
                    },
                    new AssessmentOverview
                    {
                        Action = _interestsAssessmentStatus,
                        AssessmentName = Constants.SkillsHealthCheck.InterestsAssessmentTitle,
                        AssessmentCategory = Constants.SkillsHealthCheck.InterestsAssessmentCategory,
                        Description = Constants.SkillsHealthCheck.InterestsAssessmentDescription,
                        AssessmentDuration = Constants.SkillsHealthCheck.InterestsAssessmentTimeToComplete,
                        AssessmentType = AssessmentType.Interest,
                        PersonalAssessment = true
                    },
                    new AssessmentOverview
                    {
                        Action = _personalAssessmentStatus,
                        AssessmentName = Constants.SkillsHealthCheck.PersonalAssessmentTitle,
                        AssessmentCategory = Constants.SkillsHealthCheck.PersonalAssessmentCategory,
                        Description = Constants.SkillsHealthCheck.PersonalAssessmentDescription,
                        AssessmentDuration = Constants.SkillsHealthCheck.PersonalAssessmentTimeToComplete,
                        AssessmentType = AssessmentType.Personal,
                        PersonalAssessment = true
                    },
                    new AssessmentOverview
                    {
                        Action = _motivationAssessmentStatus,
                        AssessmentName = Constants.SkillsHealthCheck.MotivationAssessmentTitle,
                        AssessmentCategory = Constants.SkillsHealthCheck.MotivatioAssessmentCategory,
                        Description = Constants.SkillsHealthCheck.MotivationAssessmentDescription,
                        AssessmentDuration = Constants.SkillsHealthCheck.MotivationAssessmentTimeToComplete,
                        AssessmentType = AssessmentType.Motivation,
                        PersonalAssessment = true
                    },
                    new AssessmentOverview
                    {
                        Action = _numericAssesmentStatus,
                        AssessmentName = Constants.SkillsHealthCheck.NumericAssessmentTitle,
                        AssessmentCategory = Constants.SkillsHealthCheck.NumericAssessmentCategory,
                        Description = Constants.SkillsHealthCheck.NumericAssessmentDescription,
                        AssessmentDuration = Constants.SkillsHealthCheck.NumericAssessmentTimeToComplete,
                        AssessmentType = AssessmentType.Numeric,
                        ActivityAssessment = true
                    },
                    new AssessmentOverview
                    {
                        Action = _verbalAssessmentStatus,
                        AssessmentName = Constants.SkillsHealthCheck.VerbalAssessmentTitle,
                        AssessmentCategory = Constants.SkillsHealthCheck.VerbalAssessmentCategory,
                        Description = Constants.SkillsHealthCheck.VerbalAssessmentDescription,
                        AssessmentDuration = Constants.SkillsHealthCheck.VerbalAssessmentTimeToComplete,
                        AssessmentType = AssessmentType.Verbal,
                        ActivityAssessment = true
                    },
                    new AssessmentOverview
                    {
                        Action = _checkActivityAssessmentStatus,
                        AssessmentName = Constants.SkillsHealthCheck.CheckingAssessmentTitle,
                        AssessmentCategory = Constants.SkillsHealthCheck.CheckinAssessmentCategory,
                        Description = Constants.SkillsHealthCheck.CheckingAssessmentDescription,
                        AssessmentDuration = Constants.SkillsHealthCheck.CheckingAssessmentTimeToComplete,
                        AssessmentType = AssessmentType.Checking,
                        ActivityAssessment = true
                    },
                    new AssessmentOverview
                    {
                        Action = _mechanicalAssessmentStatus,
                        AssessmentName = Constants.SkillsHealthCheck.MechanicalAssessmentTitle,
                        AssessmentCategory = Constants.SkillsHealthCheck.MechanicalAssessmentCategory,
                        Description = Constants.SkillsHealthCheck.MechanicalAssessmentDescription,
                        AssessmentDuration = Constants.SkillsHealthCheck.MechanicalAssessmentTimeToComplete,
                        AssessmentType = AssessmentType.Mechanical,
                        ActivityAssessment = true
                    },
                    new AssessmentOverview
                    {
                        Action = _spatialActivityAssessmentStatus,
                        AssessmentName = Constants.SkillsHealthCheck.SpatialAssessmentTitle,
                        AssessmentCategory = Constants.SkillsHealthCheck.SpatialAssessmentCategory,
                        Description = Constants.SkillsHealthCheck.SpatialAssessmentDescription,
                        AssessmentDuration = Constants.SkillsHealthCheck.SpatialAssessmentTimeToComplete,
                        AssessmentType = AssessmentType.Spatial,
                        ActivityAssessment = true
                    },
                    new AssessmentOverview
                    {
                        Action = _abstractActivityAssessmentStatus,
                        AssessmentName = Constants.SkillsHealthCheck.AbstractAssessmentTitle,
                        AssessmentCategory = Constants.SkillsHealthCheck.AbstractAssessmentCategory,
                        Description = Constants.SkillsHealthCheck.AbstractAssessmentDescription,
                        AssessmentDuration = Constants.SkillsHealthCheck.AbstractAssessmentTimeToComplete,
                        AssessmentType = AssessmentType.Abstract,
                        ActivityAssessment = true
                    },
                };
        }
    }
}
