using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace DFC.App.SkillsHealthCheck.ViewModels.Home
{
    [ExcludeFromCodeCoverage]
    public class BodyViewModel
    {
        public string YourAssessmentsURL { get; set; }
        public RightBarViewModel RightBarViewModel { get; set; }
        public IEnumerable<string> ListTypeFields { get; set; }
        public string ListTypeFieldsString { get; set; }
    }
}
