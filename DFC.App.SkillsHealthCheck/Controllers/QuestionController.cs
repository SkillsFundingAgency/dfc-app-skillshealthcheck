using DFC.App.SkillsHealthCheck.Data.Models.ContentModels;
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
using DFC.Compui.Cosmos.Contracts;
using DFC.Compui.Sessionstate;
using DFC.Content.Pkg.Netcore.Data.Models.ClientOptions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    [TypeFilter(typeof(SessionStateFilter))]
    public class QuestionController : BaseController<QuestionController>
    {
        public const string PageTitle = "Question";

        private readonly ILogger<QuestionController> logger;
        private readonly IDocumentService<SharedContentItemModel> sharedContentItemDocumentService;
        private readonly CmsApiClientOptions cmsApiClientOptions;
        private readonly IQuestionService _questionService;

        public QuestionController(
            ILogger<QuestionController> logger,
            ISessionStateService<SessionDataModel> sessionStateService,
            IOptions<SessionStateOptions> sessionStateOptions,
            IDocumentService<SharedContentItemModel> sharedContentItemDocumentService,
            CmsApiClientOptions cmsApiClientOptions,
            IQuestionService questionService)
            : base(logger, sessionStateService, sessionStateOptions)
        {
            this.logger = logger;
            this.sharedContentItemDocumentService = sharedContentItemDocumentService;
            this.cmsApiClientOptions = cmsApiClientOptions;
            _questionService = questionService;
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

        private async Task<BodyViewModel> GetBodyViewModel(string assessmentType, Level level = Level.Level1, Accessibility accessibility = Accessibility.Full)
        {
            var assessmentQuestionViewModel = await GetAssessmentQuestionViewModel(assessmentType);
            var assessmentTypeEnum = assessmentQuestionViewModel is FeedBackQuestionViewModel fqvm
                ? fqvm.FeedbackQuestion.AssessmentType
                : assessmentQuestionViewModel.Question.AssessmentType;

            return new BodyViewModel
            {
                AssessmentQuestionViewModel = assessmentQuestionViewModel,
                RightBarViewModel = await GetRightBarViewModel(assessmentTypeEnum),
            };
        }

        private async Task<RightBarViewModel> GetRightBarViewModel(AssessmentType assessmentType)
        {
            SharedContentItemModel? speakToAnAdviser = null;
            if (!string.IsNullOrWhiteSpace(cmsApiClientOptions.ContentIds))
            {
                speakToAnAdviser = await sharedContentItemDocumentService
                    .GetByIdAsync(new Guid(cmsApiClientOptions.ContentIds));
            }

            var rightBarViewModel = new RightBarViewModel
            {
                AssessmentType = assessmentType.ToString(),
            };

            if (speakToAnAdviser != null)
            {
                rightBarViewModel.SpeakToAnAdviser = speakToAnAdviser;
            }

            return rightBarViewModel;
        }

        private async Task<AssessmentQuestionViewModel> GetAssessmentQuestionViewModel(string assessmentType, Level level = Level.Level1, Accessibility accessibility = Accessibility.Full)
        {
            var sessionDataModel = await GetSessionDataModel();
            long documentId =  sessionDataModel.DocumentId;
            var documentResponse = _questionService.GetSkillsDocument(new GetSkillsDocumentRequest {DocumentId = documentId,});

            if (!documentResponse.Success)
            {
                // TODO: probably an error response on the question page would be better but this is how current system handles this
                Response.Redirect(HomeURL);
            }

            var qnAssessmentType = FromSet<AssessmentType>.Get(assessmentType, AssessmentType.SkillAreas);

            if (!documentResponse.SkillsDocument.AssessmentNotCompleted(qnAssessmentType))
            {
                // TODO: probably an error response on the question page would be better but this is how current system handles this
                Response.Redirect(HomeURL);
            }

            var assessmentQuestionOverview = _questionService.GetAssessmentQuestionsOverview(sessionDataModel, level, accessibility, qnAssessmentType, documentResponse.SkillsDocument);

            await SetSessionStateAsync(sessionDataModel);

            return GetAssessmentQuestionViewModel(level, accessibility, qnAssessmentType, documentResponse.SkillsDocument, assessmentQuestionOverview);
        }

        private AssessmentQuestionViewModel GetAssessmentQuestionViewModel(Level level, Accessibility accessibility, AssessmentType assessmentType, SkillsDocument skillsDocument, AssessmentQuestionsOverView assessmentQuestionsOverView)
        {
            var answerVm = _questionService.GetAssessmentQuestionViewModel(level, accessibility, assessmentType, skillsDocument, assessmentQuestionsOverView);

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

        private async Task<IActionResult> ReturnErrorPostback(SessionDataModel sessionDataModel, Level level, Accessibility accessibility, AssessmentType assessmentType)
        {
            ViewData["QuestionAnswerError"] = true;

            var assessmentQuestionOverview = sessionDataModel.AssessmentQuestionsOverViews[string.Format(Constants.SkillsHealthCheck.AssessmentQuestionOverviewId, assessmentType)];
            var documentResponse = _questionService.GetSkillsDocument(new GetSkillsDocumentRequest { DocumentId = sessionDataModel.DocumentId, });
            var assessmentQuestionViewModel = GetAssessmentQuestionViewModel(level, accessibility, assessmentType, documentResponse.SkillsDocument, assessmentQuestionOverview);

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
                t.Key.Equals(assessmentQuestionViewModel.Question.AssessmentType.ToString(), StringComparison.InvariantCultureIgnoreCase)).Value;
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
        public async Task<IActionResult> AnswerQuestion(AssessmentQuestionViewModel model)
        {
            var sessionDataModel = await GetSessionDataModel();
            if (ModelState.IsValid)
            {
                var saveAnswerResponse = await _questionService.SubmitAnswer(sessionDataModel!, model);
                if (saveAnswerResponse.Success)
                {
                    await SetSessionStateAsync(sessionDataModel);

                    return RedirectToNextAction(model);
                }

                return Redirect($"{QuestionURL}?assessmentType={model.Question.AssessmentType}&saveerror={saveAnswerResponse.ErrorMessage}");
            }

            return await ReturnErrorPostback(sessionDataModel, model.Question.Level, model.Question.Accessibility, model.Question.AssessmentType);
        }

        [HttpPost]
        [Route("skills-health-check/question/answer-multiple-question")]
        [Route("skills-health-check/question/answer-multiple-question/body")]
        public async Task<IActionResult> AnswerMultipleQuestion(MultipleAnswerQuestionViewModel model)
        {
            var sessionDataModel = await GetSessionDataModel();
            if (ModelState.IsValid)
            {
                var saveAnswerResponse = await _questionService.SubmitAnswer(sessionDataModel!, model);
                if (saveAnswerResponse.Success)
                {
                    await SetSessionStateAsync(sessionDataModel);

                    return RedirectToNextAction(model);
                }

                return Redirect($"{QuestionURL}?assessmentType={model.Question.AssessmentType}&saveerror={saveAnswerResponse.ErrorMessage}");
            }

            return await ReturnErrorPostback(sessionDataModel, model.Question.Level, model.Question.Accessibility, model.Question.AssessmentType);
        }

        [HttpPost]
        [Route("skills-health-check/question/answer-elimination-question")]
        [Route("skills-health-check/question/answer-elimination-question/body")]
        public async Task<IActionResult> AnswerEliminationQuestion(EliminationAnswerQuestionViewModel model)
        {
            var sessionDataModel = await GetSessionDataModel();
            if (ModelState.IsValid)
            {
                var saveAnswerResponse = await _questionService.SubmitAnswer(sessionDataModel!, model);
                if (saveAnswerResponse.Success)
                {
                    await SetSessionStateAsync(sessionDataModel);
                    return RedirectToNextAction(model);
                }

                return Redirect($"{QuestionURL}?assessmentType={model.Question.AssessmentType}&saveerror={saveAnswerResponse.ErrorMessage}");
            }

            return await ReturnErrorPostback(sessionDataModel, model.Question.Level, model.Question.Accessibility, model.Question.AssessmentType);
        }

        [HttpPost]
        [Route("skills-health-check/question/answer-feedback-question")]
        [Route("skills-health-check/question/answer-feedback-question/body")]
        public async Task<IActionResult> AnswerFeedbackQuestion(FeedBackQuestionViewModel model)
        {
            var sessionDataModel = await GetSessionDataModel();
            if (ModelState.IsValid)
            {
                var saveAnswerResponse = await _questionService.SubmitAnswer(sessionDataModel!, model);
                if (saveAnswerResponse.Success)
                {
                    return RedirectToNextAction(model);
                }

                return Redirect($"{QuestionURL}?assessmentType={model.FeedbackQuestion.AssessmentType}&saveerror={saveAnswerResponse.ErrorMessage}");
            }

            return await ReturnErrorPostback(sessionDataModel, model.FeedbackQuestion.Level, model.FeedbackQuestion.Accessibility, model.FeedbackQuestion.AssessmentType);
        }

        [HttpPost]
        [Route("skills-health-check/question/answer-checking-question")]
        [Route("skills-health-check/question/answer-checking-question/body")]
        public async Task<IActionResult> AnswerCheckingQuestion(TabularAnswerQuestionViewModel model)
        {
            var sessionDataModel = await GetSessionDataModel();
            CheckingQuestionValidation(model);

            if (ModelState.IsValid)
            {
                var saveAnswerResponse = await _questionService.SubmitAnswer(sessionDataModel!, model);
                if (saveAnswerResponse.Success)
                {
                    await SetSessionStateAsync(sessionDataModel);
                    return RedirectToNextAction(model);
                }

                return Redirect($"{QuestionURL}?assessmentType={model.Question.AssessmentType}&saveerror={saveAnswerResponse.ErrorMessage}");
            }

            return await ReturnErrorPostback(sessionDataModel, model.Question.Level, model.Question.Accessibility, model.Question.AssessmentType);
        }

        private IActionResult RedirectToNextAction(AssessmentQuestionViewModel model)
        {
            if (model.QuestionNumber != model.ActualTotalQuestions)
            {
                var assessmenttype = model is FeedBackQuestionViewModel ? ((FeedBackQuestionViewModel) model).FeedbackQuestion.AssessmentType : model.Question.AssessmentType;
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
