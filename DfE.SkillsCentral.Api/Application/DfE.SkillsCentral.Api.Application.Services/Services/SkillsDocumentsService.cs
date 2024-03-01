using DFC.SkillsCentral.Api.Application.Interfaces.Services;
using DFC.SkillsCentral.Api.Application.Interfaces.Repositories;
using DFC.SkillsCentral.Api.Domain.Models;

namespace DfE.SkillsCentral.Api.Application.Services.Services
{
    public class SkillsDocumentsService : ISkillsDocumentsService
    {

        private readonly ISkillsDocumentsRepository _skillsDocumentsRepository;

        public SkillsDocumentsService(ISkillsDocumentsRepository skillsDocumentsRepository)
        {
            _skillsDocumentsRepository = skillsDocumentsRepository;
        }
        public async Task<SkillsDocument> CreateSkillsDocument(SkillsDocument skillsDocument)
        {
            await _skillsDocumentsRepository.AddAsync(skillsDocument);
            return await _skillsDocumentsRepository.GetByReferenceCodeAsync(skillsDocument.ReferenceCode);
        }

        public void CalculateResults()
        {
            throw new NotImplementedException();
        }

        public string DownloadDocument()
        {
            throw new NotImplementedException();
        }

        public async Task<SkillsDocument?> GetSkillsDocument(int id)
        {

            var skillsDocument =  await _skillsDocumentsRepository.GetByIdAsync(id);
            if (skillsDocument == null)
                return default;
            return skillsDocument;
        }

        public async Task<SkillsDocument?> GetSkillsDocumentByReferenceCode(string referenceCode)
        {
            var skillsDocument = await _skillsDocumentsRepository.GetByReferenceCodeAsync(referenceCode);
            if (skillsDocument == null)
                return default;
            return skillsDocument;
        }
    }
}
