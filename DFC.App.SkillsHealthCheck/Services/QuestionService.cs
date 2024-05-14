using System;
using DFC.App.SkillsHealthCheck.Services.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Helpers;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
using DFC.App.SkillsHealthCheck.ViewModels.Question;
using System.Linq;
using System.Threading.Tasks;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.Models;
using System.Collections.Generic;
using DFC.SkillsCentral.Api.Domain.Models;

namespace DFC.App.SkillsHealthCheck.Services
{
    public class QuestionService : IQuestionService
    {
        private ISkillsHealthCheckService _skillsHealthCheckService;

        public QuestionService(ISkillsHealthCheckService skillsHealthCheckService)
        {
            this._skillsHealthCheckService = skillsHealthCheckService;
        }

        public async Task<DFC.SkillsCentral.Api.Domain.Models.SkillsDocument> GetSkillsDocument(int documentId)
        {
            return await _skillsHealthCheckService.GetSkillsDocument(documentId);
        }

        public async Task<AssessmentQuestionViewModel> GetAssessmentQuestionViewModel(AssessmentType assessmentType, DFC.SkillsCentral.Api.Domain.Models.SkillsDocument skillsDocument, AssessmentQuestionsOverView assessmentQuestionOverview)
        {
            var feedbackQuestionNext = false;

            var questionNumber = GetQuestionNumber(assessmentType, assessmentQuestionOverview, skillsDocument, ref feedbackQuestionNext);

            if (feedbackQuestionNext)
            {
                return GetAssessmentFeedBackQuestionViewModel(assessmentType, questionNumber, skillsDocument, assessmentQuestionOverview);
            }

            return await GetDetailedAssessmentQuestionViewModel(assessmentType, questionNumber, skillsDocument, assessmentQuestionOverview);
        }

        
        private static int GetQuestionNumber(AssessmentType assessmentType, AssessmentQuestionsOverView assessmentQuestionOverview, DFC.SkillsCentral.Api.Domain.Models.SkillsDocument currentSkillsDocument, ref bool feedbackQuestionNext)
        {
            int questionNumber = 1;
            switch (assessmentType)
            {
                case AssessmentType.Abstract:
                case AssessmentType.Mechanical:
                case AssessmentType.Numerical:
                case AssessmentType.Spatial:
                case AssessmentType.Verbal:
                    questionNumber =
                        currentSkillsDocument.GetAssessmentNextQuestionNumber(assessmentType);
                    feedbackQuestionNext = questionNumber > assessmentQuestionOverview.TotalQuestionsNumber;
                    break;

                case AssessmentType.Motivation:
                case AssessmentType.Interests:
                case AssessmentType.Personal:

                    questionNumber = currentSkillsDocument
                        .GetAssessmentNextMultipleQuestionNumber(
                            assessmentType, assessmentQuestionOverview);
                    break;

                case AssessmentType.Checking:
                    questionNumber = currentSkillsDocument
                        .GetAssessmentNextMultipleQuestionNumber(
                            assessmentType, assessmentQuestionOverview);
                    feedbackQuestionNext = questionNumber > assessmentQuestionOverview.TotalQuestionsNumber;
                    break;

                case AssessmentType.SkillAreas:
                    questionNumber = currentSkillsDocument
                        .GetAssessmentNextEliminationQuestionNumber(
                            assessmentType);
                    feedbackQuestionNext = questionNumber > assessmentQuestionOverview.TotalQuestionsNumber;
                    break;

                default:
                    questionNumber = currentSkillsDocument
                        .GetAssessmentNextEliminationQuestionNumber(
                            assessmentType);
                    break;
            }

            return questionNumber;
        }
   
