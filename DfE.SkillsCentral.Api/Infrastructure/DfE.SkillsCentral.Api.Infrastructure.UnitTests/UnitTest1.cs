using DFC.SkillsCentral.Api.Application.Interfaces.Repositories;
using DFC.SkillsCentral.Api.Infrastructure;
using DFC.SkillsCentral.Api.Infrastructure.Repositories;
using Moq;

namespace DfE.SkillsCentral.Api.Infrastructure.UnitTests
{
    public class AssessmentsRepositoryTests
    {
        private readonly AssessmentsRepository _assessmentsRepository;
        private readonly Mock<DatabaseContext> _dbContext = new Mock<DatabaseContext>(); 
        public AssessmentsRepositoryTests() 
        {
            _assessmentsRepository = new AssessmentsRepository(_dbContext.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnAssessment_WhenAssessmentExists()
        {
            var assessmentId = new int();
            
            var assessment = await _assessmentsRepository.GetByIdAsync(assessmentId);
        }
    }
}