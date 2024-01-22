using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Data.Models.ContentModels;
using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.ViewModels;
using DFC.Common.SharedContent.Pkg.Netcore.Interfaces;
using DFC.Compui.Cosmos.Contracts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    public class HealthController : Controller
    {
        private readonly ILogger<HealthController> logger;
        //private readonly IDocumentService<SharedContentItemModel> sharedContentItemDocumentService;
        private readonly string resourceName = typeof(Program).Namespace!;
        private readonly ISharedContentRedisInterface sharedContentRedis;

        public HealthController(ILogger<HealthController> logger, ISharedContentRedisInterface sharedContentRedis)
        {
            this.logger = logger;
            //this.sharedContentItemDocumentService = sharedContentItemDocumentService;
            this.sharedContentRedis = sharedContentRedis;
        }

        [HttpGet]
        [Route("skills-health-check/health")]
        public async Task<IActionResult> HealthView()
        {
            var result = await Health();

            return result;
        }

        [HttpGet]
        [Route("health")]
        public async Task<IActionResult> Health()
        {
            logger.LogInformation("Generating Health report");



            //var isHealthy = await sharedContentItemDocumentService.PingAsync();

            //if (isHealthy)
            //{
            //    const string message = "Document store is available";
            //    logger.LogInformation($"{nameof(Health)} responded with: {resourceName} - {message}");

            //    var viewModel = CreateHealthViewModel(message);

            //    logger.LogInformation("Generated Health report");

            //    return this.NegotiateContentResult(viewModel, viewModel.HealthItems);
            //}

            //logger.LogError($"{nameof(Health)}: Ping to {resourceName} has failed");


            return StatusCode((int)HttpStatusCode.ServiceUnavailable);
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