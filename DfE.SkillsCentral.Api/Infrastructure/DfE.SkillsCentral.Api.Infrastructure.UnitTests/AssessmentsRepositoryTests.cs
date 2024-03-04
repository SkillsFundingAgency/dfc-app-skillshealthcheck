using Dapper;
using DFC.SkillsCentral.Api.Application.Interfaces.Repositories;
using DFC.SkillsCentral.Api.Infrastructure;
using DFC.SkillsCentral.Api.Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Moq;
using System.Data;

namespace DfE.SkillsCentral.Api.Infrastructure.UnitTests
{
    public class AssessmentsRepositoryTests
    {
        private readonly IDbConnection _connection;
        private readonly AssessmentsRepository _assessmentsRepository;
        private readonly Mock<DatabaseContext> _dbContext = new Mock<DatabaseContext>(); 
        public AssessmentsRepositoryTests() 
        {

            //SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());


            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();
            _connection.Execute("CREATE TABLE Assessments (AssessmentId INTEGER PRIMARY KEY, AssessmentType TEXT, AssessmentTitle TEXT, AssessmentSubtitle TEXT, AssessmentIntroduction TEXT, QualificationLevelNumber INTEGER, AccessibilityLevelNumber INTEGER)");
            _connection.Execute("INSERT INTO Assessments (AssessmentId, AssessmentType, AssessmentTitle, AssessmentSubtitle, AssessmentIntroduction, QualificationLevelNumber, AccessibilityLevelNumbe) VALUES (2, 'Checking', 'test', 'test', 'test', 1, 1)");

            _dbContext.Setup(x => x.CreateConnection()).Returns(_connection);
            _assessmentsRepository = new AssessmentsRepository(_dbContext.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnAssessment_WhenAssessmentExists()
        {
            var assessmentId = 2;
            
            var assessment = await _assessmentsRepository.GetByIdAsync(assessmentId);

            Assert.NotNull(assessment);
            Assert.Equal("test", assessment.Title);
        }
    }
}