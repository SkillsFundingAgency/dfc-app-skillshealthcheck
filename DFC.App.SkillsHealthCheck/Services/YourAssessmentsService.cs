using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Helpers;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
using DFC.App.SkillsHealthCheck.ViewModels.YourAssessments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DFC.App.SkillsHealthCheck.Constants;

namespace DFC.App.SkillsHealthCheck.Services
{
    public class YourAssessmentsService : IYourAssessmentsService
    {
        private ISkillsHealthCheckService _skillsHealthCheckService;
        private IUserAssetService _userAssetService;

        public YourAssessmentsService(
            ISkillsHealthCheckService skillsHealthCheckService,
            IUserAssetService userAssetService,
            IQuestionService questionService)
        {
            _questionService = questionService;
            _userAssetService = userAssetService;
            _skillsHealthCheckService = skillsHealthCheckService;
        }

        public DocumentFormatter GetFormatter(DownloadType downloadType)
        {
            return downloadType == DownloadType.Pdf
                ? new DocumentFormatter
                {
                    Title = DocumentTitle.Pdf,
                    FileExtension = DocumentFileExtensions.Pdf,
                    ContentType = DocumentContentTypes.Pdf,
                    FormatterName = DocumentFormatName.ShcFullPdfFormatter,
                }
                : new DocumentFormatter
                {
                    Title = DocumentTitle.Word,
                    FileExtension = DocumentFileExtensions.Docx,
                    ContentType = DocumentContentTypes.Docx,
                    FormatterName = DocumentFormatName.ShcFullDocxFormatter,
                };
        }

        public DownloadDocumentResponse GetDownloadDocument(SessionDataModel sessionDataModel, DocumentFormatter formatter, List<string> selectedJobs, out string documentTitle)
        {
            var documentId = sessionDataModel.DocumentId;

            var skillsDocumentResponse = _skillsHealthCheckService.GetSkillsDocument(new GetSkillsDocumentRequest
            {
                DocumentId = documentId,
            });

            if (skillsDocumentResponse.Success)
            {
                var downloadDocumentResponse = GetDownloadDocument(sessionDataModel, skillsDocumentResponse, formatter, selectedJobs);
                documentTitle = skillsDocumentResponse.SkillsDocument.SkillsDocumentTitle;
                return downloadDocumentResponse;
            }

            documentTitle = string.Empty;
            return new DownloadDocumentResponse
            {
                Success = false,
            };
        }

        private DownloadDocumentResponse GetDownloadDocument(SessionDataModel sessionDataModel, GetSkillsDocumentResponse documentResponse, DocumentFormatter formatter, List<string> selectedJobs,  bool retry = false)
        {
            var saveQuestionAnswerResponse = new SaveQuestionAnswerResponse {Success = true};
            var skillsDocument = documentResponse.SkillsDocument;

            if (selectedJobs.Any())
            {
                for (var clearJob = 1; clearJob < 4; clearJob++)
                {
                    skillsDocument = skillsDocument.UpdateJobFamilyDataValue(clearJob, string.Empty);
                }

                var jobNumber = 1;
                foreach (var selectedJob in selectedJobs)
                {
                    skillsDocument = skillsDocument.UpdateJobFamilyDataValue(jobNumber++, selectedJob);
                }

                saveQuestionAnswerResponse = _skillsHealthCheckService.SaveQuestionAnswer(new SaveQuestionAnswerRequest
                {
                    DocumentId = sessionDataModel.DocumentId,
                    SkillsDocument = skillsDocument,
                });
            }

            if (saveQuestionAnswerResponse.Success)
            {
                var result = _userAssetService.RequestDownload(skillsDocument.DocumentId, formatter.FormatterName, skillsDocument.CreatedBy);

                while (new[] {"Pending", "Creating"}.Contains(result, StringComparer.OrdinalIgnoreCase))
                {
                    Task.WaitAll(Task.Delay(1000));
                    result = _userAssetService.QueryDownloadStatus(skillsDocument.DocumentId, formatter.FormatterName);
                }

                if (result.Equals("Created", StringComparison.OrdinalIgnoreCase))
                {
                    var downloadRequest = new DownloadDocumentRequest
                    {
                        DocumentId = skillsDocument.DocumentId,
                        Formatter = formatter.FormatterName,
                    };

                    var downloadResponse =
                        Task.Run(() => _skillsHealthCheckService.DownloadDocument(downloadRequest)).Result;

                    if (downloadResponse.Success)
                    {
                        return downloadResponse;
                        //UpdateShcUsageDate();
                    }
                }

                if (result.Equals("Error", StringComparison.OrdinalIgnoreCase) && !retry)
                {
                    saveQuestionAnswerResponse = UpdateShcAssessmentStatusIfFoundErrorsInAssesmentDocument(sessionDataModel, saveQuestionAnswerResponse, skillsDocument);
                    if (saveQuestionAnswerResponse.Success)
                    {
                        return GetDownloadDocument(sessionDataModel, documentResponse, formatter, selectedJobs, true);
                    }
                }
            }

            return new DownloadDocumentResponse
            {
                Success = false,
            };
        }

