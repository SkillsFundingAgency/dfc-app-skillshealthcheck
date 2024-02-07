using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC.SkillsCentral.Api.Domain.Models
{
    public class SkillsDocument
    {
        public int SkillsDocumentId { get; set; }

        public string SkillsDocumentTitle { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime DeletedAt { get; set; }

        public string DeletedBy { get; set; }

        public int ExpiresTimespan { get; set; }

        public int ExpiresType { get; set; }

        public string XMLValueKeys { get; set; }

        public DateTime LastAccessed { get; set; }




    }
}
