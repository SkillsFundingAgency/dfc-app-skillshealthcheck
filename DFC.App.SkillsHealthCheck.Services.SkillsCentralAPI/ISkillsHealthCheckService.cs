﻿using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.SkillsCentral.Api.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces
{
    public interface ISkillsHealthCheckService
    {
        GetSkillsDocumentResponse GetSkillsDocument(GetSkillsDocumentRequest getSkillsDocumentRequest);

        GetSkillsDocumentIdResponse GetSkillsDocumentByIdentifier(string Identifier);


        Task<SkillsDocument> CreateSkillsDocument([FromBody] SkillsDocument document);


        GetAssessmentQuestionResponse GetAssessmentQuestion(GetAssessmentQuestionRequest getAssessmentQuestionRequest);

        SaveQuestionAnswerResponse SaveQuestionAnswer(SaveQuestionAnswerRequest saveQuestionAnswerRequest);

        Task<DocumentStatus> RequestDownloadAsync(long documentId, string formatter, string requestedBy);

        Task<DocumentStatus> QueryDownloadStatusAsync(long documentId, string formatter);


        DownloadDocumentResponse DownloadDocument(DownloadDocumentRequest downloadDocumentRequest);
    }
}
