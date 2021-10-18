using System;
using System.Collections.Generic;
using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    public class BaseController : Controller
    {
        public const string RegistrationPath = "skills-health-check";
        public const string DefaultPageTitleSuffix = "Skills Health Check | National Careers Service";

        protected HtmlHeadViewModel GetHtmlHeadViewModel(string pageTitle)
        {
            return new HtmlHeadViewModel
            {
                CanonicalUrl = new Uri($"{Request.GetBaseAddress()}/skills-health-check", UriKind.RelativeOrAbsolute),
                Title = $"{pageTitle} | {DefaultPageTitleSuffix}",
            };
        }

        protected static BreadcrumbViewModel BuildBreadcrumb()
        {
            return new BreadcrumbViewModel
            {
                Breadcrumbs = new List<BreadcrumbItemViewModel>
                {
                    new BreadcrumbItemViewModel
                    {
                        Route = "/",
                        Title = "Home",
                    },
                },
            };
        }
    }
}
