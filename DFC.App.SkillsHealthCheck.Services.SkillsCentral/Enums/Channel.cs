using System.ComponentModel.DataAnnotations;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums
{
    public enum Channel
    {
        [Display(Name = "Email")]
        Email = 0,
        [Display(Name = "Text")]
        Text = 1,
        [Display(Name = "Phone")]
        Phone = 2
    }
}
