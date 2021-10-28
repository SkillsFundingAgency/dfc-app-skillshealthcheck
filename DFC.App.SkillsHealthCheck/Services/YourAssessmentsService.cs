using DFC.App.SkillsHealthCheck.Enums;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.ViewModels.YourAssessments;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DFC.App.SkillsHealthCheck.Services
{
    public class YourAssessmentsService : IYourAssessmentsService
    {
        private ISkillsHealthCheckService _skillsHealthCheckService;

        public YourAssessmentsService(ISkillsHealthCheckService skillsHealthCheckService)
        {
            _skillsHealthCheckService = skillsHealthCheckService;
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
                        AssessmentName = Constants.SkillsHealthCheck.SkillsAssessmentTitle,
                        AssessmentCategory = Constants.SkillsHealthCheck.SkillsAssessmentCategory,
                        Description = Constants.SkillsHealthCheck.SkillsAssessmentDescription,
                        AssessmentDuration = Constants.SkillsHealthCheck.SkillsAssessmentTimeToComplete,
                        AssessmentType = AssessmentType.SkillAreas,
                        PersonalAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = GetAssessmentOverviewAction(diagnosticReportDataValues, Constants.SkillsHealthCheck.InterestsAssessmentComplete),
                        AssessmentName = Constants.SkillsHealthCheck.InterestsAssessmentTitle,
                        AssessmentCategory = Constants.SkillsHealthCheck.InterestsAssessmentCategory,
                        Description = Constants.SkillsHealthCheck.InterestsAssessmentDescription,
                        AssessmentDuration = Constants.SkillsHealthCheck.InterestsAssessmentTimeToComplete,
                        AssessmentType = AssessmentType.Interest,
                        PersonalAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = GetAssessmentOverviewAction(diagnosticReportDataValues, Constants.SkillsHealthCheck.PersonalAssessmentComplete),
                        AssessmentName = Constants.SkillsHealthCheck.PersonalAssessmentTitle,
                        AssessmentCategory = Constants.SkillsHealthCheck.PersonalAssessmentCategory,
                        Description = Constants.SkillsHealthCheck.PersonalAssessmentDescription,
                        AssessmentDuration = Constants.SkillsHealthCheck.PersonalAssessmentTimeToComplete,
                        AssessmentType = AssessmentType.Personal,
                        PersonalAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = GetAssessmentOverviewAction(diagnosticReportDataValues, Constants.SkillsHealthCheck.MotivationAssessmentComplete),
                        AssessmentName = Constants.SkillsHealthCheck.MotivationAssessmentTitle,
                        AssessmentCategory = Constants.SkillsHealthCheck.MotivatioAssessmentCategory,
                        Description = Constants.SkillsHealthCheck.MotivationAssessmentDescription,
                        AssessmentDuration = Constants.SkillsHealthCheck.MotivationAssessmentTimeToComplete,
                        AssessmentType = AssessmentType.Motivation,
                        PersonalAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = GetAssessmentOverviewAction(diagnosticReportDataValues, Constants.SkillsHealthCheck.NumericAssessmentComplete),
                        AssessmentName = Constants.SkillsHealthCheck.NumericAssessmentTitle,
                        AssessmentCategory = Constants.SkillsHealthCheck.NumericAssessmentCategory,
                        Description = Constants.SkillsHealthCheck.NumericAssessmentDescription,
                        AssessmentDuration = Constants.SkillsHealthCheck.NumericAssessmentTimeToComplete,
                        AssessmentType = AssessmentType.Numeric,
                        ActivityAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = GetAssessmentOverviewAction(diagnosticReportDataValues, Constants.SkillsHealthCheck.VerbalAssessmentComplete),
                        AssessmentName = Constants.SkillsHealthCheck.VerbalAssessmentTitle,
                        AssessmentCategory = Constants.SkillsHealthCheck.VerbalAssessmentCategory,
                        Description = Constants.SkillsHealthCheck.VerbalAssessmentDescription,
                        AssessmentDuration = Constants.SkillsHealthCheck.VerbalAssessmentTimeToComplete,
                        AssessmentType = AssessmentType.Verbal,
                        ActivityAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = GetAssessmentOverviewAction(diagnosticReportDataValues, Constants.SkillsHealthCheck.CheckingAssessmentComplete),
                        AssessmentName = Constants.SkillsHealthCheck.CheckingAssessmentTitle,
                        AssessmentCategory = Constants.SkillsHealthCheck.CheckinAssessmentCategory,
                        Description = Constants.SkillsHealthCheck.CheckingAssessmentDescription,
                        AssessmentDuration = Constants.SkillsHealthCheck.CheckingAssessmentTimeToComplete,
                        AssessmentType = AssessmentType.Checking,
                        ActivityAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = GetAssessmentOverviewAction(diagnosticReportDataValues, Constants.SkillsHealthCheck.MechanicalAssessmentComplete),
                        AssessmentName = Constants.SkillsHealthCheck.MechanicalAssessmentTitle,
                        AssessmentCategory = Constants.SkillsHealthCheck.MechanicalAssessmentCategory,
                        Description = Constants.SkillsHealthCheck.MechanicalAssessmentDescription,
                        AssessmentDuration = Constants.SkillsHealthCheck.MechanicalAssessmentTimeToComplete,
                        AssessmentType = AssessmentType.Mechanical,
                        ActivityAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = GetAssessmentOverviewAction(diagnosticReportDataValues, Constants.SkillsHealthCheck.SpatialAssessmentComplete),
                        AssessmentName = Constants.SkillsHealthCheck.SpatialAssessmentTitle,
                        AssessmentCategory = Constants.SkillsHealthCheck.SpatialAssessmentCategory,
                        Description = Constants.SkillsHealthCheck.SpatialAssessmentDescription,
                        AssessmentDuration = Constants.SkillsHealthCheck.SpatialAssessmentTimeToComplete,
                        AssessmentType = AssessmentType.Spatial,
                        ActivityAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = GetAssessmentOverviewAction(diagnosticReportDataValues, Constants.SkillsHealthCheck.AbstractAssessmentComplete),
                        AssessmentName = Constants.SkillsHealthCheck.AbstractAssessmentTitle,
                        AssessmentCategory = Constants.SkillsHealthCheck.AbstractAssessmentCategory,
                        Description = Constants.SkillsHealthCheck.AbstractAssessmentDescription,
                        AssessmentDuration = Constants.SkillsHealthCheck.AbstractAssessmentTimeToComplete,
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
    }
}
