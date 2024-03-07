using DFC.SkillsCentral.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC.SkillsCentral.Api.Application.Interfaces.Services
{
    public interface ISkillsDocumentsService
    {
        Task<SkillsDocument?> GetSkillsDocument(int id);

        Task<SkillsDocument?> GetSkillsDocumentByReferenceCode(string referenceCode);

        Task<SkillsDocument> CreateSkillsDocument(SkillsDocument skillsDocument);
    }
}
