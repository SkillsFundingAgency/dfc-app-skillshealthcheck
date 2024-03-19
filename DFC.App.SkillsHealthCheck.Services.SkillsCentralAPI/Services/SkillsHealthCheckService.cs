using System;
using System.ServiceModel;
using System.Threading.Tasks;

using DFC.SkillsCentral.Api.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestSharp;


namespace DFC.App.SkillsHealthCheck.Services.SkillsCentralAPI.Services
{
    public class SkillsHealthCheckService : ISkillsHealthCheckService
    {
        private readonly IRestClient client;
        private readonly IOptions<SkillsCentralSettings> skillsCentralSettings;


        public SkillsHealthCheckService(IRestClient client, IOptions<SkillsCentralSettings> settings)
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
            return null;
        }
        public async Task<SkillsDocument> GetSkillsDocumentByReferenceCode(string referenceCode)
        {
            return null;
        }
        public async Task<SkillsDocument> CreateSkillsDocument([FromBody] SkillsDocument document)
        {
            try
            {
                var request = new RestRequest($"{skillsCentralSettings.Value.SkillsCentralApiUrl}SkillsDocument/", Method.Post).AddObjectStatic(document);
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
            return null;
        }

        
    }
}
