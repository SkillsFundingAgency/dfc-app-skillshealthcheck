using DFC.SkillsCentral.Api.Application.Interfaces.Services;
using DFC.SkillsCentral.Api.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DfE.SkillsCentral.Api.Application.Services.IntegrationTests;

public class SkillsDocumentServiceTests
{
    private readonly ISkillsDocumentsService sut;

    public SkillsDocumentServiceTests()
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
    public async Task SkillsDocumentService_CreateNewSkillsDocument_CreatedBySet()
    {
        // Arrange
        var document = CreateNewSkillsDocument();

        // Act
        var result = await sut.CreateSkillsDocument(document);

        // Assert
        Assert.NotNull(result.CreatedBy);
    }

    [Fact]
    public async Task SkillsDocumentService_CreateNewSkillsDocument_CreatedAtSet()
    {
        // Arrange
        var currentDateTime = DateTime.Now;
        var document = CreateNewSkillsDocument();

        // Act
        var result = await sut.CreateSkillsDocument(document);

        // Assert
        Assert.True(currentDateTime < result.CreatedAt);
    }

    [Fact]
    public async Task SkillsDocumentService_GetValidDocumentById_ReturnsDocument()
    {
        // Arrange
        var document = CreateNewSkillsDocument();

        var savedDocument = await sut.CreateSkillsDocument(document);

        // Act
        var result = sut.GetSkillsDocument(savedDocument.Id.GetValueOrDefault());

        Assert.NotNull(result);
    }

    [Fact]
    public async Task SkillsDocumentService_GetValidDocumentByReferenceCode_ReturnsDocument()
    {
        // Arrange
        var document = CreateNewSkillsDocument();

        _ = await sut.CreateSkillsDocument(document);

        // Act
        var result = sut.GetSkillsDocumentByReferenceCode(document.ReferenceCode!);

        Assert.NotNull(result);
    }

    private SkillsDocument CreateNewSkillsDocument()
    {
        return new SkillsDocument
        {
            ReferenceCode = Guid.NewGuid().ToString()
        };
    }
}