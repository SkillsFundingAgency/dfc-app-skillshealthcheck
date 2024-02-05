using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using static DFC.App.SkillsHealthCheck.IntegrationTests.Helper;

namespace DFC.App.SkillsHealthCheck.IntegrationTests.ControllerTests.QuestionController;

[Trait("Category", "Question Controller Integration Tests")]
public class EndSessionRouteTests : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private const string Url = "skills-health-check/question/end-session/body";
    private readonly CustomWebApplicationFactory<Startup> factory;

    public EndSessionRouteTests(CustomWebApplicationFactory<Startup> factory)
    {
        this.factory = factory;
    }

    [Fact]
    public async Task RequestWithoutActiveSessionRedirectToSessionTimeout()
    {
        // Arrange
        var uri = new Uri(Url, UriKind.Relative);
        var client = factory.CreateClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));

        // Act
        var response = await client.GetAsync(uri);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Redirect);
        response.Headers.Location!.ToString().Should().StartWith("/skills-health-check/session-timeout");
    }

    [Fact]
    public async Task RequestWithActiveSessionRedirectsAsExpected()
    {
        // Arrange
        var uri = new Uri(Url, UriKind.Relative);
        var client = factory.CreateClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Text.Html));
        SetSession(client, factory);
        factory.SetSkillsDocument();

        // Act
        var response = await client.GetAsync(uri);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Redirect);
        response.Headers.Location!.ToString().Should().Contain("skills-health-check/home");
    }
}