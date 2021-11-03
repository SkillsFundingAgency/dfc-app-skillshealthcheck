using System;
using System.Collections.Generic;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces
{
    public interface IUserAssetService
    {
        String RequestDownload(Int64 documentId, String formatter, String requestedBy);
        String QueryDownloadStatus(Int64 documentId, String formatter);
        Byte[] Download(Int64 documentId, String formatter);

        //Int64 GetLifelongLearnerAccountId(String accessToken, Guid citizenId);
        //IEnumerable<dynamic> GetUserRecords(String docType, String userId);
        //void UpdateUsage(Int64 userId, DateTime? current, String activity);
        //void Delete(Int64 documentId, String requestedBy);
        //void UpdateUsage(Guid citizenId, SFA.Careers.Citizen.Data.AccountModel.UsageType usageType);
        //[Obsolete("Please use UpdateUsage(Guid citizenId, SFA.Careers.Citizen.Data.AccountModel.UsageType usageType)")]
        //dynamic GetAdviserCreatedActionPlan(Int64 id);
        //void UpdateAdviserCreatedActionPlan(Int64 documentId, IDictionary<byte, String> actionStatuses);
        //void UpdateAdviserCreatedActionPlanFeedback(Int64 documentId, String value);

        //bool CheckAccess(long documentId, long skillsAccountId);
        //IEnumerable<DocumentType> GetDocumentTypes();
       // DocumentFormatter GetDocumentFormatter(DocumentTypeCode skillsDocumentType, String targetFormat);
    }
}