        private SaveQuestionAnswerResponse UpdateShcAssessmentStatusIfFoundErrorsInAssesmentDocument(SessionDataModel sessionDataModel, SaveQuestionAnswerResponse saveQuestionAnswerResponse, SkillsDocument skillsDocument)
        {
            var diagnosticReportDataValues = skillsDocument.SkillsDocumentDataValues.Where(dv =>
                validDataValues.Any(vdv =>
                    dv.Title.Contains(vdv.Key, StringComparison.InvariantCultureIgnoreCase)));

            // correct data issue
            foreach (var dataValue in diagnosticReportDataValues.Where(dv => dv.Value.Equals(bool.TrueString, StringComparison.InvariantCultureIgnoreCase)))
            {
                var validDataValue = validDataValues.First(vdv => vdv.Key.Equals(dataValue.Title, StringComparison.InvariantCultureIgnoreCase));
                CheckAssessmentTypeDataValueAndCorrect(sessionDataModel, skillsDocument, validDataValue.Value, validDataValue.Key);
            }

            saveQuestionAnswerResponse =
                _skillsHealthCheckService.SaveQuestionAnswer(new SaveQuestionAnswerRequest
                {
                    DocumentId = skillsDocument.DocumentId,
                    SkillsDocument = skillsDocument,
                });

            return saveQuestionAnswerResponse;
        }

        private readonly Dictionary<string, AssessmentType> validDataValues = new Dictionary<string, AssessmentType>
        {
            {Constants.SkillsHealthCheck.NumericAssessmentComplete, AssessmentType.Numeric},
            {Constants.SkillsHealthCheck.VerbalAssessmentComplete, AssessmentType.Numeric},
            {Constants.SkillsHealthCheck.CheckingAssessmentComplete, AssessmentType.Numeric},
            {Constants.SkillsHealthCheck.MechanicalAssessmentComplete, AssessmentType.Numeric},
            {Constants.SkillsHealthCheck.SpatialAssessmentComplete, AssessmentType.Numeric},
            {Constants.SkillsHealthCheck.SkillsAssessmentComplete, AssessmentType.Numeric},
            {Constants.SkillsHealthCheck.InterestsAssessmentDataValue, AssessmentType.Numeric},
            {Constants.SkillsHealthCheck.PersonalAssessmentComplete, AssessmentType.Numeric},
            {Constants.SkillsHealthCheck.MotivationAssessmentComplete, AssessmentType.Numeric},
            {Constants.SkillsHealthCheck.AbstractAssessmentComplete, AssessmentType.Numeric},
        };

        // TODO: can we avoid having this service here?
        private IQuestionService _questionService;

