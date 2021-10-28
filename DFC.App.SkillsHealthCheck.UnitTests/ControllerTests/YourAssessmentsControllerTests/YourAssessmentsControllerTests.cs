using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Controllers;
using DFC.App.SkillsHealthCheck.Enums;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.ViewModels;
using DFC.App.SkillsHealthCheck.ViewModels.YourAssessments;
using DFC.Compui.Sessionstate;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;

using Xunit;
using static DFC.App.SkillsHealthCheck.Constants;

namespace DFC.App.SkillsHealthCheck.UnitTests.ControllerTests.YourAssessmentsControllerTests
{
    [Trait("Category", "Your Assessements Unit Tests")]
    public class YourAssessmentsControllerTests : BaseYourAssessmentsControllerTests
    {
        [Fact]
        public void YourAssessmentsControllerHtmlHeadRequestReturnsSuccess()
        {
            using var controller = BuildHomeController(MediaTypeNames.Text.Html);

            var result = controller.HtmlHead();

            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<HtmlHeadViewModel>(viewResult.ViewData.Model);
            Assert.Equal($"{YourAssessmentsController.PageTitle} | {YourAssessmentsController.DefaultPageTitleSuffix}", viewModel.Title);
        }

        [Fact]
        public void YourAssessmentsControllerBreadCrumbRequestReturnsSuccess()
        {
            using var controller = BuildHomeController(MediaTypeNames.Text.Html);

            var result = controller.Breadcrumb();

            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<BreadcrumbViewModel>(viewResult.ViewData.Model);
            Assert.NotNull(viewModel.Breadcrumbs);
            Assert.Single(viewModel.Breadcrumbs);
            Assert.Equal("Home", viewModel.Breadcrumbs[0].Title);
        }

        [Fact]
        public async Task YourAssessmentsControllerBodyRequestReturnsSuccess()
        {
            using var controller = BuildHomeController(MediaTypeNames.Text.Html);

            controller.Request.Headers.Add(ConstantStrings.CompositeSessionIdHeaderName, Guid.NewGuid().ToString());
            A.CallTo(() => SessionStateService.GetAsync(A<Guid>._)).Returns(new SessionStateModel<SessionDataModel>
                {State = new SessionDataModel {DocumentId = 1}});
            A.CallTo(() => FakeYourAssessmentService.GetAssessmentListViewModel(A<long>._)).Returns(GetFakeBodyViewModel());
            var result = await controller.Body();

            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<BodyViewModel>(viewResult.ViewData.Model);
            Assert.NotNull(viewModel.AssessmentsPersonal);
            Assert.Equal(4, viewModel.AssessmentsPersonal.Count);
            Assert.Equal(6, viewModel.AssessmentsActivity.Count);
            Assert.Equal(0, viewModel.AssessmentsCompleted.Count);
        }

        private BodyViewModel GetFakeBodyViewModel()
        {
            return new BodyViewModel
            {
                AssessmentsPersonal = GetAssessmentList.Where(a => a.PersonalAssessment).ToList(),
                AssessmentsActivity = GetAssessmentList.Where(a => a.ActivityAssessment).ToList(),
                AssessmentsCompleted = new List<AssessmentOverview>(),
            };
        }

        private static List<AssessmentOverview> GetAssessmentList => new List<AssessmentOverview>
                {
                    new AssessmentOverview
                    {
                        Action = Assessments.Skills.Action,
                        AssessmentName = Assessments.Skills.Title,
                        AssessmentCategory = Assessments.Skills.Category,
                        Description = Assessments.Skills.Description,
                        AssessmentDuration = Assessments.Skills.TimeToComplete,
                        AssessmentType = AssessmentType.SkillAreas,
                        PersonalAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = Assessments.Interests.Action,
                        AssessmentName = Assessments.Interests.Title,
                        AssessmentCategory = Assessments.Interests.Category,
                        Description = Assessments.Interests.Description,
                        AssessmentDuration = Assessments.Interests.TimeToComplete,
                        AssessmentType = AssessmentType.Interest,
                        PersonalAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = Assessments.Personal.Action,
                        AssessmentName = Assessments.Personal.Title,
                        AssessmentCategory = Assessments.Personal.Category,
                        Description = Assessments.Personal.Description,
                        AssessmentDuration = Assessments.Personal.TimeToComplete,
                        AssessmentType = AssessmentType.Personal,
                        PersonalAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = Assessments.Motivation.Action,
                        AssessmentName = Assessments.Motivation.Title,
                        AssessmentCategory = Assessments.Motivation.Category,
                        Description = Assessments.Motivation.Description,
                        AssessmentDuration = Assessments.Motivation.TimeToComplete,
                        AssessmentType = AssessmentType.Motivation,
                        PersonalAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = Assessments.Numeric.Action,
                        AssessmentName = Assessments.Numeric.Title,
                        AssessmentCategory = Assessments.Numeric.Category,
                        Description = Assessments.Numeric.Description,
                        AssessmentDuration = Assessments.Numeric.TimeToComplete,
                        AssessmentType = AssessmentType.Numeric,
                        ActivityAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = Assessments.Verbal.Action,
                        AssessmentName = Assessments.Verbal.Title,
                        AssessmentCategory = Assessments.Verbal.Category,
                        Description = Assessments.Verbal.Description,
                        AssessmentDuration = Assessments.Verbal.TimeToComplete,
                        AssessmentType = AssessmentType.Verbal,
                        ActivityAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = Assessments.Checking.Action,
                        AssessmentName = Assessments.Checking.Title,
                        AssessmentCategory = Assessments.Checking.Category,
                        Description = Assessments.Checking.Description,
                        AssessmentDuration = Assessments.Checking.TimeToComplete,
                        AssessmentType = AssessmentType.Checking,
                        ActivityAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = Assessments.Mechanical.Action,
                        AssessmentName = Assessments.Mechanical.Title,
                        AssessmentCategory = Assessments.Mechanical.Category,
                        Description = Assessments.Mechanical.Description,
                        AssessmentDuration = Assessments.Mechanical.TimeToComplete,
                        AssessmentType = AssessmentType.Mechanical,
                        ActivityAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = Assessments.Spatial.Action,
                        AssessmentName = Assessments.Spatial.Title,
                        AssessmentCategory = Assessments.Spatial.Category,
                        Description = Assessments.Spatial.Description,
                        AssessmentDuration = Assessments.Spatial.TimeToComplete,
                        AssessmentType = AssessmentType.Spatial,
                        ActivityAssessment = true,
                    },
                    new AssessmentOverview
                    {
                        Action = Assessments.Abstract.Action,
                        AssessmentName = Assessments.Abstract.Title,
                        AssessmentCategory = Assessments.Abstract.Category,
                        Description = Assessments.Abstract.Description,
                        AssessmentDuration = Assessments.Abstract.TimeToComplete,
                        AssessmentType = AssessmentType.Abstract,
                        ActivityAssessment = true,
                    },
                };
        
    }
}
