using DFC.App.SkillsHealthCheck.Controllers;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.ViewModels.Home;
using DFC.Compui.Sessionstate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Web;

namespace DFC.App.SkillsHealthCheck.Filters
{
    [ExcludeFromCodeCoverage]
    public class SessionStateFilter : IActionFilter
    {
        private readonly ISessionStateService<SessionDataModel> sessionStateService;
        private readonly string[] urlRegionParts = new[] { "/htmlhead", "/breadcrumb" };

        public SessionStateFilter(ISessionStateService<SessionDataModel> sessionStateService)
        {
            this.sessionStateService = sessionStateService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var path = request.Path.ToString();
            if (urlRegionParts.Any(p => path.EndsWith(p, System.StringComparison.InvariantCultureIgnoreCase)))
            {
                // We are not fussed about checking session state for the htmlhead and breadcrumb requests
                return;
            }
            else if (path.EndsWith("/body", System.StringComparison.CurrentCultureIgnoreCase))
            {
                path = path.Remove(path.Length - 5);
            }

            var authorised = false;
            var compositeSessionId = request.CompositeSessionId();
            if (compositeSessionId.HasValue)
            {
                var sessionDataModel = sessionStateService.GetAsync(compositeSessionId.Value).Result;
                var sessionTime = (DateTime)sessionDataModel.State.Timestamp;
                var sessionTimeIn30Min = sessionTime.AddMinutes(30);

                authorised = sessionDataModel?.State != null && sessionTimeIn30Min > DateTime.Now;
            }

            if (!authorised)
            {
                context.Result = new RedirectResult(path);
            }
        }
    }
}
