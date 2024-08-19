// -----------------------------------------------------------------------
// <copyright file="PersonalStyle.cs" company="tesl.com">
// Trinity Expert Systems
// </copyright>
// -----------------------------------------------------------------------
namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    /// <summary>
    /// Entity to store Personal Style Infomation
    /// </summary>
    public class PersonalStyle
    {
        #region | Properties |
        /// <summary>
        /// Set / Get Category name
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Set / Get Right Name
        /// </summary>
        public string RightName { get; set; }       
 
        /// <summary>
        /// Set / Get Strength Left
        /// </summary>
        public string StrengthLeft { get; set; }

        /// <summary>
        /// Set / Get Strength Mid
        /// </summary>
        public string StrengthMid { get; set; }

        /// <summary>
        /// Set / Get Strength Right
        /// </summary>
        public string StrengthRight { get; set; }

        /// <summary>
        /// Set / Get Challenge Left
        /// </summary>
        public string ChallengeLeft { get; set; }

        /// <summary>
        /// Set / Get Challenge Mid
        /// </summary>
        public string ChallengeMid { get; set; }

        /// <summary>
        /// Set / Get Challenge Right
        /// </summary>
        public string ChallengeRight { get; set; }

        /// <summary>
        /// Set / Get Development Left
        /// </summary>
        public string DevelopmentLeft { get; set; }

        /// <summary>
        /// Set / Get Development Right
        /// </summary>
        public string DevelopmentRight { get; set; }

        /// <summary>
        /// Set / Get Score
        /// </summary>
        public int Score { get; set; }

        #endregion
    }
}
