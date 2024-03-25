using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.ViewModels.YourAssessments;

using FakeItEasy;

using FluentAssertions;

using Xunit;

using static DFC.App.SkillsHealthCheck.IntegrationTests.Helper;

namespace DFC.App.SkillsHealthCheck.IntegrationTests.ControllerTests.YourAssessmentController
{
    [Trait("Category", "YourAssessment Controller Integration Tests")]
    public class DownloadDocumentRouteTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;

        public DownloadDocumentRouteTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task DownloadDocumentBodyEndpointWithoutActiveSessionRedirectToSessionTimeout()
        {
            // Arrange
            var uri = new Uri("skills-health-check/your-assessments/download-document/body", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));

            // Act
            var response = await client.PostAsJsonAsync(uri, new BodyViewModel { });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Redirect);
            response.Headers.Location.ToString().Should().StartWith("/skills-health-check/session-timeout");
        }

        [Theory]
        [InlineData(DownloadType.Pdf, MediaTypeNames.Application.Pdf)]
        [InlineData(DownloadType.Word, "application/docx")]
        public async Task DownloadDocumentBodyValidRequestWithActiveSessionReturnFile(DownloadType downloadType, string contentType)
        {
            // Arrange
            var uri = new Uri("skills-health-check/your-assessments/download-document/body", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            SetSession(client, factory);
            factory.SetSkillsDocument();
            A.CallTo(() => factory.FakeSkillsHealthCheckService.GenerateWordDoc(A<int>.Ignored)
            A.CallTo(() => factory.FakeSkillsHealthCheckService.DownloadDocument(A<DownloadDocumentRequest>.Ignored))
                .Returns(new DownloadDocumentResponse { Success = true, DocumentName = "test", DocumentBytes = GetFileBytes() });

            // Act
            using var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                ["DownloadType"] = downloadType.ToString()
            });
            var response = await client.PostAsync(uri, content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be(contentType);
        }

        [Fact]
        public async Task DownloadDocumentBodyWithActiveSessionButDocumentDownloadFailedReturnSuccessWithValidationError()
        {
            // Arrange
            var uri = new Uri("skills-health-check/your-assessments/download-document/body", UriKind.Relative);
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
            SetSession(client, factory);
            factory.SetSkillsDocument();
            A.CallTo(() => factory.FakeSkillsHealthCheckService.RequestDownloadAsync(A<long>.Ignored, A<string>.Ignored, A<string>.Ignored))
                .Returns(DocumentStatus.Error);

            // Act
            var response = await client.PostAsJsonAsync(uri, new BodyViewModel { DownloadType = DownloadType.Pdf });

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Text.Html);
        }
    }
}
