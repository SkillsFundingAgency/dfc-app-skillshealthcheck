using System.Diagnostics;

using DFC.App.SkillsHealthCheck.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    public class HomeController : Controller
    {
        public const string ThisViewCanonicalName = "home";

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Comment to be deleted
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
