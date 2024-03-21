using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.Filters;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Helpers;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
using DFC.App.SkillsHealthCheck.ViewModels;
using DFC.App.SkillsHealthCheck.ViewModels.Question;
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
    [ServiceFilter(typeof(SessionStateFilter))]
    public class QuestionController : BaseController<QuestionController>
    {
        public const string PageTitle = "Question";
        private const string SharedContentStaxId = "2c9da1b3-3529-4834-afc9-9cd741e59788";
        private readonly ILogger<QuestionController> logger;
        private readonly IQuestionService questionService;
        private readonly ISharedContentRedisInterface sharedContentRedis;
        private readonly IConfiguration configuration;
        private string status;

        public QuestionController(
            ILogger<QuestionController> logger,
            ISessionStateService<SessionDataModel> sessionStateService,
            IOptions<SessionStateOptions> sessionStateOptions,
            ISharedContentRedisInterface sharedContentRedis,
            IQuestionService questionService,
            IConfiguration configuration)
            : base(logger, sessionStateService, sessionStateOptions)
        {
            this.logger = logger;
            this.sharedContentRedis = sharedContentRedis;
            this.questionService = questionService;
            status = configuration.GetSection("contentMode:contentMode").Get<string>();
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
        [Route("skills-health-check/question/answer-question/htmlhead")]
        [Route("skills-health-check/question/answer-multiple-question/htmlhead")]
        [Route("skills-health-check/question/answer-elimination-question/htmlhead")]
        [Route("skills-health-check/question/answer-feedback-question/htmlhead")]
        [Route("skills-health-check/question/answer-checking-question/htmlhead")]
        public IActionResult HtmlHead(string assessmentType)
        {
            var title = Constants.SkillsHealthCheckQuestion.AssessmentTypeTitle.FirstOrDefault(t =>
                t.Key.Equals(assessmentType, StringComparison.InvariantCultureIgnoreCase)).Value;
            var viewModel = GetHtmlHeadViewModel(string.IsNullOrWhiteSpace(title) ? PageTitle : title);

            logger.LogInformation($"{nameof(HtmlHead)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        [Route("skills-health-check/question/breadcrumb")]
        [Route("skills-health-check/question/answer-question/breadcrumb")]
        [Route("skills-health-check/question/answer-multiple-question/breadcrumb")]
        [Route("skills-health-check/question/answer-elimination-question/breadcrumb")]
        [Route("skills-health-check/question/answer-feedback-question/breadcrumb")]
        [Route("skills-health-check/question/answer-checking-question/breadcrumb")]
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

        private static Accessibility GetDefaultAccessibility(AssessmentType assessmentType) =>
            assessmentType switch
            {
                AssessmentType.Numerical => Accessibility.Accessible,
                _ => Accessibility.Full,
            };

        private async Task<BodyViewModel> GetBodyViewModel(string assessmentType)
        {
            var assessmentQuestionViewModel = await GetAssessmentQuestionViewModel(assessmentType);
            Enum.TryParse(assessmentType, out AssessmentType assessmentTypeEnum);


            return new BodyViewModel
            {
                AssessmentQuestionViewModel = assessmentQuestionViewModel,
                RightBarViewModel = await GetRightBarViewModel(assessmentTypeEnum),
            };
        }

        private async Task<RightBarViewModel> GetRightBarViewModel(AssessmentType assessmentType)
        {
            if (string.IsNullOrEmpty(status))
            {
                status = "PUBLISHED";
            }

            var speakToAnAdviser = await sharedContentRedis.GetDataAsync<SharedHtml>(AppConstants.SpeakToAnAdviserSharedContent, status);

            var rightBarViewModel = new RightBarViewModel
            {
                AssessmentType = assessmentType.ToString(),
            };

            if (speakToAnAdviser != null)
            {
                rightBarViewModel.SpeakToAnAdviser = speakToAnAdviser.Html;
            }

            return rightBarViewModel;
        }

        private async Task<AssessmentQuestionViewModel> GetAssessmentQuestionViewModel(string assessmentType)
        {
            var sessionDataModel = await GetSessionDataModel();
            long documentId = sessionDataModel.DocumentId;
            var documentResponse = await questionService.GetSkillsDocument((int)documentId);

            if (documentResponse == null)
            {
                // TODO: probably an error response on the question page would be better but this is how current system handles this
                Response.Redirect(HomeURL);
            }

            var qnAssessmentType = FromSet<AssessmentType>.Get(assessmentType, AssessmentType.SkillAreas);
            var accessibility = GetDefaultAccessibility(qnAssessmentType);

            if (documentResponse.DataValueKeys.ContainsKey(qnAssessmentType + ".Complete") && documentResponse.DataValueKeys[qnAssessmentType + ".Complete"] == bool.TrueString)
            {
                // TODO: probably an error response on the question page would be better but this is how current system handles this
                Response.Redirect(HomeURL);
            }

            var assessmentQuestionOverview = await questionService.GetAssessmentQuestionsOverview(sessionDataModel, qnAssessmentType, documentResponse);

            await SetSessionStateAsync(sessionDataModel);

            return await GetAssessmentQuestionViewModel(qnAssessmentType, documentResponse, assessmentQuestionOverview);
        }

        private async Task<AssessmentQuestionViewModel> GetAssessmentQuestionViewModel(AssessmentType assessmentType, DFC.SkillsCentral.Api.Domain.Models.SkillsDocument skillsDocument, AssessmentQuestionsOverView assessmentQuestionsOverView)
        {
            var answerVm = await questionService.GetAssessmentQuestionViewModel(assessmentType, skillsDocument, assessmentQuestionsOverView);

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
                    feedBackQuestion.ViewName = "_AqFeedBackQuestion";
                    break;
                default:
                    answerVm.ViewName = "_AqSimpleRadio";
                    break;
            }

            return answerVm;
        }

        private async Task<IActionResult> ReturnErrorPostback(SessionDataModel sessionDataModel, AssessmentType assessmentType)
        {
            ViewData["QuestionAnswerError"] = true;

            var assessmentQuestionOverview = sessionDataModel.AssessmentQuestionsOverViews[string.Format(Constants.SkillsHealthCheck.AssessmentQuestionOverviewId, assessmentType)];
            var documentResponse = await questionService.GetSkillsDocument((int)sessionDataModel.DocumentId);
            var assessmentQuestionViewModel = await GetAssessmentQuestionViewModel(assessmentType, documentResponse, assessmentQuestionOverview);

            var bodyViewModel = new BodyViewModel
            {
                AssessmentQuestionViewModel = assessmentQuestionViewModel,
                RightBarViewModel = await GetRightBarViewModel(assessmentType),
            };

            if (Request.Path.Value.Contains("body"))
            {
                return this.NegotiateContentResult(bodyViewModel);
            }

            var title = Constants.SkillsHealthCheckQuestion.AssessmentTypeTitle.FirstOrDefault(t =>
                t.Key.Equals(assessmentType.ToString(), StringComparison.InvariantCultureIgnoreCase)).Value;
            var htmlHeadViewModel = GetHtmlHeadViewModel(string.IsNullOrWhiteSpace(title) ? PageTitle : title);
            var breadcrumbViewModel = BuildBreadcrumb();

            return this.NegotiateContentResult(new DocumentViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                BodyViewModel = bodyViewModel,
            });
        }

        [HttpPost]
        [Route("skills-health-check/question/answer-question")]
        [Route("skills-health-check/question/answer-question/body")]
        public async Task<IActionResult> AnswerQuestion([FromForm] AssessmentQuestionViewModel model)
        {
            var sessionDataModel = await GetSessionDataModel();
            if (ModelState.IsValid)
            {
                var saveAnswerResponse = await questionService.SubmitAnswer(sessionDataModel!, model);
                if (saveAnswerResponse != null)
                {
                    await SetSessionStateAsync(sessionDataModel);

                    return RedirectToNextAction(model);
                }

                return Redirect($"{QuestionURL}?assessmentType={model.AssessmentType}&saveerror=Could not retrieve skills document");
            }

            return await ReturnErrorPostback(sessionDataModel, (AssessmentType)model.AssessmentType);
        }

        [HttpPost]
        [Route("skills-health-check/question/answer-multiple-question")]
        [Route("skills-health-check/question/answer-multiple-question/body")]
        public async Task<IActionResult> AnswerMultipleQuestion([FromForm] MultipleAnswerQuestionViewModel model)
        {
            var sessionDataModel = await GetSessionDataModel();
            if (ModelState.IsValid)
            {
                var saveAnswerResponse = await questionService.SubmitAnswer(sessionDataModel!, model);
                if (saveAnswerResponse != null)
                {
                    await SetSessionStateAsync(sessionDataModel);

                    return RedirectToNextAction(model);
                }

                return Redirect($"{QuestionURL}?assessmentType={model.AssessmentType}&saveerror=Could not retrieve skills document");
            }

            return await ReturnErrorPostback(sessionDataModel, (AssessmentType)model.AssessmentType);
        }

        [HttpPost]
        [Route("skills-health-check/question/answer-elimination-question")]
        [Route("skills-health-check/question/answer-elimination-question/body")]
        public async Task<IActionResult> AnswerEliminationQuestion([FromForm] EliminationAnswerQuestionViewModel model)
        {
            var sessionDataModel = await GetSessionDataModel();
            if (ModelState.IsValid)
            {
                var saveAnswerResponse = await questionService.SubmitAnswer(sessionDataModel!, model);
                if (saveAnswerResponse != null)
                {
                    await SetSessionStateAsync(sessionDataModel);
                    return RedirectToNextAction(model);
                }

                return Redirect($"{QuestionURL}?assessmentType={model.AssessmentType}&saveerror=Could not retrieve skills document");
            }

            return await ReturnErrorPostback(sessionDataModel, (AssessmentType)model.AssessmentType);
        }

        [HttpPost]
        [Route("skills-health-check/question/answer-feedback-question")]
        [Route("skills-health-check/question/answer-feedback-question/body")]
        public async Task<IActionResult> AnswerFeedbackQuestion([FromForm] FeedBackQuestionViewModel model)
        {
            var sessionDataModel = await GetSessionDataModel();
            if (ModelState.IsValid)
            {
                var saveAnswerResponse = await questionService.SubmitAnswer(sessionDataModel!, model);
                if (saveAnswerResponse != null)
                {
                    return RedirectToNextAction(model);
                }

                return Redirect($"{QuestionURL}?assessmentType={model.FeedbackQuestion.AssessmentType}&saveerror=Could not retrieve skills document");
            }

            return await ReturnErrorPostback(sessionDataModel, model.FeedbackQuestion.AssessmentType);
        }

        [HttpPost]
        [Route("skills-health-check/question/answer-checking-question")]
        [Route("skills-health-check/question/answer-checking-question/body")]
        public async Task<IActionResult> AnswerCheckingQuestion([FromForm] TabularAnswerQuestionViewModel model)
        {
            var sessionDataModel = await GetSessionDataModel();
            CheckingQuestionValidation(model);

            if (ModelState.IsValid)
            {
                var saveAnswerResponse = await questionService.SubmitAnswer(sessionDataModel!, model);
                if (saveAnswerResponse != null)
                {
                    await SetSessionStateAsync(sessionDataModel);
                    return RedirectToNextAction(model);
                }

                return Redirect($"{QuestionURL}?assessmentType={model.AssessmentType}&saveerror=Could not retrieve skills document");
            }

            return await ReturnErrorPostback(sessionDataModel, (AssessmentType)model.AssessmentType);
        }

        private IActionResult RedirectToNextAction(AssessmentQuestionViewModel model)
        {
            if (model.QuestionNumber != model.ActualTotalQuestions)
            {
                var assessmenttype = model is FeedBackQuestionViewModel ? ((FeedBackQuestionViewModel)model).FeedbackQuestion.AssessmentType : model.AssessmentType;
                return Redirect($"{QuestionURL}?assessmentType={assessmenttype}");
            }

            return Redirect($"{YourAssessmentsURL}");
        }

        private void CheckingQuestionValidation(TabularAnswerQuestionViewModel model)
        {
            if (model.AnswerSelection == null || !model.AnswerSelection.Any())
            {
                ModelState.AddModelError(nameof(model.AnswerSelection), "Choose an answer");
            }

            // Shell breaks the model binding so need to fix here
            else if (model.AnswerSelection.Count() == 1 && model.AnswerSelection.First().Contains(","))
            {
                model.AnswerSelection = model.AnswerSelection.First().Split(',');
            }

            if (model.AnswerSelection != null && model.AnswerSelection.Count() > 1 && model.AnswerSelection.Any(val => val.Equals("E", StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError(nameof(model.AnswerSelection), "When No error is selected no other answer can be chosen");
            }
        }
    }
}
