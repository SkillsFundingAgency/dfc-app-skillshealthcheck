using DFC.App.SkillsHealthCheck.Controllers;
using DFC.App.SkillsHealthCheck.Models;
using DFC.Compui.Sessionstate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

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
                authorised = sessionDataModel?.State != null && sessionDataModel.State.DocumentId != 0;
            }

            if (!authorised)
            {
                var popupHTML = @"<div id=""popup1"" class=""overlay"">
                    <div class=""popup"">
                        <div class=""govuk-grid-row"">
                            <div class=""govuk-grid-column-two-thirds"">
                                <h2 class=""heading-m"">Your current session is about to end due to 30 minutes of inactivity</h2>
                                <p>Select <b>'extend my session'</b> if you want to continue or click <b>'end my session'</b> to leave.</p>
                                <p>This message will show for 5 minutes. If there is no further activity, your session will automatically end.</p>
                        </div>
                        <button class=""govuk-button button-start"" data-module=""govuk-button"" href=""#popup1"">
                        Extend Session
                        </button>
                        <a class=""govuk-link"" href=""@Model.HomePageUrl"" rel=""external"">Leave skills health check</a>
                </div>
            </div>
    </div>";
                context.HttpContext.Response.Body.Write(Encoding.UTF8.GetBytes(popupHTML));
                context.HttpContext.Response.Body.Flush();
                context.Result = new EmptyResult();
            }
        }
    }
}