        private void CheckAssessmentTypeDataValueAndCorrect(SessionDataModel sessionDataModel, SkillsDocument skillsDocument, AssessmentType assessmentType, string assessmentCompleteTitle)
        {
            var answersDataValue = skillsDocument.SkillsDocumentDataValues.FirstOrDefault(docValue => docValue.Title.Equals(assessmentCompleteTitle.Replace("Complete", "Answers")));
            if (answersDataValue != null)
            {
                var completedAnswers = answersDataValue.Value.Split(',').ToList();
                var assessmentOverview = _questionService.GetAssessmentQuestionsOverview(sessionDataModel, Level.Level1, Accessibility.Full, assessmentType, skillsDocument);
                int expectedAnswerCount;
                switch (assessmentType)
                {
                    case AssessmentType.Abstract:
                    case AssessmentType.Spatial:
                    case AssessmentType.Verbal:
                    case AssessmentType.Mechanical:
                    case AssessmentType.Numeric:
                        expectedAnswerCount = assessmentOverview.TotalQuestionsNumber;
                        break;

                    case AssessmentType.Personal:
                    case AssessmentType.SkillAreas:
                    case AssessmentType.Checking:
                    case AssessmentType.Interest:
                    case AssessmentType.Motivation:
                        expectedAnswerCount = assessmentOverview.ActualQuestionsNumber;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(assessmentType), assessmentType, null);
                }

                if (expectedAnswerCount < completedAnswers.Count)
                {
                    //Log.Writer.Write(
                    //  $"Correcting document id {skillsDocument.DocumentId} . Correcting {assessmentType} assesment. Supplied {completedAnswers.Count} answers whilst expecting {expectedAnswerCount}",
                    //  new List<string> { nameof(ConfigurationPolicy.ErrorLog) }, -1,
                    //  1, TraceEventType.Error);

                    var updatedList = completedAnswers.Take(expectedAnswerCount);

                    answersDataValue.Value = string.Join(",", updatedList);

                    //Log.Writer.Write(
                    //    $"Completed correction of document id {skillsDocument.DocumentId} . Correcting {assessmentType} assesment. Supplied {completedAnswers.Count} answers whilst expecting {expectedAnswerCount}",
                    //    new List<string> { nameof(ConfigurationPolicy.ErrorLog) }, -1,
                    //    1, TraceEventType.Error);
                }
                else if (expectedAnswerCount > completedAnswers.Count)
                {
                    //Log.Writer.Write(
                    //    $"Correcting document id {skillsDocument.DocumentId} . Correcting {assessmentType} assesment. Although marked as completed, Supplied {completedAnswers.Count} answers whilst expecting {expectedAnswerCount}",
                    //    new List<string> { nameof(ConfigurationPolicy.ErrorLog) }, -1,
                    //    1, TraceEventType.Error);

                    var titleDataValue =
                        skillsDocument.SkillsDocumentDataValues.FirstOrDefault(
                            docValue =>
                                docValue.Title.Equals(
                                    assessmentCompleteTitle, StringComparison.InvariantCultureIgnoreCase));

                    if (titleDataValue != null &&
                        titleDataValue.Value.Equals(bool.TrueString, StringComparison.InvariantCultureIgnoreCase))
                    {
                        // Start - Reset Survey Questions
                        var howLongDocValue =
                            skillsDocument.SkillsDocumentDataValues.FirstOrDefault(
                                docValue =>
                                    docValue.Title.Equals($"{assessmentType}.Timing", StringComparison.OrdinalIgnoreCase));

                        if (howLongDocValue != null)
                        {
                            howLongDocValue.Value = string.Empty;
                        }

                        var howEasyDocValue =
                            skillsDocument.SkillsDocumentDataValues.FirstOrDefault(
                                docValue =>
                                    docValue.Title.Equals($"{assessmentType}.Ease", StringComparison.OrdinalIgnoreCase));

                        if (howEasyDocValue != null)
                        {
                            howEasyDocValue.Value = string.Empty;
                        }

                        var howEnjoyableDocValue =
                            skillsDocument.SkillsDocumentDataValues.FirstOrDefault(
                                docValue =>
                                    docValue.Title.Equals($"{assessmentType}.Enjoyment",
                                        StringComparison.OrdinalIgnoreCase));

                        if (howEnjoyableDocValue != null)
                        {
                            howEnjoyableDocValue.Value = string.Empty;
                        }

                        // Done - Reset Survey Questions

                        titleDataValue.Value = bool.FalseString;
                        //Log.Writer.Write(
                        //    $"Completed correction of document id {skillsDocument.DocumentId} . Correcting {assessmentType} assesment from 'Complete = True' to 'Complete = {titleDataValue.Value}', reset additional surver questions",
                        //    new List<string> { nameof(ConfigurationPolicy.ErrorLog) }, -1,
                        //    1, TraceEventType.Error);
                    }
                }
            }
        }

        private string GetAssessmentOverviewAction(Dictionary<string, string> diagnosticReportDataValues, string key)
        {
            var dataValue =
                diagnosticReportDataValues.FirstOrDefault(d => d.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));

            if (string.IsNullOrWhiteSpace(dataValue.Value))
            {
                return Constants.SkillsHealthCheck.QuestionSetNotStartedAction;
            }

