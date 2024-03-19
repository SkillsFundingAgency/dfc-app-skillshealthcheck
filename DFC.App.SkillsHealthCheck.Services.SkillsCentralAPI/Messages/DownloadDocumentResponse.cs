namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages
{
    public class DownloadDocumentResponse : GenericResponse
    {
        public byte[] DocumentBytes { get; set; }

        public string DocumentName { get; set; }
    }
}
