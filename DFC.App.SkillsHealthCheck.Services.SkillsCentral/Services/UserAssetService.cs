using System;
//using System.Collections.Generic;
//using System.Dynamic;
//using System.IO;
//using System.Linq;
using System.ServiceModel;
using System.Threading;
//using System.Xml.Serialization;
using SkillsDocumentService;
//using SFA.Careers.Identity.Interfaces;
//using SFA.Careers.Identity;
//using SFA.Careers.Citizen.Data.CitizenModel;
//using System.Threading.Tasks;
//using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Extensions;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
//using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
//using SkillsDocument = SkillsDocumentService.SkillsDocument;
//using SkillsDocumentDataValue = SkillsDocumentService.SkillsDocumentDataValue;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Services
{
    public sealed class UserAssetService : IUserAssetService
    {

        private object SyncRoot
        {
            get
            {
                return m_syncRoot.Value;
            }
        }
        private Lazy<object> m_syncRoot = new Lazy<object>(() => new object());

        private SkillsCentralServiceClient ServiceClient
        {
            get
            {
                lock (this.SyncRoot)
                {
                    if (m_serviceClient.Value == null || m_serviceClient.Value.State != CommunicationState.Opened)
                    {
                        m_serviceClient = null;
                        Interlocked.CompareExchange<Lazy<SkillsCentralServiceClient>>(
                                ref m_serviceClient,
                                new Lazy<SkillsCentralServiceClient>(() => new SkillsCentralServiceClient()),
                                null
                            );
                    }
                    return m_serviceClient.Value;
                }
            }
        }
        private Lazy<SkillsCentralServiceClient> m_serviceClient = new Lazy<SkillsCentralServiceClient>(() => new SkillsCentralServiceClient());

        public UserAssetService()
        {
        }

        //private IEnumerable<SkillsDocument> GetUserRecordsPrivate(String docType, String userId)
        //{
        //    if (String.IsNullOrWhiteSpace(docType))
        //    {
        //        throw new ArgumentNullException(nameof(docType));
        //    }
        //    if (String.IsNullOrWhiteSpace(userId))
        //    {
        //        throw new ArgumentNullException(nameof(userId));
        //    }

        //    IEnumerable<SkillsDocument> items = new List<SkillsDocument>();
        //    lock (this.SyncRoot)
        //    {
        //        DocumentTypeCode selectedDocumentType = (DocumentTypeCode)Enum.Parse(typeof(DocumentTypeCode), docType);
        //        foreach (String typeName in selectedDocumentType.GetDocumentType().DocumentTypes)
        //        {
        //            items = items.Union(this.ServiceClient.FindDocumentsByServiceNameKeyValueAndDocTypeAsync(typeName, ServiceCommon.SERVICE_NAME, userId, docType.Equals(DocumentTypeCode.ACAP.ToString(), StringComparison.OrdinalIgnoreCase) == true).Result);
        //        }
        //    }

        //    if (items != null && items.Count() > 0)
        //    {
        //        items = items.Where(x => x.DeletedAt.HasValue == false);
        //    }

        //    return items;
        //}

        //public void UpdateUsage(Guid citizenId, SFA.Careers.Citizen.Data.AccountModel.UsageType usageType)
        //{
        //    if (Guid.Empty.Equals(citizenId) == true)
        //    {
        //        throw new ArgumentNullException(nameof(citizenId));
        //    }
        //    ICitizen citizen = new Citizen(citizenId, CitizenAccessContext.Self);
        //    citizen.UpdateUsageAsync(usageType);
        //}
        //public void UpdateUsage(Int64 userId, DateTime? current, String activity)
        //{
        //    if (userId <= 0)
        //    {
        //        throw new ArgumentOutOfRangeException(nameof(userId));
        //    }
        //    if (current.HasValue == false)
        //    {
        //        throw new ArgumentNullException(nameof(current));
        //    }
        //    if (String.IsNullOrWhiteSpace(activity))
        //    {
        //        throw new ArgumentNullException(nameof(activity));
        //    }
        //    UsageLog.UpdateUsagePrivate(userId, current, activity);
        //}

        //private void DeletePrivate(Int64 documentId, String requestedBy)
        //{
        //    lock (this.SyncRoot)
        //    {
        //        this.ServiceClient.LogicalDeleteDocumentWithAuditAsync(documentId, requestedBy).RunSynchronously();
        //    }
        //}

        private FormatDocumentResponse RequestDownloadPrivate(long documentId, string formatter, string requestedBy)
        {
            //TODO: not sure about these locks and can't this just be asynchronous? Needs looking at
            lock (this.SyncRoot)
            {
                return this.ServiceClient.FormatDocumentMakeRequestAsync(documentId, formatter, requestedBy).Result;
            }
        }

        private FormatDocumentResponse QueryDownloadStatusPrivate(long documentId, string formatter)
        {
            lock (this.SyncRoot)
            {
                return this.ServiceClient.FormatDocumentPollStatusAsync(documentId, formatter).Result;
            }
        }

        private byte[] DownloadPrivate(long documentId, string formatter)
        {
            lock (this.SyncRoot)
            {
                return this.ServiceClient.FormatDocumentGetPayloadAsync(documentId, formatter).Result;
            }
        }

        //private SkillsDocument GetAdviserCreatedActionPlanPrivate(Int64 documentId)
        //{
        //    lock (this.SyncRoot)
        //    {
        //        return this.ServiceClient.ReadDocumentAsync(documentId).Result;
        //    }
        //}

        //private void UpdateAdviserCreatedActionPlanPrivate(Int64 documentId, IDictionary<byte, String> actionStatuses)
        //{
        //    lock (this.SyncRoot)
        //    {
        //        SkillsDocument current = this.ServiceClient.ReadDocumentAsync(documentId).Result;
        //        foreach (SkillsDocumentDataValue value in current.DataValues)
        //        {
        //            if (value.Title.Equals("ap.advised.Action.Xml", StringComparison.OrdinalIgnoreCase) == true)
        //            {
        //                ActionItemsDictionary actions = null;
        //                XmlSerializer serialiser = new XmlSerializer(typeof(ActionItemsDictionary));
        //                using (var readerStream = new StringReader(value.Value))
        //                {
        //                    actions = serialiser.Deserialize(readerStream) as ActionItemsDictionary;
        //                }
        //                if (actions != null && actions.item != null && actions.item.Length > 0)
        //                {
        //                    foreach (ActionItemsDictionaryEntry dictionaryEntry in actions.item)
        //                    {
        //                        if (actionStatuses.ContainsKey(dictionaryEntry.key.@long))
        //                        {
        //                            dictionaryEntry.value.Action.ActionPlanActionStatus = actionStatuses[dictionaryEntry.key.@long];
        //                        }
        //                    }
        //                }
        //                //XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
        //                //namespaces.Add("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
        //                //namespaces.Add("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
        //                var writerStream = new StringWriter();
        //                //serialiser.Serialize(writerStream, actions, namespaces);
        //                serialiser.Serialize(writerStream, actions);
        //                value.Value = writerStream.ToString();
        //                break;
        //            }
        //        }
        //        this.ServiceClient.UpdateSkillsDocumentDataValuesAsync(documentId, current).RunSynchronously();
        //    }
        //}

        //private void UpdateAdviserCreatedActionPlanFeedbackPrivate(Int64 documentId, String item)
        //{
        //    lock (this.SyncRoot)
        //    {
        //        SkillsDocument current = this.ServiceClient.ReadDocumentAsync(documentId).Result;
        //        if (current != null && current.DataValues != null && current.DataValues.Count > 0)
        //        {
        //            if (current.DataValues.Any(x => x.Title.Equals("ap.advised.SatisfactionIndicator", StringComparison.OrdinalIgnoreCase) == true))
        //            {
        //                foreach (SkillsDocumentDataValue value in current.DataValues)
        //                {
        //                    if (value.Title.Equals("ap.advised.SatisfactionIndicator", StringComparison.OrdinalIgnoreCase) == true)
        //                    {
        //                        value.Value = item;
        //                        break;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                current.DataValues.Add(new SkillsDocumentDataValue() { Title = "ap.advised.SatisfactionIndicator", Value = item });
        //            }
        //            if (current.DataValues.Any(x => x.Title.Equals("ap.advised.SatisfactionIndicatorDate", StringComparison.OrdinalIgnoreCase) == true))
        //            {
        //                foreach (SkillsDocumentDataValue value in current.DataValues)
        //                {
        //                    if (value.Title.Equals("ap.advised.SatisfactionIndicatorDate", StringComparison.OrdinalIgnoreCase) == true)
        //                    {
        //                        value.Value = DateTime.UtcNow.ToString("s");
        //                        break;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                current.DataValues.Add(new SkillsDocumentDataValue() { Title = "ap.advised.SatisfactionIndicatorDate", Value = DateTime.UtcNow.ToString("s") });
        //            }
        //            this.ServiceClient.UpdateSkillsDocumentDataValuesAsync(documentId, current).RunSynchronously();
        //        }
        //    }
        //}

        //public Int64 GetLifelongLearnerAccountId(String accessToken, Guid citizenId)
        //{
        //    ICitizen citizen = null;
        //    citizen = new Citizen(citizenId, CitizenAccessContext.Self);
        //    return Int64.Parse(Task.Run(() => citizen.GetLifelongLearnerAccountIdAsync()).Result.ToString());
        //}

        //public IEnumerable<dynamic> GetUserRecords(String docType, String userId)
        //{
        //    if (String.IsNullOrWhiteSpace(userId) == true)
        //    {
        //        throw new ArgumentNullException(nameof(userId));
        //    }

        //    IEnumerable<SkillsDocument> items = null;

        //    if (((DocumentTypeCode)Enum.Parse(typeof(DocumentTypeCode), docType)).Equals(DocumentTypeCode.CS) == false)
        //    {
        //        items = this.GetUserRecordsPrivate(docType, userId);
        //    }

        //    if (items != null && items.Count() > 0)
        //    {
        //        return
        //             items
        //             .Select(x => new
        //             {
        //                 Id = x.Identifiers.ExtractId(),
        //                 //NewId = new Guid(x.Identifiers.SingleOrDefault(y => y.GetType() == typeof(Guid)).ToString()),
        //                 x.DocumentId,
        //                 DateCreated = (docType.Equals(DocumentTypeCode.ACAP.ToString(), StringComparison.OrdinalIgnoreCase) == true) ? DateTime.Parse(x.DataValues.SingleOrDefault(y => y.Title.Equals("ap.advised.SessionDate", StringComparison.OrdinalIgnoreCase)).Value) : x.CreatedAt,
        //                 Title = x.SkillsDocumentTitle,
        //                 CreatedBy = (docType.Equals(DocumentTypeCode.ACAP.ToString(), StringComparison.OrdinalIgnoreCase) == true) ? x.DataValues.SingleOrDefault(y => y.Title.Equals("ap.advised.AdviserName", StringComparison.OrdinalIgnoreCase)).Value : x.CreatedBy,
        //                 DocumentType = x.SkillsDocumentType.GetDocumentType()
        //             }.
        //             ToExpando()).
        //             ToList();
        //    }

        //    return null;
        //}

        public string RequestDownload(long documentId, string formatter, string requestedBy)
        {
            if (documentId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(documentId));
            }

            if (string.IsNullOrWhiteSpace(formatter))
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            if (string.IsNullOrWhiteSpace(requestedBy))
            {
                throw new ArgumentNullException(nameof(requestedBy));
            }

            return this.RequestDownloadPrivate(documentId, formatter, requestedBy).Status.ToString();
        }

        public string QueryDownloadStatus(long documentId, string formatter)
        {
            if (documentId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(documentId));
            }

            if (string.IsNullOrWhiteSpace(formatter))
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            return this.QueryDownloadStatusPrivate(documentId, formatter).Status.ToString();
        }

        public byte[] Download(long documentId, string formatter)
        {
            if (documentId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(documentId));
            }

            if (string.IsNullOrWhiteSpace(formatter))
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            return this.DownloadPrivate(documentId, formatter);
        }

        //public void Delete(Int64 documentId, String requestedBy)
        //{
        //    if (documentId <= 0)
        //    {
        //        throw new ArgumentOutOfRangeException(nameof(documentId));
        //    }

        //    if (String.IsNullOrWhiteSpace(requestedBy) == true)
        //    {
        //        throw new ArgumentNullException(nameof(requestedBy));
        //    }

        //    this.DeletePrivate(documentId, requestedBy);
        //}

        //public dynamic GetAdviserCreatedActionPlan(Int64 documentId)
        //{
        //    dynamic result = null;
        //    if (documentId <= 0)
        //    {
        //        throw new ArgumentOutOfRangeException(nameof(documentId));
        //    }
        //    SkillsDocument current = this.GetAdviserCreatedActionPlanPrivate(documentId);
        //    if (current != null)
        //    {
        //        result = new ExpandoObject();
        //        if (current.DataValues != null && current.DataValues.Count > 0)
        //        {
        //            current.DataValues.ForEach(x =>
        //            {
        //                if (x.Title.Equals("ap.advised.Action.Xml", StringComparison.OrdinalIgnoreCase) == false)
        //                {
        //                    ((IDictionary<String, Object>)result).Add(x.Title.Replace("ap.advised.", ""), x.Value);
        //                }
        //                else
        //                {
        //                    using (var reader = new StringReader(x.Value))
        //                    {
        //                        XmlSerializer serialiser = new XmlSerializer(typeof(ActionItemsDictionary));
        //                        ((IDictionary<String, Object>)result).Add("Actions", serialiser.Deserialize(reader));
        //                    }
        //                }
        //            });
        //        }
        //        result.CreatedAt = DateTime.Parse(current.DataValues.SingleOrDefault(y => y.Title.Equals("ap.advised.SessionDate", StringComparison.OrdinalIgnoreCase)).Value);
        //        result.CreatedBy = current.DataValues.SingleOrDefault(y => y.Title.Equals("ap.advised.AdviserName", StringComparison.OrdinalIgnoreCase)).Value;
        //        result.DocumentType = current.SkillsDocumentType;
        //        result.UpdatedAt = current.UpdatedAt;
        //        if (current.Identifiers != null && current.Identifiers.Count > 0 && current.Identifiers.Any(x => x.ServiceName.Equals(ServiceCommon.SERVICE_NAME)) == true)
        //        {
        //            result.UserId = current.Identifiers.SingleOrDefault(x => x.ServiceName.Equals(ServiceCommon.SERVICE_NAME)).Value;
        //        }
        //    }
        //    return result;
        //}

        //public void UpdateAdviserCreatedActionPlan(Int64 documentId, IDictionary<byte, String> actionStatuses)
        //{
        //    if (documentId <= 0)
        //    {
        //        throw new ArgumentOutOfRangeException(nameof(documentId));
        //    }
        //    this.UpdateAdviserCreatedActionPlanPrivate(documentId, actionStatuses);
        //}
        //public void UpdateAdviserCreatedActionPlanFeedback(Int64 documentId, String feedback)
        //{
        //    if (documentId <= 0)
        //    {
        //        throw new ArgumentOutOfRangeException(nameof(documentId));
        //    }
        //    if (String.IsNullOrWhiteSpace(feedback))
        //    {
        //        throw new ArgumentNullException(nameof(feedback));
        //    }
        //    if (feedback.Equals("Yes", StringComparison.OrdinalIgnoreCase) == false && feedback.Equals("No", StringComparison.OrdinalIgnoreCase) == false)
        //    {
        //        throw new ArgumentException("invalid feedback value supplied");
        //    }
        //    this.UpdateAdviserCreatedActionPlanFeedbackPrivate(documentId, feedback);
        //}

        //public IEnumerable<DocumentType> GetDocumentTypes()
        //{
        //    return ServiceCommon.GetDocumentTypes();
        //}

        //public DocumentFormatter GetDocumentFormatter(DocumentTypeCode skillsDocumentType, string targetFormat)
        //{
        //    return ServiceCommon.GetDocumentFormatter(skillsDocumentType, targetFormat);
        //}

        //public bool CheckAccess(long documentId, long skillsAccountId)
        //{
        //    var doc = this.ServiceClient.ReadDocumentAsync(documentId).Result;
        //    return doc.Identifiers.Any(id => id.Value == skillsAccountId.ToString());
        //}
    }
}
