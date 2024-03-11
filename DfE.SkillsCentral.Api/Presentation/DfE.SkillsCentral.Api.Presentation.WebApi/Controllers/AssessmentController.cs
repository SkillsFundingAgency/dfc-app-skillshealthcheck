using DFC.SkillsCentral.Api.Application.Interfaces.Services;
using DFC.SkillsCentral.Api.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<Assessment>> GetAssessment(string assessmentType)
        {
            try
            {
                //TODO: Validate before sending to service
                if (assessmentType == null)
                { return StatusCode(500, $"No Assessment Type was provided"); }

                var result = await assessmentsService.GetAssessmentQuestions(assessmentType);
                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"{nameof(GetAssessment)} exception");
                return StatusCode(500, $"An error has occurred");
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveSkillsDocument([FromBody] SkillsDocument document)
        {
            try
            {
                //TODO: Validate before passing to service
                if (document == null)
                { return StatusCode(500, $"No Skills Document was provided"); }

                //TODO: Review do we want to return the document on Save also, similar to create?
                await assessmentsService.SaveSkillsDocument(document);
                return Ok();
            }
            catch (Exception e)
            {
                logger.LogError(e, $"{nameof(SaveSkillsDocument)} exception");

                return StatusCode(500, $"An error has occurred");
            }
        }


    }
}
