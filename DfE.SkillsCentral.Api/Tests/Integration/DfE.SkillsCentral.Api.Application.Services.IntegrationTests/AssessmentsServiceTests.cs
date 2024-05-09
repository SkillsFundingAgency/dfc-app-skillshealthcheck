using DFC.SkillsCentral.Api.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using DFC.SkillsCentral.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DfE.SkillsCentral.Api.Application.Services.IntegrationTests;

public class AssessmentsServiceTests
{
    private readonly IAssessmentsService sut;

    public AssessmentsServiceTests()
    {
        var services = new ServiceCollection();
        TestSetup.ConfigureServices(services);
        var serviceProvider = services.BuildServiceProvider();
        sut = serviceProvider.GetRequiredService<IAssessmentsService>();
    }

    /*
    [Fact]
    public async Task AssessmentsService_GetAllAssessmentQuestions_ReturnsQuestions()
    {
        // Arrange
        var assessment = CreateNewAssessment();

        // Act
        var result = await sut.GetAssessmentQuestions(assessment.Type);

        // Assert
        Assert.NotNull(result);
    }
    private Assessment CreateNewAssessment()
    {
        return new Assessment
        {
            //Type = "numerical"
        };
    }
    */
}
