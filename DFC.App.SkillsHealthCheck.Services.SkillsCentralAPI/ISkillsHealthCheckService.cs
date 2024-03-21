using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.SkillsCentral.Api.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces
{
    public interface ISkillsHealthCheckService
    {
        Task<SkillsDocument> GetSkillsDocument(int documentId);

        Task<SkillsDocument> GetSkillsDocumentByReferenceCode(string referenceCode);

        Task<SkillsDocument> CreateSkillsDocument([FromBody] SkillsDocument document);

        GetAssessmentQuestionsResponse GetAssessmentQuestions(GetAssessmentQuestionsRequest getAssessmentQuestionRequest);

        Task<QuestionAnswers>GetSingleQuestionResponse (int questionNumber, string assessmentId);

        Task<SkillsDocument> SaveSkillsDocument([FromBody] SkillsDocument document);

        Task<byte[]> GenerateWordDoc(int documentId);

        Task<byte[]> GeneratePDF(int documentId);

    }
}
