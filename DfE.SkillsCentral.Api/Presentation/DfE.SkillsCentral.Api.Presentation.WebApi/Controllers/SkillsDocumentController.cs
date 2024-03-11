using DFC.SkillsCentral.Api.Application.Interfaces.Services;
using DFC.SkillsCentral.Api.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DfE.SkillsCentral.Api.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsDocumentController : ControllerBase
    {
        private readonly ISkillsDocumentsService skillsDocumentsService;
        private readonly IAssessmentsService assessmentsService;
        private readonly ILogger<SkillsDocumentController> logger;

        public SkillsDocumentController(ISkillsDocumentsService skillsDocumentsService, IAssessmentsService assessmentsService, ILogger<SkillsDocumentController> logger)
        {
            this.skillsDocumentsService = skillsDocumentsService;
            this.assessmentsService = assessmentsService;
            this.logger = logger;
        }

        [HttpGet("{referenceCode}")]
        public async Task<ActionResult<SkillsDocument>> GetSkillsDocumentByReferenceCode(string referenceCode)
        {
            try
            {
                //TODO: Validate before passing to service
                bool isValid = Guid.TryParse(referenceCode, out _);
                if (isValid == false)
                { return StatusCode(500, $"An invalid Reference Code was provided"); }

                var result = await skillsDocumentsService.GetSkillsDocumentByReferenceCode(referenceCode);
                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"{nameof(GetSkillsDocumentByReferenceCode)} exception");
                return StatusCode(500, $"An error has occurred");
            }

        }

        [HttpPost]
        public async Task<ActionResult<SkillsDocument>> CreateSkillsDocument([FromBody] SkillsDocument document)
        {
            try
            {
                //TODO: Validate before passing to service
                if (document == null)
                { return StatusCode(500, $"No Skills Document was provided"); }

                var result = await skillsDocumentsService.CreateSkillsDocument(document);
                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e,$"{nameof(CreateSkillsDocument)} exception");

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
