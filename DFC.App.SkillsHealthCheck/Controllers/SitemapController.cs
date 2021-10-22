using System.Linq;
using System.Net.Mime;

using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    public class SitemapController : Controller
    {
        public const string SitemapViewCanonicalName = "sitemap";

        private readonly ILogger<SitemapController> logger;

        public SitemapController(ILogger<SitemapController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        [Route("pages/sitemap")]
        public IActionResult SitemapView()
        {
            var result = Sitemap();

            return result;
        }

        [HttpGet]
        [Route("/sitemap.xml")]
        public IActionResult Sitemap()
        {
            logger.LogInformation("Generating Sitemap");

            var sitemapUrlPrefix = $"{Request.GetBaseAddress()}{HomeController.RegistrationPath}";
            var sitemap = new Sitemap();

            // add the defaults
            sitemap.Add(new SitemapLocation
            {
                Url = sitemapUrlPrefix,
                Priority = 1,
            });

            if (!sitemap.Locations.Any())
            {
                return NoContent();
            }

            var xmlString = sitemap.WriteSitemapToString();

            logger.LogInformation("Generated Sitemap");

            return Content(xmlString, MediaTypeNames.Application.Xml);
        }
    }
}