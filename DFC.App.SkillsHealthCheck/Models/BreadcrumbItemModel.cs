﻿using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.Models
{
    [ExcludeFromCodeCoverage]
    public class BreadcrumbItemModel
    {
        public string? Route { get; set; }

        public string? Title { get; set; }
    }
}
