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
using Microsoft.Extensions.Logging;

namespace DFC.App.SkillsHealthCheck.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly ILogger<QuestionService> logger;
        private ISkillsHealthCheckService _skillsHealthCheckService;

        public QuestionService(ISkillsHealthCheckService skillsHealthCheckService, ILogger<QuestionService> logger)
        {
            _skillsHealthCheckService = skillsHealthCheckService;
            this.logger = logger;
        }

        public GetSkillsDocumentResponse GetSkillsDocument(GetSkillsDocumentRequest getSkillsDocumentRequest)
        {
            logger.LogInformation($"{nameof(GetSkillsDocument)} has been called");

            return _skillsHealthCheckService.GetSkillsDocument(getSkillsDocumentRequest);
        }

        public AssessmentQuestionViewModel GetAssessmentQuestionViewModel(Level level, Accessibility accessibility, AssessmentType assessmentType, SkillsDocument skillsDocument, AssessmentQuestionsOverView assessmentQuestionOverview)
        {
            logger.LogInformation($"{nameof(GetAssessmentQuestionViewModel)} has been called");

            var feedbackQuestionNext = false;

            var questionNumber = GetQuestionNumber(assessmentType, assessmentQuestionOverview, skillsDocument, ref feedbackQuestionNext);

            if (feedbackQuestionNext)
            {
                return GetAssessmentFeedBackQuestionViewModel(assessmentType, questionNumber, skillsDocument, assessmentQuestionOverview);
            }

            return GetDetailedAssessmentQuestionViewModel(level, accessibility, assessmentType, questionNumber, skillsDocument, assessmentQuestionOverview);
        }

        /// <summary>
        /// Gets the question number.
        /// </summary>
        /// <param name="assessmentType">Type of the assessment.</param>
        /// <param name="assessmentQuestionOverview">The assessment question overview.</param>
        /// <param name="currentSkillsDocument">The current skills document.</param>
        /// <param name="feedbackQuestionNext">if set to <c>true</c> [feedback question next].</param>
        /// <returns></returns>
        private static int GetQuestionNumber(AssessmentType assessmentType, AssessmentQuestionsOverView assessmentQuestionOverview, SkillsDocument currentSkillsDocument, ref bool feedbackQuestionNext)
        {
            int questionNumber;
            switch (assessmentType)
            {
                case AssessmentType.Abstract:
                case AssessmentType.Mechanical:
                case AssessmentType.Numeric:
                case AssessmentType.Spatial:
                case AssessmentType.Verbal:
                    questionNumber =
                        currentSkillsDocument.GetAssessmentNextQuestionNumber(assessmentType);
                    feedbackQuestionNext = questionNumber > assessmentQuestionOverview.TotalQuestionsNumber;
                    break;

                case AssessmentType.Motivation:
                case AssessmentType.Interest:
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

        /// <summary>
        /// Gets the assessment feed back question view model.
        /// </summary>
        /// <param name="assessmentType">Type of the assessment.</param>
        /// <param name="questionNumber">The question number.</param>
        /// <param name="skillsDocument">The skills document.</param>
        /// <param name="assessmentQuestionOverview">The assessment question overview.</param>
        /// <returns></returns>
        private AssessmentQuestionViewModel GetAssessmentFeedBackQuestionViewModel(AssessmentType assessmentType, int questionNumber, SkillsDocument skillsDocument, AssessmentQuestionsOverView assessmentQuestionOverview)
        {
            logger.LogInformation($"{nameof(GetAssessmentFeedBackQuestionViewModel)} has been called");

            var viewModel = new FeedBackQuestionViewModel
            {
                AssessmentTitle = assessmentQuestionOverview.AssessmentTitle
            };

            switch (assessmentType)
            {
                case AssessmentType.Abstract:
                case AssessmentType.Mechanical:
                case AssessmentType.Numeric:
                case AssessmentType.Spatial:
                case AssessmentType.Verbal:
                    viewModel.ActualTotalQuestions = assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback;
                    break;

                case AssessmentType.Motivation:
                case AssessmentType.Interest:
                case AssessmentType.Personal:
                    viewModel.ActualTotalQuestions = assessmentQuestionOverview.ActualQuestionsNumberPlusFeedback;
                    break;

                case AssessmentType.Checking:
                    viewModel.ActualTotalQuestions = assessmentQuestionOverview.ActualQuestionsNumberPlusFeedback;
                    questionNumber = assessmentQuestionOverview.ActualQuestionsNumber + 1;
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
                skillsDocument.SkillsDocumentDataValues.FirstOrDefault(
                    docValue => docValue.Title.Equals($"{assessmentType}.Timing", StringComparison.OrdinalIgnoreCase));

            if (howLongDocValue != null)
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
            else
            {
                logger.LogWarning($"{nameof(howLongDocValue)} has returned null");
            }

            var howEasyDocValue =
                skillsDocument.SkillsDocumentDataValues.FirstOrDefault(
                    docValue => docValue.Title.Equals($"{assessmentType}.Ease", StringComparison.OrdinalIgnoreCase));

            if (howEasyDocValue != null)
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
            else
            {
                logger.LogWarning($"{nameof(howEasyDocValue)} has returned null");
            }

            var howEnjoyableDocValue =
                skillsDocument.SkillsDocumentDataValues.FirstOrDefault(
                    docValue => docValue.Title.Equals($"{assessmentType}.Enjoyment",
                        StringComparison.OrdinalIgnoreCase));

            if (howEnjoyableDocValue != null)
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
            else
            {
                logger.LogWarning($"{nameof(howEnjoyableDocValue)} has returned null");
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
        private AssessmentQuestionViewModel GetDetailedAssessmentQuestionViewModel(Level level, Accessibility accessibility, AssessmentType assessmentType, int questionNumber, SkillsDocument skillsDocument, AssessmentQuestionsOverView assessmentQuestionOverview)
        {
            logger.LogInformation($"{nameof(GetDetailedAssessmentQuestionViewModel)} has been called");

            var viewModel = new AssessmentQuestionViewModel();

            var apiRequest = new GetAssessmentQuestionRequest
            {
                QuestionNumber = questionNumber,
                Accessibility = accessibility,
                AsessmentType = assessmentType,
                Level = level,
            };

            var apiResponse = _skillsHealthCheckService.GetAssessmentQuestion(apiRequest);
            if (apiResponse.Success)
            {
                logger.LogInformation($"{nameof(apiResponse)} has returned successfully");

                switch (assessmentType)
                {
                    case AssessmentType.Abstract:
                    case AssessmentType.Mechanical:
                    case AssessmentType.Numeric:
                    case AssessmentType.Spatial:
                    case AssessmentType.Verbal:
                        var model = new AssessmentQuestionViewModel
                        {
                            Question = apiResponse.Question,
                            QuestionImages = apiResponse.Question.QuestionData.GetDataImages(assessmentType),
                            ActualTotalQuestions = assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback,
                            QuestionNumber = apiResponse.Question.QuestionNumber,
                            AssessmentTitle = apiResponse.Question.AssessmentTitle,
                        };
                        return model;

                    case AssessmentType.Motivation:
                    case AssessmentType.Interest:
                    case AssessmentType.Personal:
                        var multipleAnswerModel = new MultipleAnswerQuestionViewModel
                        {
                            Question = apiResponse.Question,
                            SubQuestions = apiResponse.Question.PossibleResponses.Count,
                            CurrentQuestion =
                                skillsDocument.GetCurrentSubQuestionAnswer(assessmentType, questionNumber,
                                    assessmentQuestionOverview),
                            ActualTotalQuestions = assessmentQuestionOverview.ActualQuestionsNumberPlusFeedback,
                            QuestionNumber = skillsDocument.GetCurrentMultipleAnswerQuestionNumber(assessmentType),
                            AssessmentTitle = apiResponse.Question.AssessmentTitle,
                        };
                        return multipleAnswerModel;

                    case AssessmentType.Checking:
                        var tabularViewModel = new TabularAnswerQuestionViewModel
                        {
                            Question = apiResponse.Question,
                            QuestionAnswer = "checking",
                            SubQuestions = 10,
                            CurrentQuestion =
                                skillsDocument.GetCurrentSubQuestionAnswer(assessmentType, questionNumber,
                                    assessmentQuestionOverview),
                            ActualTotalQuestions = assessmentQuestionOverview.ActualQuestionsNumberPlusFeedback,
                            QuestionNumber = skillsDocument.GetCurrentMultipleAnswerQuestionNumber(assessmentType),
                            AssessmentTitle = apiResponse.Question.AssessmentTitle,
                        };
                        return tabularViewModel;

                    case AssessmentType.SkillAreas:
                        var eliminationModel = new EliminationAnswerQuestionViewModel
                        {
                            Question = apiResponse.Question,
                            AlreadySelected =
                                skillsDocument.GetAlreadySelectedAnswer(
                                    assessmentType, apiResponse.Question.QuestionNumber),
                            ActualTotalQuestions =
                                (assessmentQuestionOverview.TotalQuestionsNumber * 2) +
                                (assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback -
                                 assessmentQuestionOverview.TotalQuestionsNumber),
                            QuestionNumber = skillsDocument.GetCurrentNumberEliminationQuestions(assessmentType, apiResponse.Question.QuestionNumber),
                            AssessmentTitle = apiResponse.Question.AssessmentTitle,
                        };
                        return eliminationModel;

                    default:
                        return new AssessmentQuestionViewModel
                        {
                            Question = apiResponse.Question,
                        };
                }
            }
            else
            {
                logger.LogWarning($"{nameof(apiResponse)} has returned unsuccessfully");
            }

            return viewModel;
        }

        /// <summary>
        /// Gets the assessment questions overview.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="accessibility">The accessibility.</param>
        /// <param name="assessmentType">Type of the assessment.</param>
        /// <param name="activeSkillsDocument">The active skills document.</param>
        /// <returns></returns>
        private AssessmentQuestionsOverView GetAssessmentQuestionsOverview(Level level, Accessibility accessibility, AssessmentType assessmentType, SkillsDocument activeSkillsDocument)
        {
            logger.LogInformation($"{nameof(GetAssessmentQuestionsOverview)} has been called");

            var assessmentQuestionOverview = new AssessmentQuestionsOverView {AssessmentType = assessmentType};
            var apiOverviewRequest = new GetAssessmentQuestionRequest
            {
                QuestionNumber = 1,
                Accessibility = accessibility,
                AsessmentType = assessmentType,
                Level = level
            };

            var overViewResponse = _skillsHealthCheckService.GetAssessmentQuestion(apiOverviewRequest);
            if (overViewResponse.Success)
            {
                logger.LogInformation($"{nameof(overViewResponse)} has returned successfully");

                assessmentQuestionOverview.TotalQuestionsNumber = overViewResponse.Question.TotalQuestionNumber;
                assessmentQuestionOverview.SubTitle = overViewResponse.Question.SubTitle;
                assessmentQuestionOverview.AssessmentTitle = overViewResponse.Question.AssessmentTitle;

                var actualQuestionsNumber = 0;
                var totalQuestions = overViewResponse.Question.TotalQuestionNumber;
                if (assessmentType == AssessmentType.Checking)
                {
                    for (var i = 1; i <= totalQuestions; i++)
                    {
                        assessmentQuestionOverview.QuestionOverViewList.Add(new QuestionOverView
                        {
                            QuestionNumber = i,
                            SubQuestions = 10,
                        });
                        actualQuestionsNumber = actualQuestionsNumber + 10;
                    }
                }
                else
                {
                    assessmentQuestionOverview.QuestionOverViewList.Add(new QuestionOverView
                    {
                        QuestionNumber = overViewResponse.Question.QuestionNumber,
                        SubQuestions = overViewResponse.Question.PossibleResponses.Count
                    });

                    actualQuestionsNumber = overViewResponse.Question.PossibleResponses.Count;

                    for (var i = 2; i <= totalQuestions; i++)
                    {
                        var questionOverviewReq = new GetAssessmentQuestionRequest
                        {
                            QuestionNumber = i,
                            Accessibility = accessibility,
                            AsessmentType = assessmentType,
                            Level = level
                        };

                        overViewResponse = _skillsHealthCheckService.GetAssessmentQuestion(questionOverviewReq);
                        if (overViewResponse.Success)
                        {
                            assessmentQuestionOverview.QuestionOverViewList.Add(new QuestionOverView
                            {
                                QuestionNumber = overViewResponse.Question.QuestionNumber,
                                SubQuestions = overViewResponse.Question.PossibleResponses.Count
                            });

                            actualQuestionsNumber = actualQuestionsNumber +
                                                    overViewResponse.Question.PossibleResponses.Count;
                        }
                    }
                }

                assessmentQuestionOverview.ActualQuestionsNumber = actualQuestionsNumber;
            }
            else
            {
                logger.LogWarning($"{nameof(overViewResponse)} has returned unsuccessfully");
            }

            assessmentQuestionOverview.ActualQuestionsNumberPlusFeedback =
                assessmentQuestionOverview.ActualQuestionsNumber;

            assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback =
                assessmentQuestionOverview.TotalQuestionsNumber;

            //Tagging on Feedback Questions
            if (
                activeSkillsDocument.SkillsDocumentDataValues.Exists(
                    docTitle => docTitle.Title.Equals($"{assessmentType}.Ease", StringComparison.OrdinalIgnoreCase)))
            {
                assessmentQuestionOverview.ActualQuestionsNumberPlusFeedback++;
                assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback++;
            }

            if (
                activeSkillsDocument.SkillsDocumentDataValues.Exists(
                    docTitle => docTitle.Title.Equals($"{assessmentType}.Timing", StringComparison.OrdinalIgnoreCase)))
            {
                assessmentQuestionOverview.ActualQuestionsNumberPlusFeedback++;
                assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback++;
            }

            if (
                activeSkillsDocument.SkillsDocumentDataValues.Exists(
                    docTitle => docTitle.Title.Equals($"{assessmentType}.Enjoyment",
                        StringComparison.OrdinalIgnoreCase)))
            {
                assessmentQuestionOverview.ActualQuestionsNumberPlusFeedback++;
                assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback++;
            }

            return assessmentQuestionOverview;
        }

        public async Task<SaveQuestionAnswerResponse> SubmitAnswer(SessionDataModel sessionDataModel, AssessmentQuestionViewModel model)
        {
            logger.LogInformation($"{nameof(SubmitAnswer)} has been called");

            var getDocumentResponse = _skillsHealthCheckService.GetSkillsDocument(new GetSkillsDocumentRequest
            {
                DocumentId = sessionDataModel.DocumentId,
            });

            if (!getDocumentResponse.Success)
            {
                logger.LogInformation($"{nameof(getDocumentResponse)} has returned unsuccessfully");

                return new SaveQuestionAnswerResponse
                {
                    Success = false,
                    ErrorMessage = "Could not retrieve skills document",
                };
            }

            if (model is FeedBackQuestionViewModel feedBackQuestionViewModel)
            {
                getDocumentResponse.SkillsDocument = getDocumentResponse.SkillsDocument.UpdateSpecificDataValue(feedBackQuestionViewModel.FeedbackQuestion.DocValueTitle, model.QuestionAnswer);
                if (model.QuestionNumber == model.ActualTotalQuestions)
                {
                    getDocumentResponse.SkillsDocument = getDocumentResponse.SkillsDocument.UpdateSpecificDataValue($"{feedBackQuestionViewModel.FeedbackQuestion.AssessmentType}.Complete", bool.TrueString);
                }
            }
            else if (model is TabularAnswerQuestionViewModel tabularAnswerQuestionViewModel)
            {
                var subQuestionAnswer = SkillsHealthChecksHelper.GetAnswerTotal(tabularAnswerQuestionViewModel.AnswerSelection);
                var assessmentQuestionOverview = GetAssessmentQuestionsOverview(
                    sessionDataModel,
                    tabularAnswerQuestionViewModel.Question.Level,
                    tabularAnswerQuestionViewModel.Question.Accessibility,
                    tabularAnswerQuestionViewModel.Question.AssessmentType,
                    getDocumentResponse.SkillsDocument);
                getDocumentResponse.SkillsDocument = getDocumentResponse.SkillsDocument.UpdateMultipleAnswerDataValues(
                    subQuestionAnswer,
                    tabularAnswerQuestionViewModel.QuestionNumber == tabularAnswerQuestionViewModel.ActualTotalQuestions,
                    tabularAnswerQuestionViewModel.Question.AssessmentType,
                    tabularAnswerQuestionViewModel.Question.QuestionNumber,
                    tabularAnswerQuestionViewModel.CurrentQuestion - 1,
                    tabularAnswerQuestionViewModel.SubQuestions,
                    assessmentQuestionOverview,
                    tabularAnswerQuestionViewModel.QuestionNumber);
            }
            else
            {
                var assessmentQuestionOverview = GetAssessmentQuestionsOverview(
                    sessionDataModel,
                    model.Question.Level,
                    model.Question.Accessibility,
                    model.Question.AssessmentType,
                    getDocumentResponse.SkillsDocument);

                getDocumentResponse.SkillsDocument = UpdateSkillsDocument(getDocumentResponse, model, assessmentQuestionOverview);
            }

            var saveAnswerRequest = new SaveQuestionAnswerRequest
            {
                DocumentId = sessionDataModel.DocumentId,
                SkillsDocument = getDocumentResponse.SkillsDocument,
            };

            return _skillsHealthCheckService.SaveQuestionAnswer(saveAnswerRequest);
        }

        public AssessmentQuestionsOverView GetAssessmentQuestionsOverview(SessionDataModel sessionDataModel, Level level, Accessibility accessibility, AssessmentType assessmentType, SkillsDocument skillsDocument)
        {
            logger.LogInformation($"{nameof(GetAssessmentQuestionsOverview)} has been called");

            var overviewSessionId = string.Format(Constants.SkillsHealthCheck.AssessmentQuestionOverviewId, assessmentType);

            sessionDataModel.AssessmentQuestionsOverViews ??= new Dictionary<string, AssessmentQuestionsOverView>();

            var assessmentQuestionOverview = sessionDataModel.AssessmentQuestionsOverViews.ContainsKey(overviewSessionId) ? sessionDataModel.AssessmentQuestionsOverViews[overviewSessionId] : new AssessmentQuestionsOverView();

            if (assessmentQuestionOverview.ActualQuestionsNumber == 0)
            {
                assessmentQuestionOverview = GetAssessmentQuestionsOverview(level, accessibility, assessmentType, skillsDocument);
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

        private static SkillsDocument UpdateSkillsDocument(GetSkillsDocumentResponse getDocumentResponse, AssessmentQuestionViewModel model, AssessmentQuestionsOverView assessmentQuestionOverview)
        {
            var isComplete = model.Question?.QuestionNumber == assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback;
            return model switch
            {
                EliminationAnswerQuestionViewModel viewModel => getDocumentResponse.SkillsDocument.UpdateEliminationDataValues(
                        viewModel.QuestionAnswer,
                        viewModel.Question?.QuestionNumber == viewModel.Question.TotalQuestionNumber && viewModel.AlreadySelected != -1,
                        viewModel.Question.AssessmentType,
                        viewModel.Question.QuestionNumber,
                        viewModel.Question.TotalQuestionNumber,
                        assessmentQuestionOverview.ActualQuestionsNumber,
                        viewModel.QuestionNumber),
                MultipleAnswerQuestionViewModel questionViewModel => getDocumentResponse.SkillsDocument.UpdateMultipleAnswerDataValues(
                        questionViewModel.QuestionAnswer,
                        questionViewModel.QuestionNumber == questionViewModel.ActualTotalQuestions,
                        questionViewModel.Question.AssessmentType,
                        questionViewModel.Question.QuestionNumber,
                        questionViewModel.CurrentQuestion - 1,
                        questionViewModel.SubQuestions,
                        assessmentQuestionOverview,
                        questionViewModel.QuestionNumber),
                _ => getDocumentResponse.SkillsDocument.UpdateDataValues(
                    model.QuestionAnswer,
                    isComplete,
                    model.Question.AssessmentType,
                    assessmentQuestionOverview.TotalQuestionsNumber,
                    model.Question.QuestionNumber)
            };
        }
    }
}
