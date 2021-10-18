using System;
using System.Collections.Generic;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using SkillsDocumentsService;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Extensions
{
    /// <summary>
    /// Conversions to/from API models and Models
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Extracts the identifier.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        internal static Guid ExtractId(this List<SkillsDocumentIdentifier> items)
        {
            var result = Guid.Empty;
            if (items != null && items.Count > 0)
            {
                for (var counter = 0; counter < items.Count; counter++)
                {
                    if (string.IsNullOrWhiteSpace(items[counter].Value) == false &&
                        Guid.TryParse(items[counter].Value, out result) && Guid.Empty.Equals(result) == false)
                    {
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the API skills document.
        /// </summary>
        /// <param name="skillsDocument">The skills document.</param>
        /// <returns></returns>
        internal static SkillsDocument GetApiSkillsDocument(
            this Models.SkillsDocument skillsDocument)
        {
            var apiRequest = new SkillsDocument
            {
                SkillsDocumentTitle = skillsDocument.SkillsDocumentTitle,
                CreatedBy = skillsDocument.CreatedBy,
                ExpiresTimespan = skillsDocument.ExpiresTimespan,
                SkillsDocumentType = skillsDocument.SkillsDocumentType,
                ExpiresType = skillsDocument.SkillsDocumentExpiry.ConvertToDocumentExpiryEnum(),
                DataValues = skillsDocument.SkillsDocumentDataValues.GetApiDocumentDataValues(),
                Identifiers = skillsDocument.SkillsDocumentIdentifiers.GetApiSkillsDocumentIdentifiers(),
                UpdatedAt = skillsDocument.UpdatedAt
            };
            return apiRequest;
        }

        /// <summary>
        /// Converts to model skills document.
        /// </summary>
        /// <param name="apiSkillsDocument">The API skills document.</param>
        /// <returns></returns>
        internal static Models.SkillsDocument ConvertToModelSkillsDocument(
            this SkillsDocument apiSkillsDocument)
        {
            var modelSkillsDocument = new Models.SkillsDocument
            {
                SkillsDocumentTitle = apiSkillsDocument.SkillsDocumentTitle,
                CreatedBy = apiSkillsDocument.CreatedBy,
                ExpiresTimespan = apiSkillsDocument.ExpiresTimespan,
                SkillsDocumentType = apiSkillsDocument.SkillsDocumentType,
                SkillsDocumentExpiry = apiSkillsDocument.ExpiresType.ConvertToDocumentExpiry(),
                SkillsDocumentDataValues = apiSkillsDocument.DataValues.GetModelDocumentDataValues(),
                SkillsDocumentIdentifiers =
                    apiSkillsDocument.Identifiers.GetModelSkillsDocumentIdentifiers(),
                DocumentId = apiSkillsDocument.DocumentId,
                DeletedAt = apiSkillsDocument.DeletedAt,
                Deletedby = apiSkillsDocument.DeletedBy,
                CreatedAt = apiSkillsDocument.CreatedAt,
                UpdatedAt = apiSkillsDocument.UpdatedAt
            };
            return modelSkillsDocument;
        }

        /// <summary>
        /// Converts to document expiry enum.
        /// </summary>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        internal static SkillsDocumentExpiryEnum ConvertToDocumentExpiryEnum(this SkillsDocumentExpiry expiry)
        {
            switch (expiry)
            {
                case SkillsDocumentExpiry.Logical:
                    return SkillsDocumentExpiryEnum.Logical;
                case SkillsDocumentExpiry.None:
                    return SkillsDocumentExpiryEnum.None;
                case SkillsDocumentExpiry.Physical:
                    return SkillsDocumentExpiryEnum.Physical;
                default:
                    return SkillsDocumentExpiryEnum.None;
            }
        }

        /// <summary>
        /// Converts to document expiry.
        /// </summary>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        internal static SkillsDocumentExpiry ConvertToDocumentExpiry(this SkillsDocumentExpiryEnum expiry)
        {
            switch (expiry)
            {
                case SkillsDocumentExpiryEnum.Logical:
                    return SkillsDocumentExpiry.Logical;
                case SkillsDocumentExpiryEnum.None:
                    return SkillsDocumentExpiry.None;
                case SkillsDocumentExpiryEnum.Physical:
                    return SkillsDocumentExpiry.Physical;
                default:
                    return SkillsDocumentExpiry.None;
            }
        }

        /// <summary>
        /// Gets the API document data values.
        /// </summary>
        /// <param name="skillsDocumentDataValues">The skills document data values.</param>
        /// <returns></returns>
        internal static List<SkillsDocumentDataValue> GetApiDocumentDataValues(
            this IEnumerable<Models.SkillsDocumentDataValue> skillsDocumentDataValues)
        {
            var apiList = new List<SkillsDocumentDataValue>();

            foreach (var dataValue in skillsDocumentDataValues)
            {
                apiList.Add(new SkillsDocumentDataValue
                {
                    Title = dataValue.Title,
                    Value = dataValue.Value
                });
            }

            return apiList;
        }

        /// <summary>
        /// Gets the model document data values.
        /// </summary>
        /// <param name="skillsDocumentDataValues">The skills document data values.</param>
        /// <returns></returns>
        internal static List<Models.SkillsDocumentDataValue> GetModelDocumentDataValues(
            this IEnumerable<SkillsDocumentDataValue> skillsDocumentDataValues)
        {
            var modelList = new List<Models.SkillsDocumentDataValue>();

            foreach (var dataValue in skillsDocumentDataValues)
            {
                modelList.Add(new Models.SkillsDocumentDataValue
                {
                    Title = dataValue.Title,
                    Value = dataValue.Value
                });
            }

            return modelList;
        }

        /// <summary>
        /// Gets the API skills document identifiers.
        /// </summary>
        /// <param name="skillsDocumentIdentifiers">The skills document identifiers.</param>
        /// <returns></returns>
        internal static List<SkillsDocumentIdentifier> GetApiSkillsDocumentIdentifiers(
            this IEnumerable<Models.SkillsDocumentIdentifier> skillsDocumentIdentifiers)
        {
            var apiList = new List<SkillsDocumentIdentifier>();

            foreach (var dataValue in skillsDocumentIdentifiers)
            {
                apiList.Add(new SkillsDocumentIdentifier
                {
                    ServiceName = dataValue.ServiceName,
                    Value = dataValue.Value
                });
            }
            return apiList;
        }

        /// <summary>
        /// Gets the model skills document identifiers.
        /// </summary>
        /// <param name="skillsDocumentIdentifiers">The skills document identifiers.</param>
        /// <returns></returns>
        internal static List<Models.SkillsDocumentIdentifier> GetModelSkillsDocumentIdentifiers
            (
            this IEnumerable<SkillsDocumentIdentifier> skillsDocumentIdentifiers)
        {
            var modelList = new List<Models.SkillsDocumentIdentifier>();

            foreach (var dataValue in skillsDocumentIdentifiers)
            {
                modelList.Add(new Models.SkillsDocumentIdentifier
                {
                    ServiceName = dataValue.ServiceName,
                    Value = dataValue.Value
                });
            }
            return modelList;
        }
    }
}
