using System.Collections.Generic;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages
{
    public class GetListTypeFieldsResponse : GenericResponse
    {
        public List<string> TypeFields { get; set; }
    }
}
