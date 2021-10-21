using System;
using System.Collections.Generic;
using System.Linq;
using DFC.App.SkillsHealthCheck.Data.Models.ContentModels;
using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.Factories;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Helpers;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
using DFC.App.SkillsHealthCheck.ViewModels.Question;
using DFC.Compui.Cosmos.Contracts;
using DFC.Compui.Sessionstate;
using DFC.Content.Pkg.Netcore.Data.Models.ClientOptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DFC.App.SkillsHealthCheck.ViewModels;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    public class QuestionController : BaseController<QuestionController>
    {
        public const string PageTitle = "Question";

        private readonly ILogger<QuestionController> logger;
        private readonly IDocumentService<SharedContentItemModel> sharedContentItemDocumentService;
        private readonly CmsApiClientOptions cmsApiClientOptions;
        private readonly ISkillsHealthCheckService skillsHealthCheckService;
        private readonly SkillsHealthCheckViewModelFactory _skillsHealthCheckViewModelFactory;

        public QuestionController(
            ILogger<QuestionController> logger,
            ISessionStateService<SessionDataModel> sessionStateService,
            IDocumentService<SharedContentItemModel> sharedContentItemDocumentService,
            CmsApiClientOptions cmsApiClientOptions,
            ISkillsHealthCheckService skillsHealthCheckService)
            : base(logger, sessionStateService)
        {
            this.logger = logger;
            this.sharedContentItemDocumentService = sharedContentItemDocumentService;
            this.cmsApiClientOptions = cmsApiClientOptions;
            this.skillsHealthCheckService = skillsHealthCheckService;
            _skillsHealthCheckViewModelFactory = new SkillsHealthCheckViewModelFactory(skillsHealthCheckService);
        }

        [HttpGet]
        [Route("skills-health-check/question/document")]
        [Route("skills-health-check/question")]
        public async Task<IActionResult> Document(string assessmentType)
        {
            var title = Constants.SkillsHealthCheckQuestion.AssessmentTypeTitle.FirstOrDefault(t =>
                t.Key.Equals(assessmentType, StringComparison.InvariantCultureIgnoreCase)).Value;
            var htmlHeadViewModel = GetHtmlHeadViewModel(string.IsNullOrWhiteSpace(title) ? PageTitle : title);
            var breadcrumbViewModel = BuildBreadcrumb();
            var bodyViewModel = await GetBodyViewModel(assessmentType);

            return this.NegotiateContentResult(new DocumentViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                BodyViewModel = bodyViewModel,
            });
        }

        [HttpGet]
        [Route("skills-health-check/question/htmlhead")]
        public IActionResult HtmlHead(string assessmentType)
        {
            var title = Constants.SkillsHealthCheckQuestion.AssessmentTypeTitle.FirstOrDefault(t =>
                t.Key.Equals(assessmentType, StringComparison.InvariantCultureIgnoreCase)).Value;
            var viewModel = GetHtmlHeadViewModel(string.IsNullOrWhiteSpace(title) ? PageTitle : title);

            logger.LogInformation($"{nameof(HtmlHead)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        [Route("skills-health-check/question/breadcrumb")]
        public IActionResult Breadcrumb()
        {
            var viewModel = BuildBreadcrumb();

            logger.LogInformation($"{nameof(Breadcrumb)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        [HttpGet]
        [Route("skills-health-check/question/body")]
        public async Task<IActionResult> Body(string assessmentType)
        {
            var viewModel = await GetBodyViewModel(assessmentType);
            return this.NegotiateContentResult(viewModel);
        }

        private async Task<BodyViewModel> GetBodyViewModel(string assessmentType, Level level = Level.Level1, Accessibility accessibility = Accessibility.Full)
        {
            var assessmentQuestionViewModel = await GetAssessmentQuestionViewModel(assessmentType);

            SharedContentItemModel? speakToAnAdviser = null;
            if (!string.IsNullOrWhiteSpace(cmsApiClientOptions.ContentIds))
            {
                speakToAnAdviser = await sharedContentItemDocumentService
                    .GetByIdAsync(new Guid(cmsApiClientOptions.ContentIds));
            }

            var rightBarViewModel = new RightBarViewModel
            {
                AssessmentType = assessmentQuestionViewModel is FeedBackQuestionViewModel ? ((FeedBackQuestionViewModel)assessmentQuestionViewModel).FeedbackQuestion.AssessmentType.ToString() : assessmentQuestionViewModel.Question.AssessmentType.ToString(),
            };
            if (speakToAnAdviser != null)
            {
                rightBarViewModel.SpeakToAnAdviser = speakToAnAdviser;
            }


            return new BodyViewModel
            {
                AssessmentQuestionViewModel = assessmentQuestionViewModel,
                RightBarViewModel = rightBarViewModel,
            };
        }

        private async Task<AssessmentQuestionViewModel> GetAssessmentQuestionViewModel(string assessmentType, Level level = Level.Level1, Accessibility accessibility = Accessibility.Full)
        {
            var sessionDataModel = await GetSessionDataModel();
            if (sessionDataModel == null || sessionDataModel.DocumentId == 0)
            {
                Response.Redirect(HomeURL);
            }

            var documentResponse =
                skillsHealthCheckService.GetSkillsDocument(new GetSkillsDocumentRequest {DocumentId = sessionDataModel.DocumentId, });

            if (!documentResponse.Success)
            {
                // TODO: probably an error response on the question page would be better but this is how current system handles this
                Response.Redirect(HomeURL);
            }

            var qnAssessmentType = FromSet<AssessmentType>.Get(assessmentType, AssessmentType.SkillAreas);

            var assessmentToBeCompleted = documentResponse.SkillsDocument.AssessmentNotCompleted(qnAssessmentType);

            if (!assessmentToBeCompleted)
            {
                // TODO: probably an error response on the question page would be better but this is how current system handles this
                Response.Redirect(HomeURL);
            }

            var assessmentQuestionOverview = await GetAssessmentQuestionsOverView(sessionDataModel, qnAssessmentType, level, accessibility, documentResponse.SkillsDocument);

            var answerVm = _skillsHealthCheckViewModelFactory.GetAssessmentQuestionViewModel(sessionDataModel.DocumentId, level, accessibility, qnAssessmentType, documentResponse.SkillsDocument, assessmentQuestionOverview);

            switch (answerVm)
            {
                case EliminationAnswerQuestionViewModel eliminationAnswer:
                    eliminationAnswer.ViewName = "_AqHeadingRadio";
                    break;
                case MultipleAnswerQuestionViewModel multipleAnswer:
                    multipleAnswer.ViewName = "_AqSimpleHeadingRadio";
                    break;
                case TabularAnswerQuestionViewModel tabularAnswer:
                    tabularAnswer.ViewName = "_AqChecking";
                    break;
                case FeedBackQuestionViewModel feedBackQuestion:
                    feedBackQuestion.ViewName = "_FeedBackQuestion";
                    break;
                default:
                    answerVm.ViewName = "_AqSimpleRadio";
                    break;
            }

            return answerVm;
        }

        private async Task<AssessmentQuestionsOverView> GetAssessmentQuestionsOverView(SessionDataModel sessionDataModel, AssessmentType assessmentType, Level level, Accessibility accessibility, SkillsDocument skillsDocument)
        {
            var overviewSessionId = string.Format(Constants.SkillsHealthCheck.AssessmentQuestionOverviewId, assessmentType);
            sessionDataModel.AssessmentQuestionsOverViews ??= new Dictionary<string, AssessmentQuestionsOverView>();

            var assessmentQuestionOverview = sessionDataModel.AssessmentQuestionsOverViews.ContainsKey(overviewSessionId) ? sessionDataModel.AssessmentQuestionsOverViews[overviewSessionId] : new AssessmentQuestionsOverView();
            if (assessmentQuestionOverview.ActualQuestionsNumber == 0)
            {
                assessmentQuestionOverview = _skillsHealthCheckViewModelFactory.GetAssessmentQuestionsOverview(level, accessibility, assessmentType, skillsDocument);
                if (sessionDataModel.AssessmentQuestionsOverViews.ContainsKey(overviewSessionId))
                {
                    sessionDataModel.AssessmentQuestionsOverViews[overviewSessionId] = assessmentQuestionOverview;
                }
                else
                {
                    sessionDataModel.AssessmentQuestionsOverViews.Add(overviewSessionId, assessmentQuestionOverview);
                }

                await SetSessionStateAsync(sessionDataModel);
            }

            return assessmentQuestionOverview;
        }
    }
}
