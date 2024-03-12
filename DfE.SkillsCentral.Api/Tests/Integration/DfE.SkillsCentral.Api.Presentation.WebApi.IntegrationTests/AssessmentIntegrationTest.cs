using System.Net;
using System.Net.Mail;
using DfE.SkillsCentral.Api.Application.Interfaces.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace DfE.SkillsCentral.Api.Presentation.WebApi.IntegrationTests;

public class AssessmentIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public AssessmentIntegrationTest(WebApplicationFactory<Program> factory)
    {
        client = factory.CreateClient();
    }

    [Theory]
    [InlineData("SkillAreas")]
    [InlineData("Interests")]
    [InlineData("Personal")]
    [InlineData("Motivation")]
    [InlineData("Numerical")]
    [InlineData("Verbal")]
    [InlineData("Checking")]
    [InlineData("Mechanical")]
    [InlineData("Spatial")]
    [InlineData("Abstract")]
    public async Task GetAssessment_WithValidAssessmentType_ReturnExpectedAssessment(string assessmentType)
    {
        // Act
        var response = await client.GetAsync($"/api/Assessment/{assessmentType}");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<AssessmentQuestions>(content);

        // Assert
        Assert.Equal(assessmentType, result?.Assessment.Type);
    }

    [Fact]
    public async Task GetAssessment_WithInValidAssessmentType_ReturnNoContent()
    {
        // Act
        var response = await client.GetAsync($"/api/Assessment/QUACK");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}