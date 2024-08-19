using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReferenceDataMigrations
{
    public static class JobFamiliesInterestAreasDataSetupScript
    {
        public const string HeaderSql = @"
DECLARE @JobFamiliesInterstAreas TABLE (
    JobFamilyId INT,
    Name NVARCHAR(255)
)

INSERT INTO @JobFamiliesInterstAreas (JobFamilyId , Name) VALUES
";
        public const string FooterSql = @"
MERGE JobFamiliesInterestAreas AS target using (SELECT * FROM @JobFamiliesInterstAreas) AS source
ON source.JobFamilyId = target.JobFamilyId AND source.Name = target.Name
WHEN NOT matched THEN
    INSERT (JobFamilyId, Name) VALUES (source.JobFamilyId, source.Name)
WHEN NOT matched BY source THEN
    DELETE;";

        public static void Execute(string sourceXml, string targetSql)
        {
            var doc = XDocument.Load(sourceXml);

            var jobFamilyDict = doc.Descendants("JobFamily")
                .ToDictionary(
                    jf => int.Parse(jf.Element("JobFamilyId")!.Value),
                    jf => jf.Element("InterestAreas")!
                        .Elements("Value")
                        .Select(va => va.Value)
                        .ToList()
                );

            using var writer = new StreamWriter(targetSql);
            writer.Write(HeaderSql);

            foreach (var kvp in jobFamilyDict)
            {
                if (!kvp.Value.Any()) continue;
                foreach (var interestArea in kvp.Value)
                {
                    writer.WriteLine($"({kvp.Key}, '{interestArea}'),");
                }

            }

            writer.Write(FooterSql);



        }
    }
}
