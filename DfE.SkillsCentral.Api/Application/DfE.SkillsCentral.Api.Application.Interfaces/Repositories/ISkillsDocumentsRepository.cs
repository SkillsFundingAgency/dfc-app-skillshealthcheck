using DFC.SkillsCentral.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC.SkillsCentral.Api.Application.Interfaces.Repositories
{
    public interface ISkillsDocumentsRepositiry :IRepository<SkillsDocument>
    {
        Task<string> AddAsync(SkillsDocument skillsDocument);
        Task<string> UpdateAsync(SkillsDocument skillsDocument);
        Task<string> DeleteAsync(int id);
        Task<SkillsDocument> GetByReferenceCodeAsync(string referenceCode);
    }
}
