using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using Moq;

using DFC.App.SkillsHealthCheck.ViewModels;

using FakeItEasy;

using FluentAssertions;

using Xunit;

using static DFC.App.SkillsHealthCheck.IntegrationTests.Helper;
using DFC.SkillsCentral.Api.Domain.Models;

namespace DFC.App.SkillsHealthCheck.IntegrationTests.ControllerTests.YourAssessmentController
{
    [Trait("Category", "YourAssessment Controller Integration Tests")]
    public class ReturnToAssessmentRouteTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;

        public ReturnToAssessmentRouteTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task ReturnToAssessmentBodyEndpointWithoutActiveSessionRedirectToSessionTimeout()
        {
            // Arrange
            var uri = new Uri("skills-health-check/your-assessments/return-to-assessment/body", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));

            // Act
            var response = await client.PostAsJsonAsync(uri, new ReturnToAssessmentViewModel { });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location.ToString().Should().StartWith("/skills-health-check/session-timeout");
        }

        [Fact]
        public async Task ReturnToAssessmentBodyValidRequestWithActiveSessionRedirectsToYourAssessment()
        {
            // Arrange
            var uri = new Uri("skills-health-check/your-assessments/return-to-assessment/body", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            SetSession(client, factory);
            factory.SetSkillsDocument();
            A.CallTo(() => factory.FakeSkillsHealthCheckService.GetSkillsDocumentByReferenceCode(A<string>.Ignored))
                .Returns(new SkillsDocument { Id = 1 });
                //.Returns(new Services.SkillsCentral.Messages.GetSkillsDocumentIdResponse { DocumentId = 1, Success = true });

            // Act
            using var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                ["ReferenceId"] = "some known reference",
                ["ActionUrl"] = "localhost"
            });
            var response = await client.PostAsync(uri, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location.ToString().Should().StartWith("/skills-health-check/your-assessments");
        }

        //[Fact]
        //public async Task ReturnToAssessmentBodyUnknownReferenceWithActiveSessionReturnSuccessWithValidationError()
        //{
        //    // Arrange
        //    var uri = new Uri("skills-health-check/your-assessments/return-to-assessment/body", UriKind.Relative);
        //    var client = factory.CreateClient();
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
        //    SetSession(client, factory);
        //    factory.SetSkillsDocument();
        //    A.CallTo(() => factory.FakeSkillsHealthCheckService.GetSkillsDocumentByReferenceCode(A<string>.Ignored))
        //       .Returns(new SkillsDocument { Id = 1 });
        //    /*A.CallTo(() => factory.FakeSkillsHealthCheckService.GetSkillsDocumentByIdentifier(A<string>.Ignored))
        //        .Returns(new Services.SkillsCentral.Messages.GetSkillsDocumentIdResponse { ErrorMessage = "Reference not found.", Success = false });*/

        //    // Act
        //    var response = await client.PostAsJsonAsync(uri, new ReturnToAssessmentViewModel { ReferenceId = "Unknown reference", ActionUrl = "localhost" });

        //    // Assert
        //    response.StatusCode.Should().Be(HttpStatusCode.OK);
        //    response.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Text.Html);
        //}

        //[Fact]
        //public async Task ReturnToAssessmentBodyInvalidRequestWithActiveSessionReturnSuccessWithValidationError()
        //{
        //    // Arrange
        //    var uri = new Uri("skills-health-check/your-assessments/return-to-assessment/body", UriKind.Relative);
        //    var client = factory.CreateClient();
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
        //    SetSession(client, factory);
        //    factory.SetSkillsDocument();
        //    A.CallTo(() => factory.FakeSkillsHealthCheckService.GetSkillsDocumentByReferenceCode(A<string>.Ignored))
        //      .Returns(new SkillsDocument { Id = 1 });
        //    /* A.CallTo(() => factory.FakeSkillsHealthCheckService.GetSkillsDocumentByIdentifier(A<string>.Ignored))
        //         .Returns(new Services.SkillsCentral.Messages.GetSkillsDocumentIdResponse { DocumentId = 1 });*/

        //    // Act
        //    var response = await client.PostAsJsonAsync(uri, new ReturnToAssessmentViewModel { });

        //    // Assert
        //    response.StatusCode.Should().Be(HttpStatusCode.OK);
        //    response.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Text.Html);
        //}
    }
}
