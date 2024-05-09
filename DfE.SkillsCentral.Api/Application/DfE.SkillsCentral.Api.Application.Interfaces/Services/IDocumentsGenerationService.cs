namespace DfE.SkillsCentral.Api.Application.Services.Services
{
    public interface IDocumentsGenerationService
    {

        Task<byte[]> GenerateWordDoc(int documentId);
        Task<byte[]> GeneratePDF(int documentId);

    }
}