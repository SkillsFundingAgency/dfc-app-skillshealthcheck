using System.Net;
using DFC.SkillsCentral.Api.Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace DfE.SkillsCentral.Api.Presentation.WebApi.IntegrationTests;

public class AssessmentIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public AssessmentIntegrationTests(WebApplicationFactory<Program> factory)
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

    [Theory]
    [InlineData(1, "Spatial")]
    [InlineData(3, "Mechanical")]
    [InlineData(5, "Checking")]
    public async Task GetSingleQuestion_ReturnExpectedAssessment(int questionNumber, string assessmentType)
    {
        // Act
   var response = await client.GetAsync($"/api/Assessment/{assessmentType}Question/{questionNumber}");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<QuestionAnswers>(content);

        // Assert
        Assert.NotNull(result);
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