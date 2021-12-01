using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using System;
using System.Collections.Generic;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models
{
    [Serializable]
    public class SkillsDocument
    {

        public SkillsDocument()
        {
            SkillsDocumentDataValues = new List<SkillsDocumentDataValue>();
            SkillsDocumentIdentifiers = new List<SkillsDocumentIdentifier>();
        }

        /// <summary>
        /// Gets or sets the document identifier.
        /// </summary>
        /// <value>
        /// The document identifier.
        /// </value>
        public long DocumentId { get; set; }

        /// <summary>
        /// Gets or sets the deleted at.
        /// </summary>
        /// <value>
        /// The deleted at.
        /// </value>
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// Gets or sets the deletedby.
        /// </summary>
        /// <value>
        /// The deletedby.
        /// </value>
        public string Deletedby { get; set; }

        /// <summary>
        /// Gets or sets the skills document title.
        /// </summary>
        /// <value>
        /// The skills document title.
        /// </value>
        public string SkillsDocumentTitle { get; set; }

        /// <summary>
        /// Gets or sets the type of the skills document.
        /// </summary>
        /// <value>
        /// The type of the skills document.
        /// </value>
        public string SkillsDocumentType { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the skills document data values.
        /// </summary>
        /// <value>
        /// The skills document data values.
        /// </value>
        public List<SkillsDocumentDataValue> SkillsDocumentDataValues { get; set; }

        /// <summary>
        /// Gets or sets the skills document identifiers.
        /// </summary>
        /// <value>
        /// The skills document identifiers.
        /// </value>
        public List<SkillsDocumentIdentifier> SkillsDocumentIdentifiers { get; set; }

        /// <summary>
        /// Gets or sets the expires timespan.
        /// </summary>
        /// <value>
        /// The expires timespan.
        /// </value>
        public TimeSpan? ExpiresTimespan { get; set; }

        /// <summary>
        /// Gets or sets the skills document expiry.
        /// </summary>
        /// <value>
        /// The skills document expiry.
        /// </value>
        public SkillsDocumentExpiry SkillsDocumentExpiry { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
