﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class HtmlHeadViewModel
    {
        public string? Title { get; set; }

        public Uri? CanonicalUrl { get; set; }

        public string? Description { get; set; }

        public string? Keywords { get; set; }
    }
}
