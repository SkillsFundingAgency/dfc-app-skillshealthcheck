using System.Diagnostics.CodeAnalysis;
using DFC.App.SkillsHealthCheck.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DFC.App.SkillsHealthCheck.Controllers
{

    [ExcludeFromCodeCoverage]
    public class SkillsHealthCheckController : Controller
    {
        public const string RegistrationPath = "skills-healthcheck";


        public SkillsHealthCheckController()
        {
        }


        [HttpGet]
        [Route("skills-health-check/your-assessments/body")]
        public async Task<IActionResult> YourAssessmentsBody()
        {
            return this.NegotiateContentResult(null);
        }

        [HttpGet]
        [Route("skills-health-check/save-my-progress/body")]
        public async Task<IActionResult> SaveMyProgressBody()
        {
            return this.NegotiateContentResult(null);
        }

        [HttpGet]
        [Route("skills-health-check/question/body")]
        public async Task<IActionResult> QuestionBody()
        {
            return this.NegotiateContentResult(null);
        }
    }
}