        private AssessmentQuestionViewModel GetAssessmentFeedBackQuestionViewModel(AssessmentType assessmentType, int questionNumber, DFC.SkillsCentral.Api.Domain.Models.SkillsDocument skillsDocument, AssessmentQuestionsOverView assessmentQuestionOverview)
        {
            var viewModel = new FeedBackQuestionViewModel
            {
                AssessmentTitle = assessmentQuestionOverview.AssessmentTitle,
                AssessmentType = assessmentQuestionOverview.AssessmentType,
                AssessmentSubtitle = assessmentQuestionOverview.SubTitle,
            };

            switch (assessmentType)
            {

                case AssessmentType.Numerical:
                case AssessmentType.Verbal:
                case AssessmentType.Checking:
                case AssessmentType.Mechanical:
                case AssessmentType.Spatial:
                case AssessmentType.Abstract:
                    viewModel.ActualTotalQuestions = assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback;
                    break;

                case AssessmentType.Motivation:
                case AssessmentType.Interests:
                case AssessmentType.Personal:
                    viewModel.ActualTotalQuestions = assessmentQuestionOverview.ActualQuestionsNumberPlusFeedback;
                    break;

                case AssessmentType.SkillAreas:
                    viewModel.ActualTotalQuestions = assessmentQuestionOverview.ActualQuestionsNumberPlusFeedback;
                    questionNumber = assessmentQuestionOverview.ActualQuestionsNumber + 1;
                    break;

                default:
                    questionNumber = skillsDocument
                        .GetAssessmentNextEliminationQuestionNumber(
                            assessmentType);
                    break;
            }

            var howLongDocValue =
                skillsDocument.DataValueKeys.FirstOrDefault(
                    docValue => docValue.Key.Equals($"{assessmentType}.Timing", StringComparison.OrdinalIgnoreCase));

            if (howLongDocValue.Value != null)
            {
                if (string.IsNullOrWhiteSpace(howLongDocValue.Value))
                {
                    viewModel.FeedbackQuestion =
                        SkillsHealthChecksHelper.GetFeedbackQuestionByAssessmentType(assessmentType, 1);
                    viewModel.QuestionNumber = questionNumber;
                    viewModel.FeedbackQuestion.DocValueTitle = $"{assessmentType}.Timing";
                    return viewModel;
                }
            }

            var howEasyDocValue =
                skillsDocument.DataValueKeys.FirstOrDefault(
                    docValue => docValue.Key.Equals($"{assessmentType}.Ease", StringComparison.OrdinalIgnoreCase));

            if (howEasyDocValue.Value != null)
            {
                if (string.IsNullOrWhiteSpace(howEasyDocValue.Value))
                {
                    viewModel.FeedbackQuestion =
                        SkillsHealthChecksHelper.GetFeedbackQuestionByAssessmentType(assessmentType, 2);
                    viewModel.QuestionNumber = questionNumber + 1;
                    viewModel.FeedbackQuestion.DocValueTitle = $"{assessmentType}.Ease";
                    return viewModel;
                }
            }

            var howEnjoyableDocValue =
                skillsDocument.DataValueKeys.FirstOrDefault(
                    docValue => docValue.Key.Equals($"{assessmentType}.Enjoyment",
                        StringComparison.OrdinalIgnoreCase));

            if (howEnjoyableDocValue.Value != null)
            {
                if (string.IsNullOrWhiteSpace(howEnjoyableDocValue.Value))
                {
                    viewModel.FeedbackQuestion =
                        SkillsHealthChecksHelper.GetFeedbackQuestionByAssessmentType(assessmentType, 3);
                    viewModel.QuestionNumber = questionNumber + 2;
                    viewModel.FeedbackQuestion.DocValueTitle = $"{assessmentType}.Enjoyment";
                    return viewModel;
                }
            }

            return viewModel;
        }
 
