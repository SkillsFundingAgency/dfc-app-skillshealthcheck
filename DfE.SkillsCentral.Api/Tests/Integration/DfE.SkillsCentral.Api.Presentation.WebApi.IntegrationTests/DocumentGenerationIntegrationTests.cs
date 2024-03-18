using System.Net;
using System.Net.Http.Json;
using DFC.SkillsCentral.Api.Domain.Models;
using DfE.SkillsCentral.Api.Application.Interfaces.Models;
using DocumentFormat.OpenXml.Office2010.Word;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace DfE.SkillsCentral.Api.Presentation.WebApi.IntegrationTests;

public class DocumentGenerationIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public DocumentGenerationIntegrationTests(WebApplicationFactory<Program> factory)
    {
        client = factory.CreateClient();
    }

    [Fact]
    public async Task GenerateWordDoc_WithValidDocumentId_ReturnWordDocFile()
    {
        // Arrange
        var document = CreateNewSkillsDocument();
        var response = await client.PostAsJsonAsync($"/api/SkillsDocument/", document);

        var content = await response.Content.ReadAsStringAsync();
        var documentObject = JsonConvert.DeserializeObject<SkillsDocument>(content);

        // Act
        var result = await client.GetAsync($"/api/DocumentGeneration/docx/{documentObject.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal("application/vnd.openxmlformats-officedocument.wordprocessingml.document", result.Content.Headers.ContentType.MediaType);

    }

    [Fact]
    public async Task GeneratePDF_WithValidDocumentId_ReturnPdfFile()
    {
        // Arrange
        var document = CreateNewSkillsDocument();
        var response = await client.PostAsJsonAsync($"/api/SkillsDocument/", document);

        var content = await response.Content.ReadAsStringAsync();
        var documentObject = JsonConvert.DeserializeObject<SkillsDocument>(content);

        // Act
        var result = await client.GetAsync($"/api/DocumentGeneration/pdf/{documentObject.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal("application/pdf", result.Content.Headers.ContentType.MediaType);

    }

    private SkillsDocument CreateNewSkillsDocument(string createdBy = null)
    {
        return new SkillsDocument
        {

            ReferenceCode = Guid.NewGuid().ToString(),
            CreatedBy = createdBy,
            DataValueKeys = new Dictionary<string, string>
            {
                {"SkillAreas.ExcludedJobFamilies3", "F10014" },
                {"SkillAreas.ExcludedJobFamilies2", "F10013" },
                {"SkillAreas.ExcludedJobFamilies1", "F10012" },
                {"SkillAreas.Complete", "True" },
                {"SkillAreas.Answers", "1,2,0,1,2,0,2,1,0,1,2,0,0,2,1,2,1,0,1,2,0,0,2,1,1,2,0,2,1,0,1,2,0,1,2,0,1,2,0,0,2,1,0,2,1,1,2,0,1,2,0,0,2,1,1,2,0,0,2,1,1,2,0,1,0,2,1,2,0,1,2,0,1,2,0,2,1,0,2,1,0,2,1,0" },
                {"Interest.Answers","2,5,4,4,4,4,3,5,2,3,1,3,4,2,5,4,5,3,2,2,1,5,3,4,5,4,3,4,4,4,2,3,2,4,3,2,3,4,3,2,4,3,2,3,4,3,4,3,4,5,5,3,2,4,4,3,3,3,4,2,2,3,2,3,2,3,2,2,2,3,3,2,3,4,3,4,3,4,5,5,3,4,4,5" },
                {"Interest.Complete","True" },
                {"Personal.Complete", "True" },
                {"Personal.Answers", "4,5,2,1,2,3,1,2,4,5,4,3,3,3,2,1,4,5,3,5,3,3,2,5,4,4,3,2,3,4,5,5,4,4,3,2,1,5,5,4,3,5,4,5,4,5,5,4,4,5,3,2,3,4,2" },
                {"Motivation.Answers","2,5,4,3,4,4,2,1,1,2,2,3,2,3,3,2,4,4,3,2,2,3,2,3,3,2,1,2,3,3,4,3,3,3,4,5,2,2,1,5,4,5" },
                {"Motivation.Complete","True" },
                {"Numeric.Answers","D,A,E,C,D,C,D,E,D,A" },
                {"Numeric.Complete","True" },
                {"Numeric.Ease","3" },
                {"Numeric.Timing","4" },
                {"Verbal.Answers","B,B,A,B,B,C,B,A,A,A,B,A,C,A,A,B,B,C,B,C" },
                {"Verbal.Complete","True" },
                {"Verbal.Ease","3" },
                {"Verbal.Timing","4" },
                {"Checking.Answers","16,8,15,16,15,16,12,16,8,12,12,16,8,16,16,12,12,12,6,12,15,12,3,16,8,16,12,8,16,16,16,12,16,16,3,16,16,16,16,16" },
                {"Checking.Complete","True" },
                {"Checking.Ease","3" },
                {"Checking.Timing","5" },
                {"Checking.Enjoyment","1" },
                {"Mechanical.Answers","Neither,B,A,A,C,Equal,A,C,B,A,B" },
                {"Mechanical.Complete","True" },
                {"Mechanical.Ease","2" },
                {"Mechanical.Timing","4" },
                {"Mechanical.Enjoyment","2" },
                {"Spatial.Answers" ,  "C,A,B,B,B,B,B,B,B,B,B,B,B,B" },
                {"Spatial.Complete", "True" },
                {"Spatial.Enjoyment", "2" },
                {"Spatial.Timing", "2" },
                {"Spatial.Ease", "2" },
                {"Abstract.Answers","C,A,C,A,D,A,D,A,E,A,E,B,C,D,B,E" },
                {"Abstract.Complete","True" },
                {"Abstract.Ease","1" },
                {"Abstract.Timing","4" },
                {"Abstract.Enjoyment","2" }
            }

        };
    }
}