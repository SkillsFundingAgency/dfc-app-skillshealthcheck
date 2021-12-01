using System;
using System.Collections.Generic;
using System.Text;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages
{
    /// <summary>
    /// Generic Response message from API calls
    /// inherited by most messages
    /// </summary>
    public class GenericResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GenericResponse"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the type of the error.
        /// </summary>
        /// <value>
        /// The type of the error.
        /// </value>
        public ErrorType ErrorType { get; set; }
    }
}