        private async Task<AssessmentQuestionViewModel> GetDetailedAssessmentQuestionViewModel(AssessmentType assessmentType, int questionNumber, DFC.SkillsCentral.Api.Domain.Models.SkillsDocument skillsDocument, AssessmentQuestionsOverView assessmentQuestionOverview)
        {
            var viewModel = new AssessmentQuestionViewModel();

            var apiResponse = await _skillsHealthCheckService.GetSingleQuestion(questionNumber, assessmentType.ToString());
            if (apiResponse != null)
            {
                switch (assessmentType)
                {
                    case AssessmentType.Abstract:
                    case AssessmentType.Mechanical:
                    case AssessmentType.Numerical:
                    case AssessmentType.Spatial:
                    case AssessmentType.Verbal:
                        var model = new AssessmentQuestionViewModel
                        {
                            QuestionAnswers = apiResponse,
                            QuestionImages = apiResponse.Question.DataHTML.GetDataImages(assessmentType),
                            ActualTotalQuestions = assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback,
                            QuestionNumber = apiResponse.Question.Number,
                            AssessmentTitle = assessmentQuestionOverview.AssessmentTitle,
                            AssessmentType = assessmentType,
                            AssessmentSubtitle = assessmentQuestionOverview.SubTitle,
                            IntroductionText = assessmentQuestionOverview.IntroductionText,
                        };
                        return model;

                    case AssessmentType.Motivation:
                    case AssessmentType.Interests:
                    case AssessmentType.Personal:
                        var multipleAnswerModel = new MultipleAnswerQuestionViewModel
                        {
                            QuestionAnswers = apiResponse,
                            SubQuestions = apiResponse.Answers.Count,
                            CurrentQuestion =
                                skillsDocument.GetCurrentSubQuestionAnswer(assessmentType, questionNumber,
                                    assessmentQuestionOverview),
                            ActualTotalQuestions = assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback,
                            AssessmentType = assessmentType,
                            QuestionNumber = skillsDocument.GetCurrentMultipleAnswerQuestionNumber(assessmentType),
                            AssessmentTitle = assessmentQuestionOverview.AssessmentTitle,
                            IntroductionText = assessmentQuestionOverview.IntroductionText,
                        };
                        return multipleAnswerModel;

                    case AssessmentType.Checking:
                        var tabularViewModel = new TabularAnswerQuestionViewModel
                        {
                            QuestionAnswers = apiResponse,
                            QuestionAnswer = "checking",
                            SubQuestions = 10,
                            CurrentQuestion = skillsDocument.GetCurrentMultipleAnswerQuestionNumber(assessmentType),
                            CurrentRow = skillsDocument.GetCheckingRowNumber(assessmentType),
                            ActualTotalQuestions = assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback,
                            QuestionNumber = skillsDocument.GetCurrentMultipleAnswerQuestionNumber(assessmentType),
                            AssessmentTitle = assessmentQuestionOverview.AssessmentTitle,
                            AssessmentType = assessmentType,
                            IntroductionText = assessmentQuestionOverview.IntroductionText,

                        };
                        return tabularViewModel;

                    case AssessmentType.SkillAreas:
                        var eliminationModel = new EliminationAnswerQuestionViewModel
                        {
                            QuestionAnswers = apiResponse,
                            AlreadySelected =
                                skillsDocument.GetAlreadySelectedAnswer(
                                    assessmentType, apiResponse.Question.Number),
                            ActualTotalQuestions =
                                (assessmentQuestionOverview.TotalQuestionsNumber * 2) +
                                (assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback -
                                 assessmentQuestionOverview.TotalQuestionsNumber),
                            QuestionNumber = skillsDocument.GetCurrentNumberEliminationQuestions(assessmentType, apiResponse.Question.Number),
                            AssessmentTitle = assessmentQuestionOverview.AssessmentTitle,
                            AssessmentType = assessmentType,
                            IntroductionText = assessmentQuestionOverview.IntroductionText,

                        };
                        return eliminationModel;

                    default:
                        return new AssessmentQuestionViewModel
                        {
                            QuestionAnswers = apiResponse,
                        };
                }
            }

            return viewModel;
        }

        private async Task<AssessmentQuestionsOverView> GetAssessmentQuestionsOverview(AssessmentType assessmentType, DFC.SkillsCentral.Api.Domain.Models.SkillsDocument activeSkillsDocument)
        {
            var assessmentQuestionOverview = new AssessmentQuestionsOverView {AssessmentType = assessmentType};
            var overViewResponse = await _skillsHealthCheckService.GetAssessmentQuestions(assessmentType.ToString());
            if (overViewResponse != null)
            {
                var totalQuestions = overViewResponse.Questions.Count;

                assessmentQuestionOverview.TotalQuestionsNumber = totalQuestions;
                assessmentQuestionOverview.SubTitle = overViewResponse.Assessment.Subtitle;
                assessmentQuestionOverview.AssessmentTitle = overViewResponse.Assessment.Title;
                assessmentQuestionOverview.IntroductionText = overViewResponse.Assessment.Introduction;

                var actualQuestionsNumber = 0;
                foreach (var questionAnswers in overViewResponse.Questions)
                {
                    assessmentQuestionOverview.QuestionOverViewList.Add(new QuestionOverView
                    {
                        QuestionNumber = questionAnswers.Question.Number,
                        SubQuestions = questionAnswers.Answers.Count,
                    });
                    actualQuestionsNumber = actualQuestionsNumber + questionAnswers.Answers.Count;
                }

                assessmentQuestionOverview.ActualQuestionsNumber = actualQuestionsNumber;
            }

            assessmentQuestionOverview.ActualQuestionsNumberPlusFeedback =
                assessmentQuestionOverview.ActualQuestionsNumber;

            assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback =
                assessmentQuestionOverview.TotalQuestionsNumber;

            //Tagging on Feedback Questions
            if (activeSkillsDocument.DataValueKeys.ContainsKey($"{assessmentType}.Ease"))
            {
                assessmentQuestionOverview.ActualQuestionsNumberPlusFeedback++;
                assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback++;
            }

            if (activeSkillsDocument.DataValueKeys.ContainsKey($"{assessmentType}.Timing"))
            {
                assessmentQuestionOverview.ActualQuestionsNumberPlusFeedback++;
                assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback++;
            }

            if (activeSkillsDocument.DataValueKeys.ContainsKey($"{assessmentType}.Enjoyment"))
            {
                assessmentQuestionOverview.ActualQuestionsNumberPlusFeedback++;
                assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback++;
            }

            return assessmentQuestionOverview;
        }

