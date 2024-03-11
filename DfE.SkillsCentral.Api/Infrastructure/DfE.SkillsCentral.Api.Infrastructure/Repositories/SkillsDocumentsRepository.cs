using Dapper;
using DFC.SkillsCentral.Api.Application.Interfaces.Repositories;
using DFC.SkillsCentral.Api.Domain.Models;
using DFC.SkillsCentral.Api.Infrastructure.Queries;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

            //var dataValuesJsonString = JsonConvert.SerializeObject(skillsDocument.DataValueKeys);
            using (var connection = dbContext.CreateConnection())
            {
                DateTime createdAt = DateTime.UtcNow;
                string createdBy = skillsDocument.CreatedBy != null ? skillsDocument.CreatedBy : "Anonymous";

                connection.Open();
                await connection.QueryAsync(SkillsDocumentsQueries.InsertSkillsDocumentAsync, new { createdAt, 
                    createdBy, DataValueKeys =
                    JsonConvert.SerializeObject(skillsDocument.DataValueKeys),  skillsDocument.ReferenceCode});
                
            }
        }



        public async Task<SkillsDocument?> GetByIdAsync(int id)
        {
            using (var connection = dbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<SkillsDocument>(SkillsDocumentsQueries.GetSkillsDocumentByIdAsync, new { Id = id });
                return result;
            }
        }

        public async Task<SkillsDocument?> GetByReferenceCodeAsync(string referenceCode)
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
                DateTime updatedAt = DateTime.UtcNow;
                string updatedBy = skillsDocument.UpdatedBy != null ? skillsDocument.UpdatedBy : "Anonymous";

                connection.Open();
                _=await connection.QueryAsync(SkillsDocumentsQueries.UpdateSkillsDocument, new { updatedAt, updatedBy, 
                    skillsDocument.DataValueKeys, skillsDocument.Id });
                
            }
        }

        


    }
}