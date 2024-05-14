using DfE.SkillsCentral.Api.Domain.Models;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Text.Json.Nodes;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

internal class SkillsDocumentMigrationScript
{
    //input filename(s) must match the strings below
    public const string SkillsDocuments = "SkillsDocuments";
    public const string CreatedBy = "MigrationTool";
    public const string scriptStart = "SET IDENTITY_INSERT SkillsDocuments ON;\r\n \r\n;WITH SkillsDocumentsCte AS (\r\n\tSELECT * FROM (VALUES";
    public const string scriptEnd = ") AS SkillsDocumentsTemp(Id, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy, DataValueKeys, ReferenceCode ))\r\n \r\nMERGE SkillsDocuments AS target\r\nUSING (\r\n\tSELECT *\r\n\tFROM SkillsDocumentsCte\r\n) AS source\r\nON target.Id = source.Id\r\nWHEN MATCHED\r\n\tTHEN UPDATE SET\r\n\t\ttarget.CreatedAt = source.CreatedAt,\r\n\t\ttarget.CreatedBy = source.CreatedBy,\r\n\t\ttarget.UpdatedAt = source.UpdatedAt,\r\n\t\ttarget.UpdatedBy = source.UpdatedBy,\r\n\t\ttarget.DataValueKeys = source.DataValueKeys,\r\n\t\ttarget.ReferenceCode = source.ReferenceCode\r\nWHEN NOT MATCHED\r\n\tTHEN INSERT (Id, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy, DataValueKeys, ReferenceCode ) VALUES (\r\n\t\tsource.Id,\r\n\t\tsource.CreatedAt,\r\n\t\tsource.CreatedBy,\r\n\t\tsource.UpdatedAt,\r\n\t\tsource.UpdatedBy,\r\n\t\tsource.DataValueKeys,\r\n\t\tsource.ReferenceCode\r\n\t);\r\n \r\nSET IDENTITY_INSERT SkillsDocuments OFF;";
    public const int counterMax = 10000;



    public static void Main(string[] args)
    {
        CreateSQLScript(SkillsDocuments);
    }

