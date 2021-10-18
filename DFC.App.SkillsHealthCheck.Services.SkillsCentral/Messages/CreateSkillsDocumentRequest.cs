using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
using System;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages
{
    /// <summary>
    /// Create Skills document request
    ///  </summary>
    [Serializable]
    public class CreateSkillsDocumentRequest
    {
        /// <summary>
        /// Gets or sets the skills document.
        /// </summary>
        /// <value>
        /// The skills document.
        /// </value>
        public SkillsDocument SkillsDocument { get; set; }
    }
}
