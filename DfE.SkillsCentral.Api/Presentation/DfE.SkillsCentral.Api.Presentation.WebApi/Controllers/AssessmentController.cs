using DFC.SkillsCentral.Api.Application.Interfaces.Services;
using DFC.SkillsCentral.Api.Domain.Models;
using DfE.SkillsCentral.Api.Application.Interfaces.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Assessment = DfE.SkillsCentral.Api.Domain.Models.Assessment;

namespace DfE.SkillsCentral.Api.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        private readonly ISkillsDocumentsService skillsDocumentsService;
        private readonly IAssessmentsService assessmentsService;
        private readonly ILogger<AssessmentController> logger;

        public AssessmentController(ISkillsDocumentsService skillsDocumentsService, IAssessmentsService assessmentsService, ILogger<AssessmentController> logger)
        {
            this.skillsDocumentsService = skillsDocumentsService;
            this.assessmentsService = assessmentsService;
            this.logger = logger;
        }

        [HttpGet("{assessmentType}")]
        public async Task<ActionResult<AssessmentQuestions>> GetAssessment(string assessmentType)
        {
            try
            {
                if (string.IsNullOrEmpty(assessmentType) == true)
                { 
                    return BadRequest("No Assessment Type was provided"); 
                }

                var result = await assessmentsService.GetAssessmentQuestions(assessmentType);

                if (result == null)
                {
                    return NoContent();
                }
                else
                {
                    return Ok(JsonConvert.SerializeObject(result));
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, $"{nameof(GetAssessment)} exception");
                return StatusCode(500, $"An error has occurred");
            }
        }
    }
}
