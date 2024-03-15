using System.Globalization;
using DFC.SkillsCentral.Api.Application.Interfaces.Services;
//using DFC.SkillsCentral.Api.Application.DocumentFormatter;

using DFC.SkillsCentral.Api.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using DfE.SkillsCentral.Api.Application.Services.Services;
using System.Collections;

namespace DfE.SkillsCentral.Api.Application.Services.IntegrationTests;

public class DocumentGenerationServiceTests
{
    private readonly IDocumentsGenerationService sut;
    private readonly ISkillsDocumentsService docService;

    public DocumentGenerationServiceTests()
    {
        var services = new ServiceCollection();
        TestSetup.ConfigureServices(services);
        var serviceProvider = services.BuildServiceProvider();
        sut = serviceProvider.GetRequiredService<IDocumentsGenerationService>();
        docService = serviceProvider.GetService<ISkillsDocumentsService>();
    }



    [Fact]
    public async Task quackquack()
    {
        // Arrange
        var document = CreateNewSkillsDocument();

        _ = await docService.CreateSkillsDocument(document);
        var skillsDoc = await docService.GetSkillsDocumentByReferenceCode(document.ReferenceCode!);
        // Act
        var result = await sut.GenerateWordDoc(skillsDoc.Id.Value);

        File.WriteAllBytes("C:\\Git\\quack.docx", result);

        Assert.NotNull(result);
    }

    private SkillsDocument CreateNewSkillsDocument(string createdBy = null)
    {
        return new SkillsDocument
        {
            
            ReferenceCode = Guid.NewGuid().ToString(),
            CreatedBy = createdBy,
            DataValueKeys = new Dictionary<string, string>
            {
                {"Spatial.Answers" ,  "C,A,B,B,B,B,B,B,B,B,B,B,B,B" },
                {"Spatial.Complete", "True" },
                {"Spatial.Enjoyment", "2" },
                {"Spatial.Timing", "2" },
                {"Spatial.Ease", "2" },
                {"Personal.Complete", "False" },
                {"Personal.Answers", "2,2,2,2,-1" },
                {"UpdatedAt", "2024-03-15T14:10:14.697" }

            }

        };
    }
}