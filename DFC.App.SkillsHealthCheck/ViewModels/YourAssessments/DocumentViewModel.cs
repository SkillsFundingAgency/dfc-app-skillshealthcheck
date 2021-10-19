﻿using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels.YourAssessments
{
    [ExcludeFromCodeCoverage]
    public class DocumentViewModel
    {
        public HtmlHeadViewModel HtmlHeadViewModel { get; set; }

        public BodyViewModel BodyViewModel { get; set; }

        public BreadcrumbViewModel BreadcrumbViewModel { get; set; }
    }
}