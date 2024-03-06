using Dapper;
using DFC.SkillsCentral.Api.Application.Interfaces.Repositories;
using DFC.SkillsCentral.Api.Domain.Models;
using DFC.SkillsCentral.Api.Infrastructure.Queries;
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
                connection.Open();
                await connection.QueryAsync(SkillsDocumentsQueries.InsertSkillsDocumentAsync, new { skillsDocument.CreatedAt, 
                    skillsDocument.CreatedBy, DataValueKeys =
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
                //result.DataValueKeys = JsonConvert.DeserializeObject(result.DataValueKeys)


                return result;
            }
        }

        public async Task UpdateAsync(SkillsDocument skillsDocument)
        {
            using (var connection = dbContext.CreateConnection())
            {
                connection.Open();
                _=await connection.QueryAsync(SkillsDocumentsQueries.UpdateSkillsDocument, new { skillsDocument.UpdatedAt, skillsDocument.UpdatedBy, 
                    skillsDocument.DataValueKeys, skillsDocument.Id });
                
            }
        }

        


    }
}