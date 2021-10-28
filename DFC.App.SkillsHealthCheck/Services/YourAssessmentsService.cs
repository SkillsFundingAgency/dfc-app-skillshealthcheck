using DFC.App.SkillsHealthCheck.Enums;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.ViewModels.YourAssessments;
using System;
using System.Collections.Generic;
using System.Linq;
using static DFC.App.SkillsHealthCheck.Constants;

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

        public BodyViewModel GetAssessmentListViewModel(long documentId)
        {
            // TODO: selected jobs not implemented as yet
            var model = new BodyViewModel
            {
                JobFamilyList = new JobFamilyList { SelectedJobs = new List<string>() },
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
    }
}
