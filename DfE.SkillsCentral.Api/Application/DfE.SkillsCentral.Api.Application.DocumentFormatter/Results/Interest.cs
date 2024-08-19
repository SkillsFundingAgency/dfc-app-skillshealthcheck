// -----------------------------------------------------------------------
// <copyright file="Interest.cs" company="tesl.com">
// Trinity Expert Systems
// </copyright>
// -----------------------------------------------------------------------

namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    //using IMS.SkillsCentral.XmlExtensionObjects.SkillsReport.Common;

    /// <summary>
    /// Interest - Entity to store personal interest category information
    /// </summary>
    public class Interest
    {
        #region | Properties |
        /// <summary>
        /// Set / Get interest name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Set / Get internal interest name
        /// </summary>
        public string InternalName { get; set; }

        /// <summary>
        /// Set / Get category name
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Set / Get score
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Set / Get max score
        /// </summary>
        public int MaxScoreCount { get; set; }

        /// <summary>
        /// Set / Get interest area definition 
        /// </summary>
        public string WhatItInvolves { get; set; }

        /// <summary>
        /// Set / Get releated job families
        /// </summary>
        public string RelatedJobFamilies { get; set; }

        #endregion

        #region | Public Methods |

        /// <summary>
        /// Returns individual jobs in a string array
        /// </summary>
        /// <returns>array of string holding job families</returns>
        public string[] GetIndividualJobs()
        {
            string[] items = null;
            if (this.RelatedJobFamilies != null)
            {
                items = this.RelatedJobFamilies.Split(Constant.JobFamiliySplitor.ToCharArray());
            }

            return items;
        }
        #endregion
    }
}
