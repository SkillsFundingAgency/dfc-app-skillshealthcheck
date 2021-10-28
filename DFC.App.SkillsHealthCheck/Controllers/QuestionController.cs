using System;
using System.Linq;
using DFC.App.SkillsHealthCheck.Data.Models.ContentModels;
using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Helpers;
using DFC.App.SkillsHealthCheck.ViewModels.Question;
using DFC.Compui.Cosmos.Contracts;
using DFC.Compui.Sessionstate;
using DFC.Content.Pkg.Netcore.Data.Models.ClientOptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DFC.App.SkillsHealthCheck.Services.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.ViewModels;

namespace DFC.App.SkillsHealthCheck.Controllers
{
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
            IDocumentService<SharedContentItemModel> sharedContentItemDocumentService,
            CmsApiClientOptions cmsApiClientOptions,
            IQuestionService questionService)
            : base(logger, sessionStateService)
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
            long documentId = 0;
            if (sessionDataModel == null || sessionDataModel.DocumentId == 0)
            {
                Response.Redirect(HomeURL);
            }
            else
            {
                documentId = sessionDataModel.DocumentId;
            }

            var documentResponse = _questionService.GetSkillsDocument(new GetSkillsDocumentRequest {DocumentId = documentId,});

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

            var assessmentQuestionOverview = _questionService.GetAssessmentQuestionsOverview(sessionDataModel, level, accessibility, qnAssessmentType, documentResponse.SkillsDocument);
            await SetSessionStateAsync(sessionDataModel);

            var answerVm = _questionService.GetAssessmentQuestionViewModel(level, accessibility, qnAssessmentType, documentResponse.SkillsDocument, assessmentQuestionOverview);
            //answerVm.ValidationErrors = GetValidationErrors(); TODO: needs to be looked at

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

        [HttpPost]
        [Route("skills-health-check/question/answer-question")]
        [Route("skills-health-check/question/answer-question/body")]
        public async Task<IActionResult> AnswerQuestion(AssessmentQuestionViewModel model, string answerAction)
        {
            var sessionDataModel = await GetSessionDataModel();
            if (sessionDataModel == null || sessionDataModel.DocumentId == 0)
            {
                Response.Redirect(HomeURL);
            }

            if (ModelState.IsValid)
            {
                var saveAnswerResponse = await _questionService.SubmitAnswer(sessionDataModel!, model);
                if (saveAnswerResponse.Success)
                {
                    await SetSessionStateAsync(sessionDataModel);
                    //UpdateShcUsageDate();

                    return RedirectToNextAction(model);
                }

                return Redirect($"{QuestionURL}?assessmentType={model.Question.AssessmentType}&saveerror={saveAnswerResponse.ErrorMessage}");
            }

            ViewData["QuestionAnswerError"] = true;

            return Redirect($"{QuestionURL}?assessmentType={model.Question.AssessmentType}");
        }

        [HttpPost]
        [Route("skills-health-check/question/answer-multiple-question")]
        [Route("skills-health-check/question/answer-multiple-question/body")]
        public async Task<IActionResult> AnswerMultipleQuestion(MultipleAnswerQuestionViewModel model, string answerAction)
        {
            var sessionDataModel = await GetSessionDataModel();
            if (sessionDataModel == null || sessionDataModel.DocumentId == 0)
            {
                Response.Redirect(HomeURL);
            }

            if (ModelState.IsValid)
            {
                var saveAnswerResponse = await _questionService.SubmitAnswer(sessionDataModel!, model);
                if (saveAnswerResponse.Success)
                {
                    await SetSessionStateAsync(sessionDataModel);
                    //UpdateShcUsageDate();

                    return RedirectToNextAction(model);
                }

                return Redirect($"{QuestionURL}?assessmentType={model.Question.AssessmentType}&saveerror={saveAnswerResponse.ErrorMessage}");
            }

            ViewData["QuestionAnswerError"] = true;

            return Redirect($"{QuestionURL}?assessmentType={model.Question.AssessmentType}");
        }

        [HttpPost]
        [Route("skills-health-check/question/answer-elimination-question")]
        [Route("skills-health-check/question/answer-elimination-question/body")]
        public async Task<IActionResult> AnswerEliminationQuestion(EliminationAnswerQuestionViewModel model, string answerAction)
        {
            var sessionDataModel = await GetSessionDataModel();
            if (sessionDataModel == null || sessionDataModel.DocumentId == 0)
            {
                Response.Redirect(HomeURL);
            }

            if (ModelState.IsValid)
            {
                var saveAnswerResponse = await _questionService.SubmitAnswer(sessionDataModel!, model);
                if (saveAnswerResponse.Success)
                {
                    await SetSessionStateAsync(sessionDataModel);
                    //UpdateShcUsageDate();
                    
                    return RedirectToNextAction(model);
                }

                return Redirect($"{QuestionURL}?assessmentType={model.Question.AssessmentType}&saveerror={saveAnswerResponse.ErrorMessage}");
            }

            ViewData["QuestionAnswerError"] = true;

            return Redirect($"{QuestionURL}?assessmentType={model.Question.AssessmentType}");
        }

        [HttpPost]
        [Route("skills-health-check/question/answer-feedback-question")]
        [Route("skills-health-check/question/answer-feedback-question/body")]
        public async Task<IActionResult> AnswerFeedbackQuestion(FeedBackQuestionViewModel model, string answerAction)
        {
            var sessionDataModel = await GetSessionDataModel();
            if (sessionDataModel == null || sessionDataModel.DocumentId == 0)
            {
                Response.Redirect(HomeURL);
            }

            if (ModelState.IsValid)
            {
                var saveAnswerResponse = await _questionService.SubmitAnswer(sessionDataModel!, model);
                if (saveAnswerResponse.Success)
                {
                    //UpdateShcUsageDate();

                    return RedirectToNextAction(model);
                }

                return Redirect($"{QuestionURL}?assessmentType={model.FeedbackQuestion.AssessmentType}&saveerror={saveAnswerResponse.ErrorMessage}");
            }

            ViewData["QuestionAnswerError"] = true;

            return Redirect($"{QuestionURL}?assessmentType={model.FeedbackQuestion.AssessmentType}");
        }

        [HttpPost]
        [Route("skills-health-check/question/answer-checking-question")]
        [Route("skills-health-check/question/answer-checking-question/body")]
        public async Task<IActionResult> AnswerCheckingQuestion(TabularAnswerQuestionViewModel model, string answerAction)
        {
            var sessionDataModel = await GetSessionDataModel();
            if (sessionDataModel == null || sessionDataModel.DocumentId == 0)
            {
                Response.Redirect(HomeURL);
            }

            CheckingQuestionValidation(model);

            if (ModelState.IsValid)
            {
                var saveAnswerResponse = await _questionService.SubmitAnswer(sessionDataModel!, model);
                if (saveAnswerResponse.Success)
                {
                    await SetSessionStateAsync(sessionDataModel);
                    //UpdateShcUsageDate();
                    
                    return RedirectToNextAction(model);
                }

                return Redirect($"{QuestionURL}?assessmentType={model.Question.AssessmentType}&saveerror={saveAnswerResponse.ErrorMessage}");
            }

            ViewData["QuestionAnswerError"] = true;

            return Redirect($"{QuestionURL}?assessmentType={model.Question.AssessmentType}");
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
            // TODO: it appears Sitefinity is used to provide a degree of validation message content control
            //if (model.AnswerSelection == null || !model.AnswerSelection.Any())
            //{
            //    string errorMessage = "Choose an answer";
            //    string currentPageUrl = Request.Url.AbsolutePath;
            //    var validationSet = ValidationRulesProvider.GetValidationSetPerPageAndPropertyName(currentPageUrl, nameof(model.AnswerSelection));
            //    if (validationSet != null && validationSet.Required && !string.IsNullOrEmpty(validationSet.RequiredErrorMessage))
            //    {
            //        errorMessage = validationSet.RequiredErrorMessage;
            //    }

            //    ModelState.AddModelError(nameof(model.AnswerSelection), errorMessage);
            //}

            //if (model.AnswerSelection != null && model.AnswerSelection.Count() > 1 &&
            //    model.AnswerSelection.Any(val => val.Equals("E", StringComparison.OrdinalIgnoreCase)))
            //{
            //    string errorMessage = "When No error is selected no other answer can be chosen";
            //    string currentPageUrl = Request.Url.AbsolutePath;
            //    var validationSet = ValidationRulesProvider.GetValidationSetPerPageAndPropertyName(currentPageUrl, nameof(model.AnswerSelection));
            //    if (validationSet != null && !string.IsNullOrEmpty(validationSet.CustomValidationErrorMessage))
            //    {
            //        errorMessage = validationSet.CustomValidationErrorMessage;
            //    }
            //    ModelState.AddModelError(nameof(model.AnswerSelection), errorMessage);
            //}
        }
    }
}
