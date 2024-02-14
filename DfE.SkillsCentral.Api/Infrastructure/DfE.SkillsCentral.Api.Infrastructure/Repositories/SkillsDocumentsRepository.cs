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

        public async Task AddAsync(SkillsDocument skillsDocument)
        {
            using (var connection = dbContext.CreateConnection())
            {
                connection.Open();
                await connection.QueryAsync(SkillsDocumentsQueries.InsertSkillsDocumentAsync, new { skillsDocument.SkillsDocumentTypeSysId, skillsDocument.SkillsDocumentTitle, skillsDocument.CreatedAt, 
                    skillsDocument.CreatedBy, skillsDocument.ExpiresTimespan, skillsDocument.ExpiresType, skillsDocument.XMLValueKeys, skillsDocument.LastAccessed, skillsDocument.ReferenceCode});
                
            }
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

        public async Task UpdateAsync(SkillsDocument skillsDocument)
        {
            using (var connection = dbContext.CreateConnection())
            {
                connection.Open();
                _=await connection.QueryAsync(SkillsDocumentsQueries.UpdateSkillsDocument, new { skillsDocument.SkillsDocumentTitle, skillsDocument.UpdatedAt, skillsDocument.UpdatedBy, 
                    skillsDocument.XMLValueKeys, skillsDocument.ExpiresTimespan, skillsDocument.ExpiresType, skillsDocument.LastAccessed, skillsDocument.SkillsDocumentId });
                
            }
        }

        


    }
}