using System.Globalization;
using DFC.SkillsCentral.Api.Application.Interfaces.Services;
using DFC.SkillsCentral.Api.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DfE.SkillsCentral.Api.Application.Services.IntegrationTests;

public class SkillsDocumentsServiceTests
{
    private readonly ISkillsDocumentsService sut;

    public SkillsDocumentsServiceTests()
    {
        var services = new ServiceCollection();
        TestSetup.ConfigureServices(services);
        var serviceProvider = services.BuildServiceProvider();
        sut = serviceProvider.GetRequiredService<ISkillsDocumentsService>();
    }


    [Fact]
    public async Task SkillsDocumentService_CreateNewSkillsDocument()
    {
        // Arrange
        var document = CreateNewSkillsDocument();

        // Act
        var result = await sut.CreateSkillsDocument(document);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task SkillsDocumentService_CreateNewSkillsDocument_CreatedBySetWhenProvided()
    {
        // Arrange
        string createdBy = "test_user";
        var document = CreateNewSkillsDocument(createdBy);

        // Act
        var result = await sut.CreateSkillsDocument(document);

        // Assert
        Assert.Equal(result.CreatedBy, createdBy);
    }

    [Fact]
    public async Task SkillsDocumentService_CreateNewSkillsDocument_CreatedBySetWhenNotProvided()
    {
        // Arrange
        var document = CreateNewSkillsDocument();

        // Act
        var result = await sut.CreateSkillsDocument(document);

        // Assert
        Assert.Equal(result.CreatedBy, "Anonymous");
    }

    [Fact]
    public async Task SkillsDocumentService_CreateNewSkillsDocument_CreatedAtSet()
    {
        // Arrange
        var baselineDateTime = DateTime.Now;
        var document = CreateNewSkillsDocument();

        // Act
        var result = await sut.CreateSkillsDocument(document);

        // Assert
        Assert.Equal(baselineDateTime.ToString(CultureInfo.InvariantCulture), result.CreatedAt?.ToString(CultureInfo.InvariantCulture));
    }


    [Fact]
    public async Task SkillsDocumentService_GetValidDocumentById_ReturnsDocument()
    {
        // Arrange
        var document = CreateNewSkillsDocument();

        var savedDocument = await sut.CreateSkillsDocument(document);

        // Act
        var result = await sut.GetSkillsDocument(savedDocument.Id.GetValueOrDefault());

        Assert.NotNull(result);
    }

    [Fact]
    public async Task SkillsDocumentService_GetValidDocumentByReferenceCode_ReturnsDocument()
    {
        // Arrange
        var document = CreateNewSkillsDocument();

        _ = await sut.CreateSkillsDocument(document);

        // Act
        var result = await sut.GetSkillsDocumentByReferenceCode(document.ReferenceCode!);

        Assert.NotNull(result);
    }

    private SkillsDocument CreateNewSkillsDocument(string createdBy = null)
    {
        return new SkillsDocument
        {
            ReferenceCode = Guid.NewGuid().ToString(),
            CreatedBy = createdBy,
            DataValueKeys = new Dictionary<string, object>
            {
                {"SkillAreas.Answers" , new List<string> {"A", "B", "C"}}
            }
            
        };
    }
}