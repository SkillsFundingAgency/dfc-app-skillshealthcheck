// -----------------------------------------------------------------------
// <copyright file="InterestCategory.cs" company="tesl.com">
// Trinity Expert Systems
// </copyright>
// -----------------------------------------------------------------------
namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    using System.Collections.Generic;

    /// <summary>
    /// InterestCategory - Entity to store personal interest category information
    /// </summary>
    public class InterestCategory
    {
        #region | Properties |
        /// <summary>
        /// Get / Set interest category name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get / Set interest category display name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Get / Set interest category max score 
        /// </summary>
        public int MaxScore { get; set; }

        /// <summary>
        /// Get / Set interest category colour codes
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Get / Set interest belongs to the currect interest categories
        /// </summary>
        public List<Interest> Interests { get; set; }
        #endregion
    }
}
