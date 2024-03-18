using System.Globalization;
using DFC.SkillsCentral.Api.Application.Interfaces.Services;
//using DFC.SkillsCentral.Api.Application.DocumentFormatter;

using DFC.SkillsCentral.Api.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using DfE.SkillsCentral.Api.Application.Services.Services;
using System.Collections;
using System.Reflection;

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
    public async Task GenerateWordDoc_ReturnsSkillsDocument_WhenAllDataValuesAreProvided()
    {
        // Arrange
        var document = CreateNewSkillsDocument();
        _ = await docService.CreateSkillsDocument(document);
        var skillsDoc = await docService.GetSkillsDocumentByReferenceCode(document.ReferenceCode!);

        // Act
        var result = await sut.GenerateWordDoc(skillsDoc.Id.Value);
        string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        Directory.CreateDirectory("TestOutput");
        File.WriteAllBytes($"{currentPath}/TestOutput/TestReport.docx", result);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GeneratePDF_ReturnsSkillsDocument_WhenAllDataValuesAreProvided()
    {
        // Arrange
        var document = CreateNewSkillsDocument();
        _ = await docService.CreateSkillsDocument(document);
        var skillsDoc = await docService.GetSkillsDocumentByReferenceCode(document.ReferenceCode!);
        
        // Act
        var result = await sut.GeneratePDF(skillsDoc.Id.Value);
        string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        Directory.CreateDirectory("TestOutput");
        File.WriteAllBytes($"{currentPath}/TestOutput/TestReport.pdf", result);

        // Assert
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
                //{"UpdatedAt", "2024-03-15T14:10:14.697" },
            }

        };
    }
}