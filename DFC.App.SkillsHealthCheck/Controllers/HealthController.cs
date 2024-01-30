using System.Collections.Generic;
using System.Threading.Tasks;
using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    public class HealthController : Controller
    {
        private readonly ILogger<HealthController> logger;
        private readonly string resourceName = typeof(Program).Namespace!;

        public HealthController(ILogger<HealthController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        [Route("skills-health-check/health")]
        public async Task<IActionResult> HealthView()
        {
            return await Health();
        }

        [HttpGet]
        [Route("health")]
        public async Task<IActionResult> Health()
        {
            const string message = "Document store is available";
            logger.LogInformation($"{nameof(Health)} responded with: {resourceName} - {message}");

            var viewModel = CreateHealthViewModel(message);

            return this.NegotiateContentResult(viewModel, viewModel.HealthItems);
        }

        [HttpGet]
        [Route("health/ping")]
        public IActionResult Ping()
        {
            logger.LogInformation($"{nameof(Ping)} has been called");

            return Ok();
        }

        private HealthViewModel CreateHealthViewModel(string message)
        {
            return new HealthViewModel
            {
                HealthItems = new List<HealthItemViewModel>
                {
                    new HealthItemViewModel
                    {
                        Service = resourceName,
                        Message = message,
                    },
                },
            };
        }
    }
}