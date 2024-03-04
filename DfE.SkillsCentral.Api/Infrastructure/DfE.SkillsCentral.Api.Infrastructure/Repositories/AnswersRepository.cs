using Dapper;
using DFC.SkillsCentral.Api.Application.Interfaces.Repositories;
using DFC.SkillsCentral.Api.Domain.Models;
using DFC.SkillsCentral.Api.Infrastructure.Queries;
using System.Data;

namespace DFC.SkillsCentral.Api.Infrastructure.Repositories
{
    public class AnswersRepository : IAnswersRepository
    {

        private readonly DatabaseContext dbContext;
        public AnswersRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        
        public async Task<IReadOnlyList<Answer>?> GetAllByQuestionIdAsync(int questionId)
        {
            using (var connection = dbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Answer>(AnswersQueries.AllAnswersByQuestionId, new { QuestionId = questionId});
                return result.ToList();
            }
        }

        public async Task<Answer?> GetByIdAsync(int id)
        {
            using (var connection = dbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Answer>(AnswersQueries.AnswerById, new { Id = id});
                return result;
            }
        }

    }
}