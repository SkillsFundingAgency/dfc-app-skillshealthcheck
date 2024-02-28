
namespace DFC.SkillsCentral.Api.Infrastructure.Queries
{
    public static class SkillsDocumentsQueries
    {
        public static string GetSkillsDocumentByIdAsync => "SELECT * FROM [SkillsDocuments] WHERE Id = @Id";

        public static string GetSkillsDocumentByReferenceCodeAsync => "SELECT * FROM [SkillsDocuments] WHERE [ReferenceCode] = @ReferenceCode";

        public static string InsertSkillsDocumentAsync =>
            "INSERT INTO [SkillsDocuments] (CreatedAt, CreatedBy, DataValueKeys, ReferenceCode)" +
            "VALUES (@CreatedAt, @CreatedBy, @DataValueKeys, @ReferenceCode )";

        public static string UpdateSkillsDocument =>
            "UPDATE [SkillsDocuments] SET UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy, DataValueKeys = @DataValueKeys WHERE Id = @Id";

    }
}

