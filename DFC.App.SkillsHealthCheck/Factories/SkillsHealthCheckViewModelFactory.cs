using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Helpers;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
using DFC.App.SkillsHealthCheck.ViewModels.Question;

namespace DFC.App.SkillsHealthCheck.Factories
{
    public class SkillsHealthCheckViewModelFactory
    {
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

        /// <summary>
        /// The check question access type
        /// </summary>
        private const string CheckQuestionAccessType = "sr_check";

        /// <summary>
        /// The number question accessible type
        /// </summary>
        private const string NumQuestionAccessibleType = "sr_num";

        /// <summary>
        /// The _course search service
        /// </summary>
        private readonly ISkillsHealthCheckService _skillsHealthCheckService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkillsHealthCheckViewModelFactory" /> class.
        /// </summary>
        /// <param name="skillsHealthCheckService">The course search service.</param>
        public SkillsHealthCheckViewModelFactory(ISkillsHealthCheckService skillsHealthCheckService)
        {
            _skillsHealthCheckService = skillsHealthCheckService;
        }

        /// <summary>
        /// Gets the assessment question view model.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="level">The level.</param>
        /// <param name="accessibility">The accessibility.</param>
        /// <param name="assessmentType">Type of the assessment.</param>
        /// <param name="skillsDocument">The skills document.</param>
        /// <param name="assessmentQuestionOverview">The assessment question overview.</param>
        /// <returns></returns>
        public AssessmentQuestionViewModel GetAssessmentQuestionViewModel(long documentId, Level level, Accessibility accessibility, AssessmentType assessmentType, SkillsDocument skillsDocument ,AssessmentQuestionsOverView assessmentQuestionOverview)
        {
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

            var howEnjoyableDocValue =
                skillsDocument.SkillsDocumentDataValues.FirstOrDefault(
                    docValue => docValue.Title.Equals($"{assessmentType}.Enjoyment", StringComparison.OrdinalIgnoreCase));

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
            var viewModel = new AssessmentQuestionViewModel();

            var apiRequest = new GetAssessmentQuestionRequest
            {
                QuestionNumber = questionNumber,
                Accessibility = accessibility,
                AsessmentType = assessmentType,
                Level = level
            };

            var apiResponse = _skillsHealthCheckService.GetAssessmentQuestion(apiRequest);
            if (apiResponse.Success)
            {
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
                        };
                        return multipleAnswerModel;

                    case AssessmentType.Checking:
                        var tabularViewModel = new TabularAnswerQuestionViewModel
                        {
                            Question = apiResponse.Question,
                            SubQuestions = 10,
                            CurrentQuestion =
                                skillsDocument.GetCurrentSubQuestionAnswer(assessmentType, questionNumber,
                                    assessmentQuestionOverview),
                            ActualTotalQuestions = assessmentQuestionOverview.ActualQuestionsNumberPlusFeedback,
                            QuestionNumber = skillsDocument.GetCurrentMultipleAnswerQuestionNumber(assessmentType),
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
                        };
                        return eliminationModel;

                    default:
                        return new AssessmentQuestionViewModel
                        {
                            Question = apiResponse.Question,
                        };
                }
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
        public AssessmentQuestionsOverView GetAssessmentQuestionsOverview(Level level, Accessibility accessibility, AssessmentType assessmentType, SkillsDocument activeSkillsDocument)
        {
            var assessmentQuestionOverview = new AssessmentQuestionsOverView { AssessmentType = assessmentType };
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
                            SubQuestions = 10
                        });
                        actualQuestionsNumber = actualQuestionsNumber +
                                                10;
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
                    docTitle => docTitle.Title.Equals($"{assessmentType}.Enjoyment", StringComparison.OrdinalIgnoreCase)))
            {
                assessmentQuestionOverview.ActualQuestionsNumberPlusFeedback++;
                assessmentQuestionOverview.TotalQuestionsNumberPlusFeedback++;
            }
            return assessmentQuestionOverview;
        }

        /// <summary>
        /// Gets the assessment ListView model.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <param name="selectedJobs">The selected jobs.</param>
        /// <returns></returns>
        //public SkillsAssessmentListViewModel GetAssessmentListViewModel(long documentId,
        //    IEnumerable<string> selectedJobs = null,
        //    List<SHCWidgetSettings> editorSettings = null)
        //{
        //    var model = new SkillsAssessmentListViewModel
        //    {
        //        JobFamilyList = new JobFamilyList { SelectedJobs = selectedJobs ?? new List<string>() }
        //    };

        //    var apiResult = _skillsHealthCheckService.GetSkillsDocument(new GetSkillsDocumentRequest
        //    {
        //        DocumentId = documentId
        //    });

        //    if (apiResult.Success)
        //    {
        //        model.DateAssessmentsCreated = TimeZoneInfo.ConvertTimeFromUtc(
        //            apiResult.SkillsDocument.CreatedAt, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));

        //        if (!model.JobFamilyList.SelectedJobs.Any())
        //        {
        //            var newJobList = new List<string>();
        //            for (var i = 1; i < 4; i++)
        //            {
        //                var i1 = i;
        //                var jobFamilyDocValue =
        //                    apiResult.SkillsDocument.SkillsDocumentDataValues.FirstOrDefault(
        //                        docValue =>
        //                            docValue.Title.Equals(string.Format(Constants.SkillsHealthCheck.JobFamilyTitle, i1),
        //                                StringComparison.OrdinalIgnoreCase));

        //                if (!string.IsNullOrWhiteSpace(jobFamilyDocValue?.Value))
        //                {
        //                    newJobList.Add(jobFamilyDocValue.Value);
        //                }
        //            }
        //            model.JobFamilyList.SelectedJobs = newJobList;
        //        }

        //        var diagnosticReportDataValues = from dataValues in apiResult.SkillsDocument.SkillsDocumentDataValues
        //                                         where dataValues.Title.Contains(Constants.SkillsHealthCheck.SkillsAssessmentComplete) ||
        //                                               dataValues.Title.Contains(Constants.SkillsHealthCheck.InterestsAssessmentComplete) ||
        //                                               dataValues.Title.Contains(Constants.SkillsHealthCheck.PersonalAssessmentComplete) ||
        //                                               dataValues.Title.Contains(Constants.SkillsHealthCheck.MotivationAssessmentComplete) ||
        //                                               dataValues.Title.Contains(Constants.SkillsHealthCheck.NumericAssessmentComplete) ||
        //                                               dataValues.Title.Contains(Constants.SkillsHealthCheck.VerbalAssessmentComplete) ||
        //                                               dataValues.Title.Contains(Constants.SkillsHealthCheck.CheckingAssessmentComplete) ||
        //                                               dataValues.Title.Contains(Constants.SkillsHealthCheck.MechanicalAssessmentComplete) ||
        //                                               dataValues.Title.Contains(Constants.SkillsHealthCheck.SpatialAssessmentComplete) ||
        //                                               dataValues.Title.Contains(Constants.SkillsHealthCheck.AbstractAssessmentComplete) ||
        //                                               dataValues.Title.Contains(Constants.SkillsHealthCheck.QualificationProperty) ||
        //                                               dataValues.Title.Contains(Constants.SkillsHealthCheck.CheckingTypeProperty) ||
        //                                               dataValues.Title.Contains(Constants.SkillsHealthCheck.NumericTypeProperty)
        //                                         select dataValues;

        //        foreach (var dataValue in diagnosticReportDataValues)
        //        {
        //            CheckQuestionFromDiagnosticReport(dataValue);
        //        }

        //        var assessments = new List<AssessmentOverview>
        //        {
        //            new AssessmentOverview
        //            {
        //                Action = _skillsAssessmentStatus,
        //                AssessmentName = Constants.SkillsHealthCheck.SkillsAssessmentTitle,
        //                AssessmentCategory = Constants.SkillsHealthCheck.SkillsAssessmentCategory,
        //                Description = Constants.SkillsHealthCheck.SkillsAssessmentDescription,
        //                AssessmentDuration = Constants.SkillsHealthCheck.SkillsAssessmentTimeToComplete,
        //                AssessmentType = AssessmentType.SkillAreas,
        //                PersonalAssessment = true
        //            },
        //            new AssessmentOverview
        //            {
        //                Action = _interestsAssessmentStatus,
        //                AssessmentName = Constants.SkillsHealthCheck.InterestsAssessmentTitle,
        //                AssessmentCategory = Constants.SkillsHealthCheck.InterestsAssessmentCategory,
        //                Description = Constants.SkillsHealthCheck.InterestsAssessmentDescription,
        //                AssessmentDuration = Constants.SkillsHealthCheck.InterestsAssessmentTimeToComplete,
        //                AssessmentType = AssessmentType.Interest,
        //                PersonalAssessment = true
        //            },
        //            new AssessmentOverview
        //            {
        //                Action = _personalAssessmentStatus,
        //                AssessmentName = Constants.SkillsHealthCheck.PersonalAssessmentTitle,
        //                AssessmentCategory = Constants.SkillsHealthCheck.PersonalAssessmentCategory,
        //                Description = Constants.SkillsHealthCheck.PersonalAssessmentDescription,
        //                AssessmentDuration = Constants.SkillsHealthCheck.PersonalAssessmentTimeToComplete,
        //                AssessmentType = AssessmentType.Personal,
        //                PersonalAssessment = true
        //            },
        //            new AssessmentOverview
        //            {
        //                Action = _motivationAssessmentStatus,
        //                AssessmentName = Constants.SkillsHealthCheck.MotivationAssessmentTitle,
        //                AssessmentCategory = Constants.SkillsHealthCheck.MotivatioAssessmentCategory,
        //                Description = Constants.SkillsHealthCheck.MotivationAssessmentDescription,
        //                AssessmentDuration = Constants.SkillsHealthCheck.MotivationAssessmentTimeToComplete,
        //                AssessmentType = AssessmentType.Motivation,
        //                PersonalAssessment = true
        //            },
        //            new AssessmentOverview
        //            {
        //                Action = _numericAssesmentStatus,
        //                AssessmentName = Constants.SkillsHealthCheck.NumericAssessmentTitle,
        //                AssessmentCategory = Constants.SkillsHealthCheck.NumericAssessmentCategory,
        //                Description = Constants.SkillsHealthCheck.NumericAssessmentDescription,
        //                AssessmentDuration = Constants.SkillsHealthCheck.NumericAssessmentTimeToComplete,
        //                AssessmentType = AssessmentType.Numeric,
        //                ActivityAssessment = true
        //            },
        //            new AssessmentOverview
        //            {
        //                Action = _verbalAssessmentStatus,
        //                AssessmentName = Constants.SkillsHealthCheck.VerbalAssessmentTitle,
        //                AssessmentCategory = Constants.SkillsHealthCheck.VerbalAssessmentCategory,
        //                Description = Constants.SkillsHealthCheck.VerbalAssessmentDescription,
        //                AssessmentDuration = Constants.SkillsHealthCheck.VerbalAssessmentTimeToComplete,
        //                AssessmentType = AssessmentType.Verbal,
        //                ActivityAssessment = true
        //            },
        //            new AssessmentOverview
        //            {
        //                Action = _checkActivityAssessmentStatus,
        //                AssessmentName = Constants.SkillsHealthCheck.CheckingAssessmentTitle,
        //                AssessmentCategory = Constants.SkillsHealthCheck.CheckinAssessmentCategory,
        //                Description = Constants.SkillsHealthCheck.CheckingAssessmentDescription,
        //                AssessmentDuration = Constants.SkillsHealthCheck.CheckingAssessmentTimeToComplete,
        //                AssessmentType = AssessmentType.Checking,
        //                ActivityAssessment = true
        //            },
        //            new AssessmentOverview
        //            {
        //                Action = _mechanicalAssessmentStatus,
        //                AssessmentName = Constants.SkillsHealthCheck.MechanicalAssessmentTitle,
        //                AssessmentCategory = Constants.SkillsHealthCheck.MechanicalAssessmentCategory,
        //                Description = Constants.SkillsHealthCheck.MechanicalAssessmentDescription,
        //                AssessmentDuration = Constants.SkillsHealthCheck.MechanicalAssessmentTimeToComplete,
        //                AssessmentType = AssessmentType.Mechanical,
        //                ActivityAssessment = true
        //            },
        //            new AssessmentOverview
        //            {
        //                Action = _spatialActivityAssessmentStatus,
        //                AssessmentName = Constants.SkillsHealthCheck.SpatialAssessmentTitle,
        //                AssessmentCategory = Constants.SkillsHealthCheck.SpatialAssessmentCategory,
        //                Description = Constants.SkillsHealthCheck.SpatialAssessmentDescription,
        //                AssessmentDuration = Constants.SkillsHealthCheck.SpatialAssessmentTimeToComplete,
        //                AssessmentType = AssessmentType.Spatial,
        //                ActivityAssessment = true
        //            },
        //            new AssessmentOverview
        //            {
        //                Action = _abstractActivityAssessmentStatus,
        //                AssessmentName = Constants.SkillsHealthCheck.AbstractAssessmentTitle,
        //                AssessmentCategory = Constants.SkillsHealthCheck.AbstractAssessmentCategory,
        //                Description = Constants.SkillsHealthCheck.AbstractAssessmentDescription,
        //                AssessmentDuration = Constants.SkillsHealthCheck.AbstractAssessmentTimeToComplete,
        //                AssessmentType = AssessmentType.Abstract,
        //                ActivityAssessment = true
        //            }
        //        };

        //        model.SkillsAssessmentComplete =
        //            _skillsAssessmentStatus.Equals(Constants.SkillsHealthCheck.QuestionSetCompletedAction,
        //                StringComparison.OrdinalIgnoreCase);


        //        model.AssessmentsActivity = assessments.Where(assess => assess.ActivityAssessment).ToList();
        //        model.AssessmentsPersonal = assessments.Where(assess => assess.PersonalAssessment).ToList();

        //        model.AssessmentsStarted =
        //            assessments.Where(
        //                    assess => assess.Action.Equals(Constants.SkillsHealthCheck.QuestionSetStartedAction))
        //                .ToList();
        //        model.AssessmentsCompleted =
        //            assessments.Where(
        //                    assess => assess.Action.Equals(Constants.SkillsHealthCheck.QuestionSetCompletedAction))
        //                .ToList();

        //        ChangeActionText(editorSettings, model.AssessmentsActivity);
        //        ChangeActionText(editorSettings, model.AssessmentsPersonal);

        //        ChangeActionText(editorSettings, model.AssessmentsStarted);
        //        ChangeActionText(editorSettings, model.AssessmentsCompleted);
        //    }
        //    else
        //    {
        //        model.IsAPiError = true;
        //        Log.Writer.Write(apiResult.ErrorMessage, new List<string> { "ErrorLog" }, -1, 1, TraceEventType.Error);
        //        model.ApiErrorMessage = apiResult.ErrorMessage;
        //        model.InValidDocumentId = true;
        //    }
        //    return model;
        //}

        //private void ChangeActionText(List<SHCWidgetSettings> editorSettings, IList<AssessmentOverview> assessments)
        //{
        //    foreach (var assessment in assessments)
        //    {
        //        var settings = editorSettings?.SingleOrDefault(s => s.SHCAssessmentType == assessment.AssessmentType);
        //        if (settings != null)
        //        {
        //            var originalStatus = assessment.Action;
        //            switch (originalStatus)
        //            {
        //                case Constants.SkillsHealthCheck.QuestionSetNotStartedAction:
        //                    assessment.Action = string.IsNullOrEmpty(settings.StartLinkText) ? originalStatus : settings.StartLinkText;
        //                    break;

        //                case Constants.SkillsHealthCheck.QuestionSetStartedAction:
        //                    assessment.Action = string.IsNullOrEmpty(settings.ContinueLinkText) ? originalStatus : settings.ContinueLinkText;
        //                    break;

        //                case Constants.SkillsHealthCheck.QuestionSetCompletedAction:
        //                    assessment.Action = string.IsNullOrEmpty(settings.CompleteLinkText) ? originalStatus : settings.CompleteLinkText;
        //                    break;
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Checks the question from diagnostic report.
        /// </summary>
        /// <param name="dataValue">The data value.</param>
        private void CheckQuestionFromDiagnosticReport(SkillsDocumentDataValue dataValue)
        {
            // check if qualification level is already set
            if (
                string.Compare(dataValue.Title, Constants.SkillsHealthCheck.QualificationProperty,
                    StringComparison.OrdinalIgnoreCase) == 0)
            {
                if (string.IsNullOrEmpty(dataValue.Value))
                {
                    //what to do when no qualification has not been set
                }
            }
            else if (!string.IsNullOrEmpty(dataValue.Value))
            {
                // Check to see if they have selected an accessible version of the checking question set
                if (
                    string.Compare(dataValue.Title, Constants.SkillsHealthCheck.CheckingTypeProperty,
                        StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if (!string.IsNullOrEmpty(CheckQuestionAccessType))
                    {
                        if (Regex.IsMatch(dataValue.Value, CheckQuestionAccessType))
                        {
                        }
                    }
                }
                else if (
                    string.Compare(dataValue.Title, Constants.SkillsHealthCheck.NumericTypeProperty,
                        StringComparison.OrdinalIgnoreCase) == 0)
                {
                    // Check to see if they have selected an accessible version of the numeric question set
                    if (!string.IsNullOrEmpty(NumQuestionAccessibleType))
                    {
                        if (Regex.IsMatch(dataValue.Value, NumQuestionAccessibleType))
                        {
                        }
                    }
                }
                else
                {
                    //Set the statuses
                    SetStatusFromDiagnosticReport(dataValue);
                }
            }
        }

        /// <summary>
        /// Sets the status from diagnostic report.
        /// </summary>
        /// <param name="dataValue">The data value.</param>
        private void SetStatusFromDiagnosticReport(SkillsDocumentDataValue dataValue)
        {
            // check if there are any answers already given by user for skills area question set
            if (
                string.Compare(dataValue.Title, Constants.SkillsHealthCheck.SkillsAssessmentComplete,
                    StringComparison.OrdinalIgnoreCase) == 0)
            {
                if (string.Compare(dataValue.Value, bool.TrueString, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    _skillsAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetCompletedAction;
                }
                else
                {
                    _skillsAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetStartedAction;
                }
            }
            else if (
                string.Compare(dataValue.Title, Constants.SkillsHealthCheck.InterestsAssessmentComplete,
                    StringComparison.OrdinalIgnoreCase) == 0)
            {
                // check if there are any answers already given by user for interest area question set
                if (string.Compare(dataValue.Value, bool.TrueString, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    _interestsAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetCompletedAction;
                }
                else
                {
                    _interestsAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetStartedAction;
                }
            }
            else if (
                string.Compare(dataValue.Title, Constants.SkillsHealthCheck.PersonalAssessmentComplete,
                    StringComparison.OrdinalIgnoreCase) == 0)
            {
                // check if there are any answers already given by user for personal area question set
                if (string.Compare(dataValue.Value, bool.TrueString, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    _personalAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetCompletedAction;
                }
                else
                {
                    _personalAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetStartedAction;
                }
            }
            else if (
                string.Compare(dataValue.Title, Constants.SkillsHealthCheck.MotivationAssessmentComplete,
                    StringComparison.OrdinalIgnoreCase) == 0)
            {
                // check if there are any answers already given by user for motivational area question set
                if (String.Compare(dataValue.Value, bool.TrueString, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    _motivationAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetCompletedAction;
                }
                else
                {
                    _motivationAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetStartedAction;
                }
            }
            else if (
                string.Compare(dataValue.Title, Constants.SkillsHealthCheck.NumericAssessmentComplete,
                    StringComparison.OrdinalIgnoreCase) == 0)
            {
                // check if there are any answers already given by user for numerical assesment question set
                if (String.Compare(dataValue.Value, bool.TrueString, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    _numericAssesmentStatus = Constants.SkillsHealthCheck.QuestionSetCompletedAction;
                }
                else
                {
                    _numericAssesmentStatus = Constants.SkillsHealthCheck.QuestionSetStartedAction;
                }
            }
            else if (
                string.Compare(dataValue.Title, Constants.SkillsHealthCheck.VerbalAssessmentComplete,
                    StringComparison.OrdinalIgnoreCase) == 0)
            {
                // check if there are any answers already given by user for verbal assesment question set
                if (string.Compare(dataValue.Value, bool.TrueString, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    _verbalAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetCompletedAction;
                }
                else
                {
                    _verbalAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetStartedAction;
                }
            }
            else if (
                string.Compare(dataValue.Title, Constants.SkillsHealthCheck.CheckingAssessmentComplete,
                    StringComparison.OrdinalIgnoreCase) == 0)
            {
                // check if there are any answers already given by user for checking question set
                if (string.Compare(dataValue.Value, bool.TrueString, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    _checkActivityAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetCompletedAction;
                }
                else
                {
                    _checkActivityAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetStartedAction;
                }
            }
            else if (
                string.Compare(dataValue.Title, Constants.SkillsHealthCheck.MechanicalAssessmentComplete,
                    StringComparison.OrdinalIgnoreCase) == 0)
            {
                // check if there are any answers already given by user for mechanical question set
                if (String.Compare(dataValue.Value, bool.TrueString, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    _mechanicalAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetCompletedAction;
                }
                else
                {
                    _mechanicalAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetStartedAction;
                }
            }
            else if (
                string.Compare(dataValue.Title, Constants.SkillsHealthCheck.SpatialAssessmentComplete,
                    StringComparison.OrdinalIgnoreCase) == 0)
            {
                // check if there are any answers already given by user for spatial question set
                if (String.Compare(dataValue.Value, bool.TrueString, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    _spatialActivityAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetCompletedAction;
                }
                else
                {
                    _spatialActivityAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetStartedAction;
                }
            }
            else if (
                string.Compare(dataValue.Title, Constants.SkillsHealthCheck.AbstractAssessmentComplete,
                    StringComparison.OrdinalIgnoreCase) == 0)
            {
                // check if there are any answers already given by user for abstract question set
                if (string.Compare(dataValue.Value, bool.TrueString, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    _abstractActivityAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetCompletedAction;
                }
                else
                {
                    _abstractActivityAssessmentStatus = Constants.SkillsHealthCheck.QuestionSetStartedAction;
                }
            }
        }

        /// <summary>
        /// Gets the skills document.
        /// </summary>
        /// <param name="documentId">The document identifier.</param>
        /// <returns></returns>
        public SkillsDocument GetSkillsDocument(long documentId)
        {
            var apiResult = _skillsHealthCheckService.GetSkillsDocument(new GetSkillsDocumentRequest
            {
                DocumentId = documentId
            });

            if (apiResult.Success)
            {
                return apiResult.SkillsDocument;
            }
            return null;
        }

        /// <summary>
        /// Checks the and correct complete section.
        /// </summary>
        /// <param name="skillsDocument">The skills document.</param>
        /// <param name="dataValue">The data value.</param>
        /// <returns>An updated document</returns>
        //public SkillsDocument CheckAndCorrectCompleteSection(SkillsDocument skillsDocument,
        //    SkillsDocumentDataValue dataValue)
        //{
        //    if (
        //         string.Compare(dataValue.Title, Constants.SkillsHealthCheck.NumericAssessmentComplete,
        //             StringComparison.OrdinalIgnoreCase) == 0)
        //    {
        //        // check if there are any answers already given by user for numerical assesment question set
        //        if (dataValue.Value.Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase))
        //        {
        //            CheckAssessmentTypeDataValueAndCorrect(skillsDocument,
        //                AssessmentType.Numeric, Constants.SkillsHealthCheck.NumericAssessmentComplete);
        //        }
        //    }
        //    else if (
        //        string.Compare(dataValue.Title, Constants.SkillsHealthCheck.VerbalAssessmentComplete,
        //            StringComparison.OrdinalIgnoreCase) == 0)
        //    {
        //        // check if there are any answers already given by user for verbal assesment question set
        //        if (dataValue.Value.Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase))
        //        {
        //            CheckAssessmentTypeDataValueAndCorrect(skillsDocument, AssessmentType.Verbal,
        //                Constants.SkillsHealthCheck.VerbalAssessmentComplete);
        //        }
        //    }
        //    else if (
        //        string.Compare(dataValue.Title, Constants.SkillsHealthCheck.CheckingAssessmentComplete,
        //            StringComparison.OrdinalIgnoreCase) == 0)
        //    {
        //        // check if there are any answers already given by user for checking question set
        //        if (dataValue.Value.Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase))
        //        {
        //            CheckAssessmentTypeDataValueAndCorrect(skillsDocument,
        //                AssessmentType.Checking, Constants.SkillsHealthCheck.CheckingAssessmentComplete);
        //        }
        //    }
        //    else if (
        //        string.Compare(dataValue.Title, Constants.SkillsHealthCheck.MechanicalAssessmentComplete,
        //            StringComparison.OrdinalIgnoreCase) == 0)
        //    {
        //        // check if there are any answers already given by user for mechanical question set
        //        if (dataValue.Value.Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase))
        //        {
        //            CheckAssessmentTypeDataValueAndCorrect(skillsDocument,
        //                AssessmentType.Mechanical, Constants.SkillsHealthCheck.MechanicalAssessmentComplete);
        //        }
        //    }
        //    else if (
        //        string.Compare(dataValue.Title, Constants.SkillsHealthCheck.SpatialAssessmentComplete,
        //            StringComparison.OrdinalIgnoreCase) == 0)
        //    {
        //        // check if there are any answers already given by user for spatial question set
        //        if (dataValue.Value.Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase))
        //        {
        //            CheckAssessmentTypeDataValueAndCorrect(skillsDocument,
        //                AssessmentType.Spatial, Constants.SkillsHealthCheck.SpatialAssessmentComplete);
        //        }
        //    }
        //    else if (
        //        string.Compare(dataValue.Title, Constants.SkillsHealthCheck.AbstractAssessmentComplete,
        //            StringComparison.OrdinalIgnoreCase) == 0)
        //    {
        //        // check if there are any answers already given by user for abstract question set
        //        if (dataValue.Value.Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase))
        //        {
        //            CheckAssessmentTypeDataValueAndCorrect(skillsDocument,
        //                AssessmentType.Abstract, Constants.SkillsHealthCheck.AbstractAssessmentComplete);
        //        }
        //    }
        //    else if (
        //        string.Compare(dataValue.Title, Constants.SkillsHealthCheck.SkillsAssessmentComplete,
        //            StringComparison.OrdinalIgnoreCase) == 0)
        //    {
        //        // check if there are any answers already given by user for abstract question set
        //        if (dataValue.Value.Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase))
        //        {
        //            CheckAssessmentTypeDataValueAndCorrect(skillsDocument,
        //                AssessmentType.SkillAreas, Constants.SkillsHealthCheck.SkillsAssessmentComplete);
        //        }
        //    }
        //    else if (
        //      string.Compare(dataValue.Title, Constants.SkillsHealthCheck.InterestsAssessmentComplete,
        //          StringComparison.OrdinalIgnoreCase) == 0)
        //    {
        //        // check if there are any answers already given by user for spatial question set
        //        if (dataValue.Value.Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase))
        //        {
        //            CheckAssessmentTypeDataValueAndCorrect(skillsDocument,
        //                AssessmentType.Interest, Constants.SkillsHealthCheck.InterestsAssessmentComplete);
        //        }
        //    }
        //    else if (
        //        string.Compare(dataValue.Title, Constants.SkillsHealthCheck.PersonalAssessmentComplete,
        //            StringComparison.OrdinalIgnoreCase) == 0)
        //    {
        //        // check if there are any answers already given by user for abstract question set
        //        if (dataValue.Value.Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase))
        //        {
        //            CheckAssessmentTypeDataValueAndCorrect(skillsDocument,
        //                AssessmentType.Personal, Constants.SkillsHealthCheck.PersonalAssessmentComplete);
        //        }
        //    }
        //    else if (
        //        string.Compare(dataValue.Title, Constants.SkillsHealthCheck.MotivationAssessmentComplete,
        //            StringComparison.OrdinalIgnoreCase) == 0)
        //    {
        //        // check if there are any answers already given by user for abstract question set
        //        if (dataValue.Value.Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase))
        //        {
        //            CheckAssessmentTypeDataValueAndCorrect(skillsDocument,
        //                AssessmentType.Motivation, Constants.SkillsHealthCheck.MotivationAssessmentComplete);
        //        }
        //    }
        //    return skillsDocument;
        //}

        /// <summary>
        /// Checks the assessment type data value and update complete status.
        /// </summary>
        /// <param name="skillsDocument">The skills document.</param>
        /// <param name="assessmentType">Type of the assessment.</param>
        /// <param name="assessmentCompleteTitle">The assessment complete title.</param>
        //private void CheckAssessmentTypeDataValueAndCorrect(SkillsDocument skillsDocument, AssessmentType assessmentType,
        //    string assessmentCompleteTitle)
        //{
        //    var answersDataValue =
        //        skillsDocument.SkillsDocumentDataValues.FirstOrDefault(
        //            docValue =>
        //                docValue.Title.Equals(
        //                    assessmentCompleteTitle.Replace("Complete", "Answers")));
        //    if (answersDataValue != null)
        //    {
        //        var completedAnswers = answersDataValue.Value.Split(',').ToList();
        //        var assessmentOverview = GetAssessmentQuestionsOverview(Level.Level1, Accessibility.Full,
        //            assessmentType, skillsDocument);
        //        int expectedAnswerCount;
        //        switch (assessmentType)
        //        {
        //            case AssessmentType.Abstract:
        //            case AssessmentType.Spatial:
        //            case AssessmentType.Verbal:
        //            case AssessmentType.Mechanical:
        //            case AssessmentType.Numeric:
        //                expectedAnswerCount = assessmentOverview.TotalQuestionsNumber;
        //                break;

        //            case AssessmentType.Personal:
        //            case AssessmentType.SkillAreas:
        //            case AssessmentType.Checking:
        //            case AssessmentType.Interest:
        //            case AssessmentType.Motivation:
        //                expectedAnswerCount = assessmentOverview.ActualQuestionsNumber;
        //                break;

        //            default:
        //                throw new ArgumentOutOfRangeException(nameof(assessmentType), assessmentType, null);
        //        }

        //        if (expectedAnswerCount < completedAnswers.Count)
        //        {
        //            Log.Writer.Write(
        //              $"Correcting document id {skillsDocument.DocumentId} . Correcting {assessmentType} assesment. Supplied {completedAnswers.Count} answers whilst expecting {expectedAnswerCount}",
        //              new List<string> { nameof(ConfigurationPolicy.ErrorLog) }, -1,
        //              1, TraceEventType.Error);

        //            var updatedList = completedAnswers.Take(expectedAnswerCount);

        //            answersDataValue.Value = string.Join(",", updatedList);

        //            Log.Writer.Write(
        //                $"Completed correction of document id {skillsDocument.DocumentId} . Correcting {assessmentType} assesment. Supplied {completedAnswers.Count} answers whilst expecting {expectedAnswerCount}",
        //                new List<string> { nameof(ConfigurationPolicy.ErrorLog) }, -1,
        //                1, TraceEventType.Error);
        //        }
        //        else if (expectedAnswerCount > completedAnswers.Count)
        //        {
        //            Log.Writer.Write(
        //                $"Correcting document id {skillsDocument.DocumentId} . Correcting {assessmentType} assesment. Although marked as completed, Supplied {completedAnswers.Count} answers whilst expecting {expectedAnswerCount}",
        //                new List<string> { nameof(ConfigurationPolicy.ErrorLog) }, -1,
        //                1, TraceEventType.Error);

        //            var titleDataValue =
        //                skillsDocument.SkillsDocumentDataValues.FirstOrDefault(
        //                    docValue =>
        //                        docValue.Title.Equals(
        //                            assessmentCompleteTitle, StringComparison.InvariantCultureIgnoreCase));

        //            if (titleDataValue != null &&
        //                titleDataValue.Value.Equals(bool.TrueString, StringComparison.InvariantCultureIgnoreCase))
        //            {
        //                // Start - Reset Survey Questions
        //                var howLongDocValue =
        //                    skillsDocument.SkillsDocumentDataValues.FirstOrDefault(
        //                        docValue =>
        //                            docValue.Title.Equals($"{assessmentType}.Timing", StringComparison.OrdinalIgnoreCase));

        //                if (howLongDocValue != null)
        //                {
        //                    howLongDocValue.Value = string.Empty;
        //                }

        //                var howEasyDocValue =
        //                    skillsDocument.SkillsDocumentDataValues.FirstOrDefault(
        //                        docValue =>
        //                            docValue.Title.Equals($"{assessmentType}.Ease", StringComparison.OrdinalIgnoreCase));

        //                if (howEasyDocValue != null)
        //                {
        //                    howEasyDocValue.Value = string.Empty;
        //                }

        //                var howEnjoyableDocValue =
        //                    skillsDocument.SkillsDocumentDataValues.FirstOrDefault(
        //                        docValue =>
        //                            docValue.Title.Equals($"{assessmentType}.Enjoyment",
        //                                StringComparison.OrdinalIgnoreCase));

        //                if (howEnjoyableDocValue != null)
        //                {
        //                    howEnjoyableDocValue.Value = string.Empty;
        //                }

        //                // Done - Reset Survey Questions

        //                titleDataValue.Value = bool.FalseString;
        //                Log.Writer.Write(
        //                    $"Completed correction of document id {skillsDocument.DocumentId} . Correcting {assessmentType} assesment from 'Complete = True' to 'Complete = {titleDataValue.Value}', reset additional surver questions",
        //                    new List<string> { nameof(ConfigurationPolicy.ErrorLog) }, -1,
        //                    1, TraceEventType.Error);
        //            }
        //        }
        //    }
        //}
    }
}
