using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Html;

namespace DFC.App.SkillsHealthCheck.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class BaseBodyViewModel
    {
        public HtmlString? Body { get; set; } = new HtmlString("Unknown content");
    }
}
