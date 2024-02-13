
namespace DFC.SkillsCentral.Api.Infrastructure.Queries
{
    public static class SkillsDocumentsQueries
    {
        public static string GetSkillsDocumentByIdAsync => "SELECT * FROM [SkillsDocuments] (NOLOCK) WHERE [SkillsDocumentId] = @SkillsDocumentId";

        public static string GetSkillsDocumentByReferenceCodeAsync => "SELECT * FROM [SkillsDocuments] (NOLOCK) WHERE [ReferenceCode] = @ReferenceCode";

        public static string InsertSkillsDocumentAsync => 
            "INSERT INTO [SkillsDocuments] (SkillsDocumentTypeSysId, SkillsDocumentTitle, CreatedAtDate, CreatedBy, ExpiresTimespan, ExpiresType, XMLValueKeys, LastAccess, ReferenceCode)" +
            "VALUES (@SkillsDocumentTypeSysId, @SkillsDocumentTitle, @CreatedAtDate, @CreatedBy, @ExpiresTimespan, @ExpiresType, @XMLValueKeys, @LastAccess, @ReferenceCode)";

        public static string UpdateSkillsDocument =>
            "UPDATE [SkillsDocuments] SET SkillsDocumentTitle = @SkillsDocumentTitle, UpdatedAtDate = @UpdatedAtDate, UpdatedBy = @UpdatedBy, XMLValueKeys = @XMLValueKeys, " +
            "ExpiresTimespan = @ExpiresTimespan, ExpiresType = @ExpiresType, LastAccess	= @LastAccess WHERE [SkillsDocumentId] = @SkillsDocumentId";

    }
}

