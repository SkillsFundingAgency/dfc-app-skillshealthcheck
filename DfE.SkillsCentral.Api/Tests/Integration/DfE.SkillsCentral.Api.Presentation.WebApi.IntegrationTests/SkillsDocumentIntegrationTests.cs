using System.Net;
using System.Net.Http.Json;
using DFC.SkillsCentral.Api.Domain.Models;
using DfE.SkillsCentral.Api.Application.Interfaces.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace DfE.SkillsCentral.Api.Presentation.WebApi.IntegrationTests;

public class SkillsDocumentIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public SkillsDocumentIntegrationTests(WebApplicationFactory<Program> factory)
    {
        client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateSkillsDocument_ReturnExpectedSkillsDocument()
    {
        // Act
        var skillsDocument = new SkillsDocument();
        skillsDocument.Id = 1;
        skillsDocument.ReferenceCode = Guid.NewGuid().ToString();

        var response = await client.PostAsJsonAsync($"/api/SkillsDocument/", skillsDocument);

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<AssessmentQuestions>(content);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task CreateSkillsDocument_ReturnBadRequest()
    {
        // Act
        var response = await client.PostAsJsonAsync($"/api/SkillsDocument/", string.Empty);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetSkillsDocumentByReferenceCode_ReturnExpectedSkillsDocument()
    {
        // Act
        var skillsDocument = new SkillsDocument
        {
            Id = 1,
            ReferenceCode = Guid.NewGuid().ToString()
        };

        _ = await client.PostAsJsonAsync($"/api/SkillsDocument/", skillsDocument);

        var response = await client.GetAsync($"/api/SkillsDocument/{skillsDocument.ReferenceCode}");
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<AssessmentQuestions>(content);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetSkillsDocumentByReferenceCode_ReturnNoContent()
    {
        // Act
        var response = await client.GetAsync($"/api/SkillsDocument/{Guid.NewGuid()}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task SaveSkillsDocument_ReturnExpectedSkillsDocument()
    {
        // Act
        var originalSkillsDocument = new SkillsDocument();
        originalSkillsDocument.Id = 1;
        originalSkillsDocument.ReferenceCode = Guid.NewGuid().ToString();

        var response = await client.PostAsJsonAsync($"/api/SkillsDocument/", originalSkillsDocument);
        var content = await response.Content.ReadAsStringAsync();
        var savedSkillsDocument = JsonConvert.DeserializeObject<SkillsDocument>(content);

        savedSkillsDocument.UpdatedBy = "IntegrationTestUser";

        var patchResponse = await client.PatchAsJsonAsync($"/api/SkillsDocument/", savedSkillsDocument);

        var patchContent = await patchResponse.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<AssessmentQuestions>(patchContent);

        // Assert
        Assert.Equal(HttpStatusCode.OK, patchResponse.StatusCode);
    }

    [Fact]
    public async Task SaveSkillsDocument_ReturnBadRequest()
    {
        // Act
        var originalSkillsDocument = new SkillsDocument();
        originalSkillsDocument.Id = 1;
        originalSkillsDocument.ReferenceCode = Guid.NewGuid().ToString();

        var response = await client.PatchAsJsonAsync($"/api/SkillsDocument/", string.Empty);

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<AssessmentQuestions>(content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}