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
        

        public async Task<IReadOnlyList<Answer>> GetAllByAssessmentIdAsync(int assessmentId)
        {
            using (var connection = dbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Answer>(AnswersQueries.AllAnswersByAssessmentId, new { AssessmentId = assessmentId});
                return result.ToList();
            }
        }

        public async Task<IReadOnlyList<Answer>> GetAllByQuestionIdAsync(int questionId)
        {
            using (var connection = dbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Answer>(AnswersQueries.AllAnswersByQuestionId, new { QuestionId = questionId});
                return result.ToList();
            }
        }

        public async Task<Answer> GetByIdAsync(int answerId)
        {
            using (var connection = dbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Answer>(AnswersQueries.AnswerById, new { AnswerId = answerId});
                return result;
            }
        }

    }
}