    //generates sql snippets which are to be inserted into the post deployment script
    public static void CreateSQLScript(string input)
    {
        try
        {
            //read .csv file from {repo location}\dfc-app-skillshealthcheck\DataMigrations\bin\Debug\net8.0\ and write an insert script into file
            string path = $"{System.Environment.CurrentDirectory}\\{input}.csv";
            using (TextFieldParser parser = new TextFieldParser(path))
            {
                parser.SetDelimiters(new string[] { "," });

                //skips first row/line of column names
                parser.ReadLine();


                int batchCounter = 0;
                while (!parser.EndOfData)
                {
                    batchCounter++;
                    using (StreamWriter writer = new StreamWriter($"INSERT_{input}_{batchCounter}.sql"))
                    {
                        for (var index = 0; index < 5; index++)
                        {
                            int counter = 0;

                            writer.WriteLine(scriptStart);
                            while (counter < counterMax && !parser.EndOfData)
                            {
                                counter++;

                                string[] fields = parser.ReadFields();

                                string jsonDataValues = ConvertXmlIntoJson(fields[5]);

                                string modifiedJson = RemovedRedundantSections(jsonDataValues);

                                var jsonWithoutTopLevelDataValues = RemoveTopLevelDataValues(modifiedJson);

                                var result = RemoveMinusOnes(jsonWithoutTopLevelDataValues);

                                Console.WriteLine(parser.LineNumber);

                                WriteToFile(parser, counter, writer, fields, result);
                            }
                            writer.WriteLine(scriptEnd);
                            writer.WriteLine();
                        }
                    }
                }

            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static void WriteToFile(TextFieldParser parser, int counter, StreamWriter writer, string[] fields, string result)
    {
        string writeString = $"({(fields[0])}, {SurroundWithCastAsDatetime(fields[1])}, {N(CreatedBy)}, {SurroundWithCastAsDatetime(fields[3])}, {N(fields[4])}, {N(result)}, {N(fields[6])})";
        if (counter == counterMax || parser.EndOfData)
        {
            writer.WriteLine(writeString);
        }
        else { writer.WriteLine(writeString + ","); }
    }

    public static string RemoveMinusOnes(string input)
    {
        if (input == "NULL")
            return input;
        JObject obj = JObject.Parse(input);

        foreach (var property in obj.Properties())
        {
            if (property.Name != "SkillAreas.Answers")
            {
                // Exclude SkillAreas.Answers from modification
                var valueArray = property.Value.ToString().Split(',');
                for (int i = 0; i < valueArray.Length; i++)
                {
                    if (valueArray[i] == "-1")
                    {
                        // Replace "-1" with an empty string
                        valueArray[i] = "";
                    }
                }
                // Remove consecutive commas
                var newValue = string.Join(",", valueArray).Replace(",,", ",");
                // Trim leading and trailing commas
                newValue = newValue.Trim(',');
                obj[property.Name] = newValue;
            }
        }

        return obj.ToString();
    }
    private static string RemovedRedundantSections(string input)
    {
        if (input == "NULL")
            return input;
        JObject obj = JObject.Parse(input);
        // Get the "DataValues" property
        JObject dataValues = (JObject)obj["DataValues"];

        var propertiesToRemove = dataValues.Properties()
            .Where(p => p.Value.Type == JTokenType.Null)
            .ToList();

        // Remove properties with null values
        foreach (var property in propertiesToRemove)
        {
            property.Value = "";
        }

        // Remove properties with names ending with ".Type"
        foreach (JProperty property in dataValues.Properties().ToList())
        {
            if (property.Name.EndsWith(".Type") || property.Name == "CandidateFullName" ||  property.Name == "Qualification.Level")
            {
                property.Remove();
            }

            if (property.Name=="Interest.Answers" || property.Name=="Interest.Complete")
            {

                // Add a new property with the name replaced by "Interests"
                obj[property.Name.Replace("Interest", "Interests", StringComparison.OrdinalIgnoreCase)] = property.Value;
                // Remove the original property
                obj.Remove(property.Name);
            }

            if (property.Name=="Numeric.Answers" || property.Name=="Numeric.Complete"|| property.Name=="Numeric.Ease"|| property.Name=="Numeric.Timing")
            {

                // Add a new property with the name replaced by "Interests"
                obj[property.Name.Replace("Numeric", "Numerical", StringComparison.OrdinalIgnoreCase)] = property.Value;
                // Remove the original property
                obj.Remove(property.Name);
            }

            if (property.Name=="Interest.Answers" || property.Name=="Interest.Complete" ||property.Name=="Numeric.Answers" || property.Name=="Numeric.Complete"|| property.Name=="Numeric.Ease"|| property.Name=="Numeric.Timing")
            {
                property.Remove();

            }

        }

        return obj.ToString();
    }

    //replaces null, empty, or similar strings consistently: not to be used on integer values
    //method name N short for NullCheck, improves readability when called on strings

    public static string SurroundWithCastAsDatetime(string input)
    {
        if (string.IsNullOrEmpty(input) || input == "NULL")
            return "NULL";
        else
            return $"CAST(N'{input}' AS DateTime)";
    }



    public static string N(string input)
    {
        if (string.IsNullOrEmpty(input)|| input == "NULL")
        { return "NULL"; }
        else
        { return $"'{input}'"; }
    }

    public static string RemoveTopLevelDataValues(string input)
    {
        if (input == "NULL")
            return input;
        JObject jsonObject = JObject.Parse(input);
        JObject dataValues = (JObject)jsonObject["DataValues"];

        // Remove the "DataValues" key from the parent object
        jsonObject.Remove("DataValues");

        // Add the contents of "DataValues" directly to the parent object
        foreach (var property in dataValues.Properties())
        {
            jsonObject.Add(property.Name, property.Value);
        }

        // Convert the result back to JSON
        return jsonObject.ToString();

    }



    public static string ConvertXmlIntoJson(string input)
    {
        try
        {
            if (input == "NULL")
                return input;
            XmlDocument xmlDataValues = new XmlDocument();
            xmlDataValues.LoadXml(input);
            return JsonConvert.SerializeXmlNode(xmlDataValues/*, Formatting.Indented*/);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

}


