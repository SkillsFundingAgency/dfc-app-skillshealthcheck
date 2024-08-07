﻿using DFC.SkillsCentral.Api.Application.Interfaces.Repositories;
using DFC.SkillsCentral.Api.Application.Interfaces.Services;
using DFC.SkillsCentral.Api.Domain.Models;
using DfE.SkillsCentral.Api.Application.DocumentsFormatters;
using DfE.SkillsCentral.Api.Application.Interfaces.Repositories;
using DfE.SkillsCentral.Api.Domain.Models;
using DfE.SkillsCentral.Api.Application.DocumentsFormatters;


namespace DfE.SkillsCentral.Api.Application.Services.Services
{

    public class DocumentsGenerationService : IDocumentsGenerationService
    {
        private readonly ISkillsDocumentsRepository _skillsDocumentsRepository;
        private readonly IAnswersRepository _answersRepository;
        private readonly IJobFamiliesRepository _jobFamiliesRepository;


        public DocumentsGenerationService(ISkillsDocumentsRepository skillsDocumentsRepository, IAnswersRepository answersRepository, IJobFamiliesRepository jobFamiliesRepository)
        {
            _answersRepository = answersRepository;
            _skillsDocumentsRepository = skillsDocumentsRepository;
            _jobFamiliesRepository = jobFamiliesRepository;
        }

        public async Task<byte[]> GenerateWordDoc(int documentId)
        {
            var document = await _skillsDocumentsRepository.GetByIdAsync(documentId);
            var formattedDoc = GenericOpenOfficeXMLFormatter.FormatDocumentWithATemplate(document, "SHCFullReport", _jobFamiliesRepository);
            return formattedDoc;
        }
        
        public async Task<byte[]> GeneratePDF(int documentId)
        {
            //generate word document as normal, then convert it
            var docxContent = await GenerateWordDoc(documentId);
            return DocxConverter.ConvertDocx(docxContent, DocxTargetFormat.PDF);
        }

        

    }
}
