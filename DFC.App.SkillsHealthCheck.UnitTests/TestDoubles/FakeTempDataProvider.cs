using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DFC.App.SkillsHealthCheck.UnitTests.TestDoubles
{
    public class FakeTempDataProvider : ITempDataProvider
    {
        private IDictionary<string, object> dictionary = new Dictionary<string, object>();

        public IDictionary<string, object> LoadTempData(HttpContext context)
        {
            return this.dictionary;
        }

        public void SaveTempData(HttpContext context, IDictionary<string, object> values)
        {
            this.dictionary = values;
        }
    }
}
