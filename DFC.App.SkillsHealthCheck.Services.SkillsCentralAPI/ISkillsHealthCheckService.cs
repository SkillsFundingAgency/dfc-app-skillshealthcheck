using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using System.Threading.Tasks;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces
{
    public interface ISkillsHealthCheckService
    {
        /// <summary>
        /// Gets the skills document.
        /// </summary>
        /// <param name="getSkillsDocumentRequest">The get skills document request.</param>
        /// <returns></returns>
        GetSkillsDocumentResponse GetSkillsDocument(GetSkillsDocumentRequest getSkillsDocumentRequest);

        /// <summary>
        /// Gets the skills document.
        /// </summary>
        /// <param name="Identifier">the Identifier of the skills document</param>
        /// <returns></returns>
        GetSkillsDocumentIdResponse GetSkillsDocumentByIdentifier(string Identifier);


        /// <summary>
        /// Creates the skills document.
        /// </summary>
        /// <param name="createSkillsDocumentRequest">The create skills document request.</param>
        /// <returns></returns>
        CreateSkillsDocumentResponse CreateSkillsDocument(CreateSkillsDocumentRequest createSkillsDocumentRequest);

        /// <summary>
        /// Gets the list type fields.
        /// </summary>
        /// <param name="getListTypeFieldsRequest">The get list type fields request.</param>
        /// <returns></returns>
        GetListTypeFieldsResponse GetListTypeFields(GetListTypeFieldsRequest getListTypeFieldsRequest);

        /// <summary>
        /// Gets the assessment question.
        /// </summary>
        /// <param name="getAssessmentQuestionRequest">The get assessment question request.</param>
        /// <returns></returns>
        GetAssessmentQuestionResponse GetAssessmentQuestion(GetAssessmentQuestionRequest getAssessmentQuestionRequest);

        /// <summary>
        /// Saves the question answer.
        /// </summary>
        /// <param name="saveQuestionAnswerRequest">The save question answer request.</param>
        /// <returns></returns>
        SaveQuestionAnswerResponse SaveQuestionAnswer(SaveQuestionAnswerRequest saveQuestionAnswerRequest);

        /// <summary>
        /// Request the preparation of a document for download.
        /// </summary>
        /// <param name="documentId">The document id.</param>
        /// <param name="formatter">The document format type.</param>
        /// <param name="documentId">Name associated with request.</param>
        /// <returns></returns>
        Task<DocumentStatus> RequestDownloadAsync(long documentId, string formatter, string requestedBy);

        /// <summary>
        /// Query download status to see if document is ready.
        /// </summary>
        /// <param name="documentId">The document id.</param>
        /// <param name="formatter">The document format type.</param>
        /// <returns></returns>
        Task<DocumentStatus> QueryDownloadStatusAsync(long documentId, string formatter);


        /// <summary>
        /// Downloads the document.
        /// </summary>
        /// <param name="downloadDocumentRequest">The download document request.</param>
        /// <returns></returns>
        DownloadDocumentResponse DownloadDocument(DownloadDocumentRequest downloadDocumentRequest);
    }
}
