using System;
using System.Reflection.Metadata;
using System.ServiceModel;
using System.Threading.Tasks;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.SkillsCentral.Api.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestSharp;


namespace DFC.App.SkillsHealthCheck.Services.SkillsCentralAPI.Services
{
    public class SkillsHealthCheckService : ISkillsHealthCheckService
    {
        private readonly RestClient client;
        private readonly IOptions<SkillsCentralSettings> skillsCentralSettings;


        public SkillsHealthCheckService(RestClient client, IOptions<SkillsCentralSettings> settings)
        {
            this.client = client;
            this.skillsCentralSettings = settings;
        }

        public async Task<AssessmentQuestions> GetAssessmentQuestions(string assessmentType)
        {
            try
            {
                var request = new RestRequest($"{skillsCentralSettings.Value.SkillsCentralApiUrl}Assessment/{assessmentType}");
                var result = await client.GetAsync<AssessmentQuestions>(request);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<byte[]> GenerateWordDoc(int documentId)
        {
            return null;
        }
        public async Task<byte[]> GeneratePDF(int documentId)
        {
            return null;
        }
        public async Task<SkillsDocument> GetSkillsDocument(int documentId)
        {
            try
            {
                var request = new RestRequest($"{skillsCentralSettings.Value.SkillsCentralApiUrl}SkillsDocument/DocumentId/{documentId}");
                var result = await client.GetAsync<SkillsDocument>(request);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<SkillsDocument> GetSkillsDocumentByReferenceCode(string referenceCode)
        {
            try
            {
                var request = new RestRequest($"{skillsCentralSettings.Value.SkillsCentralApiUrl}SkillsDocument/ReferenceCode/{referenceCode}");
                var result = await client.GetAsync<SkillsDocument>(request);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<SkillsDocument> CreateSkillsDocument([FromBody] SkillsDocument document)
        {
            try
            {
                var request = new RestRequest($"{skillsCentralSettings.Value.SkillsCentralApiUrl}SkillsDocument/", Method.Post).AddBody(document);
                var result = await client.PostAsync<SkillsDocument>(request);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<SkillsDocument> SaveSkillsDocument([FromBody] SkillsDocument document)
        {
            try
            {
                var request = new RestRequest($"{skillsCentralSettings.Value.SkillsCentralApiUrl}SkillsDocument/", Method.Patch).AddBody(document);
                var result = await client.PatchAsync<SkillsDocument>(request);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public GetSkillsDocumentIdResponse GetSkillsDocumentByIdentifier(string Identifier)
        {
            throw new NotImplementedException();
        }

        public CreateSkillsDocumentResponse CreateSkillsDocument(CreateSkillsDocumentRequest createSkillsDocumentRequest)
        {
            throw new NotImplementedException();
        }

        public GetAssessmentQuestionResponse GetAssessmentQuestion(GetAssessmentQuestionRequest getAssessmentQuestionRequest)
        {
            throw new NotImplementedException();
        }

        public SaveQuestionAnswerResponse SaveQuestionAnswer(SaveQuestionAnswerRequest saveQuestionAnswerRequest)
        {
            throw new NotImplementedException();
        }

        public Task<DocumentStatus> RequestDownloadAsync(long documentId, string formatter, string requestedBy)
        {
            throw new NotImplementedException();
        }

        public Task<DocumentStatus> QueryDownloadStatusAsync(long documentId, string formatter)
        {
            throw new NotImplementedException();
        }

        public DownloadDocumentResponse DownloadDocument(DownloadDocumentRequest downloadDocumentRequest)
        {
            throw new NotImplementedException();
        }

        
    }
}
