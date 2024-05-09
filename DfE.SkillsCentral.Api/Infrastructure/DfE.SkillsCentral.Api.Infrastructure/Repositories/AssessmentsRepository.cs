using Dapper;
using DFC.SkillsCentral.Api.Application.Interfaces.Repositories;
using DFC.SkillsCentral.Api.Domain.Models;
using DFC.SkillsCentral.Api.Infrastructure.Queries;
using System.Data;

namespace DFC.SkillsCentral.Api.Infrastructure.Repositories
{
    public class AssessmentsRepository : IAssessmentsRepository
    {

        private readonly DatabaseContext dbContext;
        public AssessmentsRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        

        public async Task<Assessment?> GetByTypeAsync(string type)
        {
            using (var connection = dbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Assessment>(AssessmentsQueries.AssessmentByType, new { Type = type });
                return result;
            }
        }
    }
}