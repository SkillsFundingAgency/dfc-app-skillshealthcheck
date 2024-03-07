using DFC.SkillsCentral.Api.Application.Interfaces.Services;
using DFC.SkillsCentral.Api.Application.Interfaces.Repositories;
using DFC.SkillsCentral.Api.Domain.Models;

namespace DfE.SkillsCentral.Api.Application.Services.Services
{
    public class SkillsDocumentsService : ISkillsDocumentsService
    {
        private readonly ISkillsDocumentsRepository skillsDocumentsRepository;

        public SkillsDocumentsService(ISkillsDocumentsRepository skillsDocumentsRepository)
        {
            this.skillsDocumentsRepository = skillsDocumentsRepository;
        }
        public async Task<SkillsDocument> CreateSkillsDocument(SkillsDocument skillsDocument)
        {
            await skillsDocumentsRepository.AddAsync(skillsDocument);
            return await skillsDocumentsRepository.GetByReferenceCodeAsync(skillsDocument.ReferenceCode);
        }

        public async Task<SkillsDocument?> GetSkillsDocument(int id)
        {
            var skillsDocument =  await skillsDocumentsRepository.GetByIdAsync(id);
            return skillsDocument;
        }

        public async Task<SkillsDocument?> GetSkillsDocumentByReferenceCode(string referenceCode)
        {
            var skillsDocument = await skillsDocumentsRepository.GetByReferenceCodeAsync(referenceCode);
            return skillsDocument;
        }
    }
}
