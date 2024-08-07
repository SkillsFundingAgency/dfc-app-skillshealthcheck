﻿using System;
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

        public async Task<AssessmentQuestionViewModel> GetAssessmentQuestionViewModel(AssessmentType assessmentType, DFC.SkillsCentral.Api.Domain.Models.SkillsDocument skillsDocument, AssessmentQuestionsOverView assessmentQuestionOverview, int questionNumber)
        {
            var feedbackQuestionNext = false;
            if (questionNumber == 0)
            {
                questionNumber = GetNextQuestionNumber(assessmentType, assessmentQuestionOverview, skillsDocument, ref feedbackQuestionNext);
            }

            feedbackQuestionNext = questionNumber > assessmentQuestionOverview.TotalQuestionsNumber && questionNumber <= assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback;

            if (feedbackQuestionNext)
            {
                return GetAssessmentFeedBackQuestionViewModel(assessmentType, questionNumber, skillsDocument, assessmentQuestionOverview);
            }

            return await GetDetailedAssessmentQuestionViewModel(assessmentType, questionNumber, skillsDocument, assessmentQuestionOverview);
        }

        /// <summary>
        /// Gets the question number.
        /// </summary>
        /// <param name="assessmentType">Type of the assessment.</param>
        /// <param name="assessmentQuestionOverview">The assessment question overview.</param>
        /// <param name="currentSkillsDocument">The current skills document.</param>
        /// <param name="feedbackQuestionNext">if set to <c>true</c> [feedback question next].</param>
        /// <returns></returns>
        private static int GetNextQuestionNumber(AssessmentType assessmentType, AssessmentQuestionsOverView assessmentQuestionOverview, DFC.SkillsCentral.Api.Domain.Models.SkillsDocument currentSkillsDocument, ref bool feedbackQuestionNext)
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

        private static int GetPreviousQuestionNumber(AssessmentType assessmentType, AssessmentQuestionsOverView assessmentQuestionOverview, DFC.SkillsCentral.Api.Domain.Models.SkillsDocument currentSkillsDocument, ref bool feedbackQuestionNext)
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
                        currentSkillsDocument.GetAssessmentPreviousQuestionNumber(assessmentType);
                    feedbackQuestionNext = questionNumber > assessmentQuestionOverview.TotalQuestionsNumber;
                    break;

                case AssessmentType.Motivation:
                case AssessmentType.Interests:
                case AssessmentType.Personal:

                    questionNumber = currentSkillsDocument
                        .GetAssessmentPreviousMultipleQuestionNumber(
                            assessmentType, assessmentQuestionOverview);
                    break;

                case AssessmentType.Checking:
                    questionNumber = currentSkillsDocument
                        .GetAssessmentPreviousMultipleQuestionNumber(
                            assessmentType, assessmentQuestionOverview);
                    feedbackQuestionNext = questionNumber > assessmentQuestionOverview.TotalQuestionsNumber;
                    break;

                case AssessmentType.SkillAreas:
                    questionNumber = currentSkillsDocument
                        .GetAssessmentPreviousEliminationQuestionNumber(
                            assessmentType);
                    feedbackQuestionNext = questionNumber > assessmentQuestionOverview.TotalQuestionsNumber;
                    break;

                default:
                    questionNumber = currentSkillsDocument
                        .GetAssessmentPreviousEliminationQuestionNumber(
                            assessmentType);
                    break;
            }

            return questionNumber;
        }

        /// <summary>
        /// Gets the assessment feed back question view model.
        /// </summary>
        /// <param name="assessmentType">Type of the assessment.</param>
        /// <param name="questionNumber">The question number.</param>
        /// <param name="skillsDocument">The skills document.</param>
        /// <param name="assessmentQuestionOverview">The assessment question overview.</param>
        /// <returns></returns>
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
                if (string.IsNullOrWhiteSpace(howLongDocValue.Value) || assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback - questionNumber == 2 ||
                    (assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback - questionNumber == 1 && (assessmentType is AssessmentType.Numerical or AssessmentType.Verbal)))
                {
                    viewModel.FeedbackQuestion =
                        SkillsHealthChecksHelper.GetFeedbackQuestionByAssessmentType(assessmentType, 1);
                    viewModel.QuestionNumber = questionNumber;
                    viewModel.FeedbackQuestion.DocValueTitle = $"{assessmentType}.Timing";
                    viewModel.QuestionAnswer = howLongDocValue.Value;
                    return viewModel;
                }
            }

            var howEasyDocValue =
                skillsDocument.DataValueKeys.FirstOrDefault(
                    docValue => docValue.Key.Equals($"{assessmentType}.Ease", StringComparison.OrdinalIgnoreCase));

            if (howEasyDocValue.Value != null)
            {
                if (string.IsNullOrWhiteSpace(howEasyDocValue.Value) || assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback - questionNumber == 1)
                {
                    viewModel.FeedbackQuestion =
                        SkillsHealthChecksHelper.GetFeedbackQuestionByAssessmentType(assessmentType, 2);
                    viewModel.QuestionNumber = questionNumber;
                    viewModel.QuestionAnswer = howEasyDocValue.Value;
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
                    viewModel.QuestionNumber = questionNumber;
                    viewModel.FeedbackQuestion.DocValueTitle = $"{assessmentType}.Enjoyment";
                    return viewModel;
                }
            }

            return viewModel;
        }

        /// <summary>
        /// Gets the detailed assessment question view model.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="accessibility">The accessibility.</param>
        /// <param name="assessmentType">Type of the assessment.</param>
        /// <param name="questionNumber">The question number.</param>
        /// <param name="skillsDocument">The skills document.</param>
        /// <param name="assessmentQuestionOverview">The assessment question overview.</param>
        /// <returns></returns>
        private async Task<AssessmentQuestionViewModel> GetDetailedAssessmentQuestionViewModel(AssessmentType assessmentType, int questionNumber, DFC.SkillsCentral.Api.Domain.Models.SkillsDocument skillsDocument, AssessmentQuestionsOverView assessmentQuestionOverview)
        {
            var viewModel = new AssessmentQuestionViewModel();
            var currentQuestion = assessmentType == AssessmentType.SkillAreas ? questionNumber % 2 == 0 ? questionNumber / 2 : (questionNumber / 2) + 1 : questionNumber;
            var apiResponse = await _skillsHealthCheckService.GetSingleQuestion(currentQuestion, assessmentType.ToString());
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
                            QuestionAnswer = skillsDocument.GetCurrentQuestionAnswer(assessmentType, currentQuestion, questionNumber),
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
                            QuestionAnswer = skillsDocument.GetCurrentQuestionAnswer(assessmentType, currentQuestion, questionNumber),
                            QuestionAnswers = apiResponse,
                            SubQuestions = apiResponse.Answers.Count,
                            ActualTotalQuestions = assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback,
                            AssessmentType = assessmentType,
                            QuestionNumber = apiResponse.Question.Number,
                            AssessmentTitle = assessmentQuestionOverview.AssessmentTitle,
                            IntroductionText = assessmentQuestionOverview.IntroductionText,
                        };
                        return multipleAnswerModel;

                    case AssessmentType.Checking:
                        var couldParse = int.TryParse(skillsDocument.GetCurrentQuestionAnswer(assessmentType, currentQuestion, questionNumber), out var previousAnswer);
                        var tabularViewModel = new TabularAnswerQuestionViewModel
                        {
                            AnswerSelection = skillsDocument.GetPreviousAnswerSelections(couldParse ? previousAnswer : 0),
                            QuestionAnswers = apiResponse,
                            SubQuestions = 10,
                            CurrentQuestion = skillsDocument.GetCurrentMultipleAnswerQuestionNumber(assessmentType),
                            CurrentRow = skillsDocument.GetCheckingRowNumber(assessmentType, questionNumber),
                            ActualTotalQuestions = assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback,
                            QuestionNumber = questionNumber,
                            AssessmentTitle = assessmentQuestionOverview.AssessmentTitle,
                            AssessmentType = assessmentType,
                            IntroductionText = assessmentQuestionOverview.IntroductionText,

                        };
                        return tabularViewModel;

                    case AssessmentType.SkillAreas:
                        var eliminationModel = new EliminationAnswerQuestionViewModel
                        {
                            QuestionAnswer = skillsDocument.GetCurrentQuestionAnswer(assessmentType, currentQuestion, questionNumber),
                            QuestionAnswers = apiResponse,
                            AlreadySelected =
                                skillsDocument.GetAlreadySelectedAnswer(
                                    assessmentType, currentQuestion, questionNumber),
                            ActualTotalQuestions =
                                (assessmentQuestionOverview.TotalQuestionsNumber * 2) +
                                (assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback -
                                 assessmentQuestionOverview.TotalQuestionsNumber),
                            QuestionNumber = questionNumber,
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
                        viewModel.QuestionAnswers?.Question.Number == assessmentQuestionOverview.TotalQuestionsNumber && viewModel.AlreadySelected != -1,
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
