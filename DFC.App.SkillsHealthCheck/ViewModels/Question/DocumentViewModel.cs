﻿using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels.Question
{
    [ExcludeFromCodeCoverage]
    public class DocumentViewModel
    {
        public HtmlHeadViewModel HtmlHeadViewModel { get; set; }

        public BreadcrumbViewModel BreadcrumbViewModel { get; set; }

        public BodyViewModel BodyViewModel { get; set; }
    }
}
