using DFC.SkillsCentral.Api.Application.Interfaces.Services;
using DfE.SkillsCentral.Api.Application.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DfE.SkillsCentral.Api.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentGenerationController : ControllerBase
    {
        private readonly IDocumentsGenerationService _documentsGenerationService;
        private readonly ILogger<SkillsDocumentController> _logger;

        public DocumentGenerationController(IDocumentsGenerationService documentsGenerationService, ILogger<SkillsDocumentController> logger)
        {
            _documentsGenerationService = documentsGenerationService;
            _logger = logger;
        }

        [HttpGet("docx/{documentId}")]
        public async Task<IActionResult> GenerateWordDoc(int documentId)
        {
            try
            {
                
                var result = await _documentsGenerationService.GenerateWordDoc(documentId);
                
                if (result == null)
                {
                    return NoContent();
                }
                else
                {
                    return File(result, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GenerateWordDoc)} exception");

                return StatusCode(500, $"An error has occurred");
            }
        }

        [HttpGet("pdf/{documentId}")]
        public async Task<IActionResult> GeneratePDF(int documentId)
        {
            try
            {

                var result = await _documentsGenerationService.GeneratePDF(documentId);

                if (result == null)
                {
                    return NoContent();
                }
                else
                {
                    return File(result, "application/pdf");
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GeneratePDF)} exception");

                return StatusCode(500, $"An error has occurred");
            }
        }

        
    }
}
