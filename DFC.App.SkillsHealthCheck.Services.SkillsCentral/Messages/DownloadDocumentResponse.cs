using System;
using System.Collections.Generic;
using System.Text;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages
{
    public class DownloadDocumentResponse : GenericResponse
    {
        public byte[] DocumentBytes { get; set; }
    }
}
