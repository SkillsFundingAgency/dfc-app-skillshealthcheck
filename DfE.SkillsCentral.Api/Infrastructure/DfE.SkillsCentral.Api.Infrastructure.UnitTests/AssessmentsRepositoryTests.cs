using System.Data;
using Dapper;
using DFC.SkillsCentral.Api.Domain.Models;
using DFC.SkillsCentral.Api.Infrastructure;
using DFC.SkillsCentral.Api.Infrastructure.Queries;
using DFC.SkillsCentral.Api.Infrastructure.Repositories;
using Moq;
using Moq.Dapper;
using Xunit.Sdk;

namespace DfE.SkillsCentral.Api.Infrastructure.UnitTests;

public class AssessmentsRepositoryTests
{
    private readonly AssessmentsRepository assessmentsRepository;
    private readonly Mock<IDbConnection> mockConnection = new();
    private readonly Mock<IDatabaseContext> mockContext = new();

    public AssessmentsRepositoryTests()
    {
        var expectedAssessment = new Assessment { AssessmentId = 2 };
        var mockDbConnection = new Mock<IDbConnection>();
        mockContext.Setup(x => x.CreateConnection()).Returns(mockConnection.Object);
        assessmentsRepository = new AssessmentsRepository(mockContext.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnAssessment_WhenAssessmentExists()
    {
        // Arrange
        var expectedAssessment = new Assessment { AssessmentId = 2 };
        mockConnection
            .SetupDapperAsync(x =>
                x.QuerySingleOrDefaultAsync<Assessment>(It.IsAny<string>(), It.IsAny<object>(),
                    null, null, null)).ReturnsAsync(expectedAssessment);

        // Act
        var assessment = await assessmentsRepository.GetByIdAsync(expectedAssessment.AssessmentId);

        // Assert
        Assert.Equal(expectedAssessment.AssessmentId, assessment.AssessmentId);
    }

    [Fact]
    public async Task GetByIdAsync_WithInValidAssessment_ReturnsNull()
    {
        // Arrange
        mockConnection
            .SetupDapperAsync(x =>
                x.QuerySingleOrDefaultAsync<Assessment>(It.IsAny<string>(), It.IsAny<object>(),
                    null, null, null)).ReturnsAsync((Assessment?)null);

        // Act
        var assessment = await assessmentsRepository.GetByIdAsync(123);

        // Assert
        Assert.Null(assessment);
    }
}