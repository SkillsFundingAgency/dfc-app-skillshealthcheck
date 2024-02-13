using Dapper;
using DFC.SkillsCentral.Api.Application.Interfaces.Repositories;
using DFC.SkillsCentral.Api.Domain.Models;
using DFC.SkillsCentral.Api.Infrastructure.Queries;
using System.Data;

namespace DFC.SkillsCentral.Api.Infrastructure.Repositories
{
    public class SkillsDocumentsRepository : ISkillsDocumentsRepository
    {
        private readonly DatabaseContext dbContext;
        public SkillsDocumentsRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<string> AddAsync(SkillsDocument skillsDocument)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<SkillsDocument> GetByIdAsync(int skillsDocumentId)
        {
            using (var connection = dbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<SkillsDocument>(SkillsDocumentsQueries.GetSkillsDocumentByIdAsync, new { SkillsDocumentId = skillsDocumentId });
                return result;
            }
        }

        public async Task<SkillsDocument> GetByReferenceCodeAsync(string referenceCode)
        {
            using (var connection = dbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<SkillsDocument>(SkillsDocumentsQueries.GetSkillsDocumentByReferenceCodeAsync , new { ReferenceCode = referenceCode });
                return result;
            }
        }

        public Task<string> UpdateAsync(SkillsDocument skillsDocument)
        {
            throw new NotImplementedException();
        }

        // AddSkillsDocument
        // UpdateSkillsDocumment


    }
}