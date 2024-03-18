using System;
using System.ServiceModel;
using System.Threading.Tasks;
using AutoMapper;

using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Extensions;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Mappers;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.SkillsCentral.Api.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using SkillsDocumentService;

using Level = DFC.App.SkillsHealthCheck.Services.SkillsCentralAPI.Enums.Level;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentralAPI.Services
{
    public class SkillsHealthCheckService : ISkillsHealthCheckService
    {
        private ISkillsCentralService _skillsCentralService;

        public SkillsHealthCheckService(ISkillsCentralService skillsCentralService)
        {
            _skillsCentralService = skillsCentralService;
        }

        public async Task<ActionResult> GetAssessment(string assessmentType)
        {
            return null;
        }
        public async Task<ActionResult> GenerateWordDoc(int documentId)
        {
            return null;
        }
        public async Task<ActionResult> GeneratePDF(int documentId)
        {
            return null;
        }
        public async Task<ActionResult> GetSkillsDocumentByReferenceCode(string referenceCode)
        {
            return null;
        }
        public async Task<ActionResult> CreateSkillsDocument([FromBody] SkillsDocument document)
        {
            return null;
        }
        public async Task<ActionResult> SaveSkillsDocument([FromBody] SkillsDocument document)
        {
            return null;
        }

        
    }
}
