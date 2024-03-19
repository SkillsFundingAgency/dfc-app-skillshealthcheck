using DFC.SkillsCentral.Api.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentralAPI.Services
{
    public interface ISkillsHealthCheckService
    {
        Task<SkillsDocument> SaveSkillsDocument(SkillsDocument document);

        Task<SkillsDocument> CreateSkillsDocument(SkillsDocument document);

        Task<SkillsDocument> GetSkillsDocumentByReferenceCode(string referenceCode);

        Task<SkillsDocument> GetSkillsDocument(int documentId);

        Task<byte[]> GeneratePDF(int documentId);

        Task<byte[]> GenerateWordDoc(int documentId);

        Task<AssessmentQuestions> GetAssessmentQuestions(string assessmentType);
    }
}