using System.ComponentModel;

namespace DFC.App.SkillsHealthCheck.Enums
{
    public enum SaveMyProgressOption
    {
        [Description("None")]
        None,
        [Description("Send me an email with a link")]
        Email,
        [Description("Get a reference code")]
        ReferenceCode,
    }
}
