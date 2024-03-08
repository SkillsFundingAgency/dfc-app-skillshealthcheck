using DFC.SkillsCentral.Api.Application.Interfaces.Repositories;
using DFC.SkillsCentral.Api.Domain.Models;
using DfE.SkillsCentral.Api.Application.Services.Services;
using Moq;

namespace DfE.SkillsCentral.Api.Application.Services.UnitTests;

public class SkillsDocumentsServiceTests
{
    [Fact]
    public async Task GetSkillsDocument_ShouldReturnSkillsDocument_WhenValidIdProvided()
    {
        // Arrange
        var documentId = 1;

        var mockRepo = new Mock<ISkillsDocumentsRepository>();
        var expectedSkillsDocument = new SkillsDocument { Id = documentId };

        mockRepo.Setup(x => x.GetByIdAsync(documentId)).ReturnsAsync(expectedSkillsDocument);
        var sut = new SkillsDocumentsService(mockRepo.Object);

        // Act
        var document = await sut.GetSkillsDocument(documentId);

        // Assert
        Assert.True(document!.Equals(expectedSkillsDocument));
    }

    [Fact]
    public async Task GetSkillsDocument_ShouldReturnDefault_WhenInvalidIdProvided()
    {
        // Arrange
        var documentId = 1;
        var mockRepo = new Mock<ISkillsDocumentsRepository>();
        mockRepo.Setup(x => x.GetByIdAsync(documentId)).ReturnsAsync(default(SkillsDocument));
        var sut = new SkillsDocumentsService(mockRepo.Object);

        // Act
        var document = await sut.GetSkillsDocument(documentId);

        // Assert
        Assert.Null(document);
    }

    [Fact]
    public async Task GetSkillsDocumentByReferenceCode_ShouldReturnSkillsDocument_WhenValidReferenceCodeProvided()
    {
        // Arrange
        var referenceCode = "ANY_CODE";

        var mockRepo = new Mock<ISkillsDocumentsRepository>();
        var expectedSkillsDocument = new SkillsDocument { ReferenceCode = referenceCode };

        mockRepo.Setup(x => x.GetByReferenceCodeAsync(referenceCode)).ReturnsAsync(expectedSkillsDocument);
        var sut = new SkillsDocumentsService(mockRepo.Object);

        // Act
        var document = await sut.GetSkillsDocumentByReferenceCode(referenceCode);

        // Assert
        Assert.True(document!.Equals(expectedSkillsDocument));
    }

    [Fact]
    public async Task GetSkillsDocumentByReferenceCode_ShouldReturnDefault_WhenInvalidReferenceCodeProvided()
    {
        // Arrange
        var referenceCode = "ANY_CODE";
        var mockRepo = new Mock<ISkillsDocumentsRepository>();
        mockRepo.Setup(x => x.GetByReferenceCodeAsync(referenceCode)).ReturnsAsync(default(SkillsDocument));
        var sut = new SkillsDocumentsService(mockRepo.Object);

        // Act
        var document = await sut.GetSkillsDocumentByReferenceCode(referenceCode);

        // Assert
        Assert.Null(document);
    }

    [Fact]
    public async Task CreateSkillsDocument_ShouldReturnSkillsDocument_WhenSuccessful()
    {
        // Arrange
        var referenceCode = "ANY_CODE";
        var expectedSkillsDocument = new SkillsDocument { ReferenceCode = referenceCode };
        var mockRepo = new Mock<ISkillsDocumentsRepository>();


        mockRepo.Setup(x => x.GetByReferenceCodeAsync(referenceCode)).ReturnsAsync(expectedSkillsDocument);
        var sut = new SkillsDocumentsService(mockRepo.Object);

        // Act
        var document = await sut.CreateSkillsDocument(expectedSkillsDocument);

        // Assert
        Assert.True(document!.Equals(expectedSkillsDocument));
        mockRepo.Verify(x => x.AddAsync(expectedSkillsDocument), Times.Once);
    }
}