﻿using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels.Home
{
    [ExcludeFromCodeCoverage]
    public class BodyViewModel : IBodyPostback
    {
        public RightBarViewModel RightBarViewModel { get; set; }

        public string ListTypeFields { get; set; }
    }
}
