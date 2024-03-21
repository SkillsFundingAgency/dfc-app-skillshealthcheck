using Azure;
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
        private ISkillsHealthCheckService skillsHealthCheckService;

        public YourAssessmentsService(
            ISkillsHealthCheckService skillsHealthCheckService,
            IQuestionService questionService)
        {
            this.questionService = questionService;
            this.skillsHealthCheckService = skillsHealthCheckService;
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

        public async Task<byte[]> GetDownloadDocumentAsync(SessionDataModel sessionDataModel, DocumentFormatter formatter, List<string> selectedJobs)
        {
            var documentId = sessionDataModel.DocumentId;

            var skillsDocument = await _skillsHealthCheckService.GetSkillsDocument((int)documentId);

            if (skillsDocument != null)
            {
                var downloadDocumentResponse = await GetDownloadDocumentAsync(sessionDataModel, skillsDocument, formatter, selectedJobs);
                return downloadDocumentResponse;
            }

            return null;
        }

        private async Task<byte[]> GetDownloadDocumentAsync(SessionDataModel sessionDataModel, DFC.SkillsCentral.Api.Domain.Models.SkillsDocument skillsDocument, DocumentFormatter formatter, List<string> selectedJobs,  bool retry = false)
        {
            var response = new DFC.SkillsCentral.Api.Domain.Models.SkillsDocument();
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

                response = await _skillsHealthCheckService.SaveSkillsDocument(skillsDocument);
            }

            if (response != null)
            {
                byte[] downloadResponse;
                if (formatter.Title == "Word")
                {
                    downloadResponse = await _skillsHealthCheckService.GenerateWordDoc((int)skillsDocument.Id);
                }
                else
                {
                    downloadResponse = await _skillsHealthCheckService.GeneratePDF((int)skillsDocument.Id);
                }

                if (downloadResponse != null)
                {
                    return downloadResponse;
                }
            }

            return null;
        }

        //private SaveQuestionAnswerResponse UpdateShcAssessmentStatusIfFoundErrorsInAssesmentDocument(SessionDataModel sessionDataModel, SaveQuestionAnswerResponse saveQuestionAnswerResponse, SkillsDocument skillsDocument)
        //{
        //    var diagnosticReportDataValues = skillsDocument.SkillsDocumentDataValues.Where(dv =>
        //        validDataValues.Any(vdv =>
        //            dv.Title.Contains(vdv.Key, StringComparison.InvariantCultureIgnoreCase)));

        //    // correct data issue
        //    foreach (var dataValue in diagnosticReportDataValues.Where(dv => dv.Value.Equals(bool.TrueString, StringComparison.InvariantCultureIgnoreCase)))
        //    {
        //        var validDataValue = validDataValues.First(vdv => vdv.Key.Equals(dataValue.Title, StringComparison.InvariantCultureIgnoreCase));
        //        CheckAssessmentTypeDataValueAndCorrect(sessionDataModel, skillsDocument, validDataValue.Value, validDataValue.Key);
        //    }

        //    saveQuestionAnswerResponse =
        //        _skillsHealthCheckService.SaveQuestionAnswer(new SaveQuestionAnswerRequest
        //        {
        //            DocumentId = skillsDocument.DocumentId,
        //            SkillsDocument = skillsDocument,
        //        });

        //    return saveQuestionAnswerResponse;
        //}

        private readonly Dictionary<string, AssessmentType> validDataValues = new Dictionary<string, AssessmentType>
        {
            { Constants.SkillsHealthCheck.NumericAssessmentComplete, AssessmentType.Numerical },
            { Constants.SkillsHealthCheck.VerbalAssessmentComplete, AssessmentType.Verbal },
            { Constants.SkillsHealthCheck.CheckingAssessmentComplete, AssessmentType.Checking },
            { Constants.SkillsHealthCheck.MechanicalAssessmentComplete, AssessmentType.Mechanical },
            { Constants.SkillsHealthCheck.SpatialAssessmentComplete, AssessmentType.Spatial },
            { Constants.SkillsHealthCheck.SkillsAssessmentComplete, AssessmentType.SkillAreas },
            { Constants.SkillsHealthCheck.InterestsAssessmentDataValue, AssessmentType.Interests },
            { Constants.SkillsHealthCheck.PersonalAssessmentComplete, AssessmentType.Personal },
            { Constants.SkillsHealthCheck.MotivationAssessmentComplete, AssessmentType.Motivation },
            { Constants.SkillsHealthCheck.AbstractAssessmentComplete, AssessmentType.Abstract },
        };

        // TODO: can we avoid having this service here?
        private IQuestionService questionService;

        //private async Task CheckAssessmentTypeDataValueAndCorrect(SessionDataModel sessionDataModel, DFC.SkillsCentral.Api.Domain.Models.SkillsDocument skillsDocument, AssessmentType assessmentType, string assessmentCompleteTitle)
        //{
        //    var answersDataValue = skillsDocument.DataValueKeys.FirstOrDefault(docValue => docValue.Key.Equals(assessmentCompleteTitle.Replace("Complete", "Answers")));
        //    if (answersDataValue.Value != null)
        //    {
        //        var completedAnswers = answersDataValue.Value.Split(',').ToList();
        //        var assessmentOverview = await _questionService.GetAssessmentQuestionsOverview(sessionDataModel, assessmentType, skillsDocument);
        //        int expectedAnswerCount;
        //        switch (assessmentType)
        //        {
        //            case AssessmentType.Abstract:
        //            case AssessmentType.Spatial:
        //            case AssessmentType.Verbal:
        //            case AssessmentType.Mechanical:
        //            case AssessmentType.Numerical:
        //                expectedAnswerCount = assessmentOverview.TotalQuestionsNumber;
        //                break;

        //            case AssessmentType.Personal:
        //            case AssessmentType.SkillAreas:
        //            case AssessmentType.Checking:
        //            case AssessmentType.Interests:
        //            case AssessmentType.Motivation:
        //                expectedAnswerCount = assessmentOverview.ActualQuestionsNumber;
        //                break;

        //            default:
        //                throw new ArgumentOutOfRangeException(assessmentType.ToString(), assessmentType, null);
        //        }

        //        if (expectedAnswerCount < completedAnswers.Count)
        //        {
        //            //Log.Writer.Write(
        //            //  $"Correcting document id {skillsDocument.DocumentId} . Correcting {assessmentType} assesment. Supplied {completedAnswers.Count} answers whilst expecting {expectedAnswerCount}",
        //            //  new List<string> { nameof(ConfigurationPolicy.ErrorLog) }, -1,
        //            //  1, TraceEventType.Error);

        //            var updatedList = completedAnswers.Take(expectedAnswerCount);

        //            skillsDocument.DataValueKeys[answersDataValue.Key] = string.Join(",", updatedList);

        //            //Log.Writer.Write(
        //            //    $"Completed correction of document id {skillsDocument.DocumentId} . Correcting {assessmentType} assesment. Supplied {completedAnswers.Count} answers whilst expecting {expectedAnswerCount}",
        //            //    new List<string> { nameof(ConfigurationPolicy.ErrorLog) }, -1,
        //            //    1, TraceEventType.Error);
        //        }
        //        else if (expectedAnswerCount > completedAnswers.Count)
        //        {
        //            //Log.Writer.Write(
        //            //    $"Correcting document id {skillsDocument.DocumentId} . Correcting {assessmentType} assesment. Although marked as completed, Supplied {completedAnswers.Count} answers whilst expecting {expectedAnswerCount}",
        //            //    new List<string> { nameof(ConfigurationPolicy.ErrorLog) }, -1,
        //            //    1, TraceEventType.Error);

        //            var titleDataValue =
        //                skillsDocument.DataValueKeys.FirstOrDefault(
        //                    docValue =>
        //                        docValue.Key.Equals(
        //                            assessmentCompleteTitle, StringComparison.InvariantCultureIgnoreCase));

        //            if (titleDataValue.Value != null &&
        //                titleDataValue.Value.Equals(bool.TrueString, StringComparison.InvariantCultureIgnoreCase))
        //            {
        //                // Start - Reset Survey Questions
        //                var howLongDocValue =
        //                    skillsDocument.DataValueKeys.FirstOrDefault(
        //                        docValue =>
        //                            docValue.Key.Equals($"{assessmentType}.Timing", StringComparison.OrdinalIgnoreCase));

        //                if (howLongDocValue.Value != null)
        //                {
        //                    skillsDocument.DataValueKeys[howLongDocValue.Key] = string.Empty;
        //                }

        //                var howEasyDocValue =
        //                    skillsDocument.DataValueKeys.FirstOrDefault(
        //                        docValue =>
        //                            docValue.Key.Equals($"{assessmentType}.Ease", StringComparison.OrdinalIgnoreCase));

        //                if (howEasyDocValue.Value != null)
        //                {
        //                    skillsDocument.DataValueKeys[howEasyDocValue.Key] = string.Empty;
        //                }

        //                var howEnjoyableDocValue =
        //                    skillsDocument.DataValueKeys.FirstOrDefault(
        //                        docValue =>
        //                            docValue.Key.Equals($"{assessmentType}.Enjoyment",
        //                                StringComparison.OrdinalIgnoreCase));

        //                if (howEnjoyableDocValue.Value != null)
        //                {
        //                    skillsDocument.DataValueKeys[howEnjoyableDocValue.Key] = string.Empty;
        //                }

        //                // Done - Reset Survey Questions

        //                skillsDocument.DataValueKeys[titleDataValue.Key] = bool.FalseString;
        //                //Log.Writer.Write(
        //                //    $"Completed correction of document id {skillsDocument.DocumentId} . Correcting {assessmentType} assesment from 'Complete = True' to 'Complete = {titleDataValue.Value}', reset additional surver questions",
        //                //    new List<string> { nameof(ConfigurationPolicy.ErrorLog) }, -1,
        //                //    1, TraceEventType.Error);
        //            }
        //        }
        //    }
        //}

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

        public async Task<BodyViewModel> GetAssessmentListViewModel(long documentId, IEnumerable<string> selectedJobs = null)
        {
            var model = new BodyViewModel
            {
                JobFamilyList = new JobFamilyList { SelectedJobs = selectedJobs ?? new List<string>() },
            };

            var apiResult = await _skillsHealthCheckService.GetSkillsDocument((int)documentId);

            if (apiResult != null)
            {
                model.DateAssessmentsCreated = TimeZoneInfo.ConvertTimeFromUtc(
                    (DateTime)apiResult.CreatedAt, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));

                if (!model.JobFamilyList.SelectedJobs.Any())
                {
                    var newJobList = new List<string>();
                    for (var i = 1; i < 4; i++)
                    {
                        var i1 = i;
                        var jobFamilyDocValue =
                            apiResult.DataValueKeys.FirstOrDefault(
                                docValue =>
                                    docValue.Key.Equals(string.Format(Constants.SkillsHealthCheck.JobFamilyTitle, i1),
                                        StringComparison.OrdinalIgnoreCase));

                        if (!string.IsNullOrWhiteSpace(jobFamilyDocValue.Value))
                        {
                            newJobList.Add(jobFamilyDocValue.Value);
                        }
                    }

                    model.JobFamilyList.SelectedJobs = newJobList;
                }

                var diagnosticReportDataValues =
                    apiResult.DataValueKeys.ToDictionary(k => k.Key, v => v.Value);

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
                        AssessmentType = AssessmentType.Interests,
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
                        AssessmentType = AssessmentType.Numerical,
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
                if(diagnosticReportDataValues.ContainsKey(Constants.SkillsHealthCheck.SkillsAssessmentComplete))
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
                model.ApiErrorMessage = "Unable to retrieve skills document";
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
            var response = await _skillsHealthCheckService.GetSkillsDocumentByReferenceCode(referenceId);
            if (response != null && response.Id > 0)
            {
                sessionDataModel.DocumentId = (long)response.Id;
                return true;
            }

            return false;
        }
    }
}