            return bool.Parse(dataValue.Value)
                ? Constants.SkillsHealthCheck.QuestionSetCompletedAction
                : Constants.SkillsHealthCheck.QuestionSetStartedAction;
        }

        public BodyViewModel GetAssessmentListViewModel(long documentId, IEnumerable<string> selectedJobs = null)
        {
            // TODO: selected jobs not implemented as yet
            var model = new BodyViewModel
            {
                JobFamilyList = new JobFamilyList { SelectedJobs = selectedJobs ?? new List<string>() },
            };

            var apiResult = _skillsHealthCheckService.GetSkillsDocument(new GetSkillsDocumentRequest
            {
                DocumentId = documentId,
            });

            if (apiResult.Success)
            {
                model.DateAssessmentsCreated = TimeZoneInfo.ConvertTimeFromUtc(
                    apiResult.SkillsDocument.CreatedAt, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));

                if (!model.JobFamilyList.SelectedJobs.Any())
                {
                    var newJobList = new List<string>();
                    for (var i = 1; i < 4; i++)
                    {
                        var i1 = i;
                        var jobFamilyDocValue =
                            apiResult.SkillsDocument.SkillsDocumentDataValues.FirstOrDefault(
                                docValue =>
                                    docValue.Title.Equals(string.Format(Constants.SkillsHealthCheck.JobFamilyTitle, i1),
                                        StringComparison.OrdinalIgnoreCase));

                        if (!string.IsNullOrWhiteSpace(jobFamilyDocValue?.Value))
                        {
                            newJobList.Add(jobFamilyDocValue.Value);
                        }
                    }

                    model.JobFamilyList.SelectedJobs = newJobList;
                }

                var diagnosticReportDataValues =
                    apiResult.SkillsDocument.SkillsDocumentDataValues.ToDictionary(k => k.Title, v => v.Value);

                var assessments = new List<AssessmentOverview>
                {
                    new AssessmentOverview
                    {
                        Action = GetAssessmentOverviewAction(diagnosticReportDataValues, Constants.SkillsHealthCheck.SkillsAssessmentComplete),
                        AssessmentName = Assessments.Skills.Title,
                        AssessmentCategory = Assessments.Skills.Category,
                        Description = Assessments.Skills.Description,
                        AssessmentDuration = Assessments.Skills.TimeToComplete,
                        AssessmentType = AssessmentType.SkillAreas,
                        PersonalAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = GetAssessmentOverviewAction(diagnosticReportDataValues, Constants.SkillsHealthCheck.InterestsAssessmentComplete),
                        AssessmentName = Assessments.Interests.Title,
                        AssessmentCategory = Assessments.Interests.Category,
                        Description = Assessments.Interests.Description,
                        AssessmentDuration = Assessments.Interests.TimeToComplete,
                        AssessmentType = AssessmentType.Interest,
                        PersonalAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = GetAssessmentOverviewAction(diagnosticReportDataValues, Constants.SkillsHealthCheck.PersonalAssessmentComplete),
                        AssessmentName = Assessments.Personal.Title,
                        AssessmentCategory = Assessments.Personal.Category,
                        Description = Assessments.Personal.Description,
                        AssessmentDuration = Assessments.Personal.TimeToComplete,
                        AssessmentType = AssessmentType.Personal,
                        PersonalAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = GetAssessmentOverviewAction(diagnosticReportDataValues, Constants.SkillsHealthCheck.MotivationAssessmentComplete),
                        AssessmentName = Assessments.Motivation.Title,
                        AssessmentCategory = Assessments.Motivation.Category,
                        Description = Assessments.Motivation.Description,
                        AssessmentDuration = Assessments.Motivation.TimeToComplete,
                        AssessmentType = AssessmentType.Motivation,
                        PersonalAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = GetAssessmentOverviewAction(diagnosticReportDataValues, Constants.SkillsHealthCheck.NumericAssessmentComplete),
                        AssessmentName = Assessments.Numeric.Title,
                        AssessmentCategory = Assessments.Numeric.Category,
                        Description = Assessments.Numeric.Description,
                        AssessmentDuration = Assessments.Numeric.TimeToComplete,
                        AssessmentType = AssessmentType.Numeric,
                        ActivityAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = GetAssessmentOverviewAction(diagnosticReportDataValues, Constants.SkillsHealthCheck.VerbalAssessmentComplete),
                        AssessmentName = Assessments.Verbal.Title,
                        AssessmentCategory = Assessments.Verbal.Category,
                        Description = Assessments.Verbal.Description,
                        AssessmentDuration = Assessments.Verbal.TimeToComplete,
                        AssessmentType = AssessmentType.Verbal,
                        ActivityAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = GetAssessmentOverviewAction(diagnosticReportDataValues, Constants.SkillsHealthCheck.CheckingAssessmentComplete),
                        AssessmentName = Assessments.Checking.Title,
                        AssessmentCategory = Assessments.Checking.Category,
                        Description = Assessments.Checking.Description,
                        AssessmentDuration = Assessments.Checking.TimeToComplete,
                        AssessmentType = AssessmentType.Checking,
                        ActivityAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = GetAssessmentOverviewAction(diagnosticReportDataValues, Constants.SkillsHealthCheck.MechanicalAssessmentComplete),
                        AssessmentName = Assessments.Mechanical.Title,
                        AssessmentCategory = Assessments.Mechanical.Category,
                        Description = Assessments.Mechanical.Description,
                        AssessmentDuration = Assessments.Mechanical.TimeToComplete,
                        AssessmentType = AssessmentType.Mechanical,
                        ActivityAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = GetAssessmentOverviewAction(diagnosticReportDataValues, Constants.SkillsHealthCheck.SpatialAssessmentComplete),
                        AssessmentName = Assessments.Spatial.Title,
                        AssessmentCategory = Assessments.Spatial.Category,
                        Description = Assessments.Spatial.Description,
                        AssessmentDuration = Assessments.Spatial.TimeToComplete,
                        AssessmentType = AssessmentType.Spatial,
                        ActivityAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = GetAssessmentOverviewAction(diagnosticReportDataValues, Constants.SkillsHealthCheck.AbstractAssessmentComplete),
                        AssessmentName = Assessments.Abstract.Title,
                        AssessmentCategory = Assessments.Abstract.Category,
                        Description = Assessments.Abstract.Description,
                        AssessmentDuration = Assessments.Abstract.TimeToComplete,
                        AssessmentType = AssessmentType.Abstract,
                        ActivityAssessment = true,
                    },
                };

                model.SkillsAssessmentComplete = diagnosticReportDataValues.FirstOrDefault(d => d.Key.Equals(Constants.SkillsHealthCheck.SkillsAssessmentComplete, StringComparison.InvariantCultureIgnoreCase)).Value.Equals(bool.TrueString, StringComparison.InvariantCultureIgnoreCase);

                model.AssessmentsActivity = assessments.Where(assess => assess.ActivityAssessment).ToList();
                model.AssessmentsPersonal = assessments.Where(assess => assess.PersonalAssessment).ToList();

                model.AssessmentsStarted =
                    assessments.Where(
                            assess => assess.Action.Equals(Constants.SkillsHealthCheck.QuestionSetStartedAction))
                        .ToList();
                model.AssessmentsCompleted =
                    assessments.Where(
                            assess => assess.Action.Equals(Constants.SkillsHealthCheck.QuestionSetCompletedAction))
                        .ToList();

                ChangeActionText(model.AssessmentsActivity);
                ChangeActionText(model.AssessmentsPersonal);

                ChangeActionText(model.AssessmentsStarted);
                ChangeActionText(model.AssessmentsCompleted);
            }
            else
            {
                model.IsAPiError = true;
                model.ApiErrorMessage = apiResult.ErrorMessage;
                model.InValidDocumentId = true;
            }

            return model;
        }

        private void ChangeActionText(IList<AssessmentOverview> assessments)
        {
            foreach (var assessment in assessments)
            {
                var settings = SHCWidgetSettings.SHCWidgetSettingsList.SingleOrDefault(s => s.SHCAssessmentType == assessment.AssessmentType);
                if (settings != null)
                {
                    var originalStatus = assessment.Action;
                    switch (originalStatus)
                    {
                        case Constants.SkillsHealthCheck.QuestionSetNotStartedAction:
                            assessment.Action = string.IsNullOrEmpty(settings.StartLinkText) ? originalStatus : settings.StartLinkText;
                            break;

                        case Constants.SkillsHealthCheck.QuestionSetStartedAction:
                            assessment.Action = string.IsNullOrEmpty(settings.ContinueLinkText) ? originalStatus : settings.ContinueLinkText;
                            break;

                        case Constants.SkillsHealthCheck.QuestionSetCompletedAction:
                            assessment.Action = string.IsNullOrEmpty(settings.CompleteLinkText) ? originalStatus : settings.CompleteLinkText;
                            break;
                    }
                }
            }
        }

        public async Task<bool> GetSkillsDocumentIDByReferenceAndStore(SessionDataModel sessionDataModel, string referenceId)
        {
            var response = _skillsHealthCheckService.GetSkillsDocumentByIdentifier(referenceId);
            if (response.Success && response.DocumentId > 0)
            {
                sessionDataModel.DocumentId = response.DocumentId;
                return true;
            }

            return false;
        }
    }
}
