using DFC.App.SkillsHealthCheck.Controllers;
using DFC.App.SkillsHealthCheck.Models;
using DFC.Compui.Sessionstate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Web;

namespace DFC.App.SkillsHealthCheck.Filters
{
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
                authorised = sessionDataModel?.State != null && sessionDataModel.State.DocumentId != 0;
            }

            if (!authorised)
            {
                context.Result = new RedirectResult($"{BaseController<SessionTimeoutController>.SessionTimeoutURL}?returnurl={HttpUtility.UrlEncode(path)}");
            }
        }
    }
}