        public async Task<DFC.SkillsCentral.Api.Domain.Models.SkillsDocument> SubmitAnswer(SessionDataModel sessionDataModel, AssessmentQuestionViewModel model)
        {
            var getDocumentResponse = await _skillsHealthCheckService.GetSkillsDocument((int)sessionDataModel.DocumentId);

            if (getDocumentResponse == null)
            {
                return getDocumentResponse;
            }

            if (model is FeedBackQuestionViewModel feedBackQuestionViewModel)
            {
                getDocumentResponse = getDocumentResponse.UpdateSpecificDataValue(feedBackQuestionViewModel.FeedbackQuestion.DocValueTitle, model.QuestionAnswer);
                if (model.QuestionNumber == model.ActualTotalQuestions)
                {
                    getDocumentResponse = getDocumentResponse.UpdateSpecificDataValue($"{feedBackQuestionViewModel.FeedbackQuestion.AssessmentType}.Complete", bool.TrueString);
                }
            }
            else if (model is TabularAnswerQuestionViewModel tabularAnswerQuestionViewModel)
            {
                var subQuestionAnswer = SkillsHealthChecksHelper.GetAnswerTotal(tabularAnswerQuestionViewModel.AnswerSelection);
                var assessmentQuestionOverview = await GetAssessmentQuestionsOverview(sessionDataModel, (AssessmentType)model.AssessmentType, getDocumentResponse);
                model.QuestionAnswer = subQuestionAnswer;
                getDocumentResponse = UpdateSkillsDocument(getDocumentResponse, model, assessmentQuestionOverview);
            }
            else
            {
                var assessmentQuestionOverview = await GetAssessmentQuestionsOverview(sessionDataModel, (AssessmentType)model.AssessmentType, getDocumentResponse);

                getDocumentResponse = UpdateSkillsDocument(getDocumentResponse, model, assessmentQuestionOverview);
            }

            return await _skillsHealthCheckService.SaveSkillsDocument(getDocumentResponse);
        }

        public async Task<AssessmentQuestionsOverView> GetAssessmentQuestionsOverview(SessionDataModel sessionDataModel, AssessmentType assessmentType, DFC.SkillsCentral.Api.Domain.Models.SkillsDocument skillsDocument)
        {
            var overviewSessionId = string.Format(Constants.SkillsHealthCheck.AssessmentQuestionOverviewId, assessmentType);

            sessionDataModel.AssessmentQuestionsOverViews ??= new Dictionary<string, AssessmentQuestionsOverView>();

            var assessmentQuestionOverview = sessionDataModel.AssessmentQuestionsOverViews.ContainsKey(overviewSessionId) ? sessionDataModel.AssessmentQuestionsOverViews[overviewSessionId] : new AssessmentQuestionsOverView();

            if (assessmentQuestionOverview.ActualQuestionsNumber == 0)
            {
                assessmentQuestionOverview = await GetAssessmentQuestionsOverview(assessmentType, skillsDocument);
                if (sessionDataModel.AssessmentQuestionsOverViews.ContainsKey(overviewSessionId))
                {
                    sessionDataModel.AssessmentQuestionsOverViews[overviewSessionId] = assessmentQuestionOverview;
                }
                else
                {
                    sessionDataModel.AssessmentQuestionsOverViews.Add(overviewSessionId, assessmentQuestionOverview);
                }
            }

            return assessmentQuestionOverview;
        }

        private static DFC.SkillsCentral.Api.Domain.Models.SkillsDocument UpdateSkillsDocument(DFC.SkillsCentral.Api.Domain.Models.SkillsDocument getDocumentResponse, AssessmentQuestionViewModel model, AssessmentQuestionsOverView assessmentQuestionOverview)
        {
            var isComplete = model.QuestionAnswers?.Question.Number == assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback;
            return model switch
            {
                EliminationAnswerQuestionViewModel viewModel => getDocumentResponse.UpdateEliminationDataValues(
                        viewModel.QuestionAnswer,
                        viewModel.QuestionAnswers?.Question.Number == assessmentQuestionOverview.TotalQuestionsNumber && viewModel.QuestionNumber == model.ActualTotalQuestions,
                        assessmentQuestionOverview.AssessmentType,
                        viewModel.QuestionAnswers.Question.Number,
                        assessmentQuestionOverview.TotalQuestionsNumber,
                        assessmentQuestionOverview.ActualQuestionsNumber,
                        viewModel.QuestionNumber),
                _ => getDocumentResponse.UpdateDataValues(
                    model.QuestionAnswer,
                    isComplete,
                    assessmentQuestionOverview.AssessmentType,
                    assessmentQuestionOverview.TotalQuestionsNumber,
                    model.QuestionAnswers.Question.Number)
            };
        }
    }
}
