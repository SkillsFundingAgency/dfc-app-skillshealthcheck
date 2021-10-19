﻿using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;

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
        /// Downloads the document.
        /// </summary>
        /// <param name="downloadDocumentRequest">The download document request.</param>
        /// <returns></returns>
        DownloadDocumentResponse DownloadDocument(DownloadDocumentRequest downloadDocumentRequest);

        /// <summary>
        /// Updates the skills document.
        /// </summary>
        /// <param name="updateSkillsDocumentRequest">The update skills document request.</param>
        /// <returns></returns>
        UpdateSkillsDocumentResponse UpdateSkillsDocument(UpdateSkillsDocumentRequest updateSkillsDocumentRequest);
    }
}