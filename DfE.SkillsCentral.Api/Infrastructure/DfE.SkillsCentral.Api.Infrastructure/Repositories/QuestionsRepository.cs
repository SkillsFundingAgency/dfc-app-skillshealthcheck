﻿using Dapper;
using DFC.SkillsCentral.Api.Application.Interfaces.Repositories;
using DFC.SkillsCentral.Api.Domain.Models;
using DFC.SkillsCentral.Api.Infrastructure.Queries;
using System.Data;

namespace DFC.SkillsCentral.Api.Infrastructure.Repositories
{
    public class QuestionsRepository : IQuestionsRepository
    {

        private readonly DatabaseContext dbContext;
        public QuestionsRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<IReadOnlyList<Question>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Question>?> GetAllByAssessmentIdAsync(int assessmentId)
        {
            using (var connection = dbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Question>(QuestionsQueries.AllQuestionsByAssessmentId, new { AssessmentId = assessmentId });
                return result.ToList();
            }
        }

        public async Task<Question?> GetByIdAsync(int id)
        {
            using (var connection = dbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Question>(QuestionsQueries.QuestionById, new { Id = id});
                return result;
            }
        }
        public async Task<Question?> GetByNumberAndAssessmentIdAsync(int questionNumber, int assessmentId)
        {
            using (var connection = dbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Question>(QuestionsQueries.QuestionByNumberAndAssessmentId, new { Number = questionNumber, AssessmentId = assessmentId });
                return result;
            }
        }
    }
}