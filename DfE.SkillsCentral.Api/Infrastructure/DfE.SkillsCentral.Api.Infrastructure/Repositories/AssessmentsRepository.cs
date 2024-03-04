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
        //public async Task<IReadOnlyList<Assessment>> GetAllAsync()
        //{
        //    using (var connection = dbContext.CreateConnection())
        //    {
        //        connection.Open();
        //        var result = await connection.QueryAsync<Assessment>(AssessmentsQueries.AllAssessments);
        //        return result.ToList();
        //    }
        //}
        // TODO:
        //public async Task<Assessment> GetByIdAsync(int id)
        //{
        //    using (var connection = dbContext.CreateConnection())
        //    {
        //        connection.Open();
        //        var result = await connection.QuerySingleOrDefaultAsync<Assessment>(AssessmentsQueries.AssessmentById, new { Id = id});
        //        return result;
        //    }
        //}

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