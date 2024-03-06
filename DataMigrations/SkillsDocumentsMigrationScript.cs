using DfE.SkillsCentral.Api.Domain.Models;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

internal class SkillsDocumentMigrationScript
{
    //input filename(s) must match the strings below
    public const string SkillsDocuments = "SkillsDocuments";
    public const string CreatedBy = "MigrationTool";



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


                using (StreamWriter writer = new StreamWriter($"INSERT_{input}.sql"))
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();

                        string jsonDataValues = ConvertXmlIntoJson(fields[5]);

                        string transformedJson = TransformJsonToMatchNewAssessmentDataValuesModel(jsonDataValues);

                        string sortedResultJson = SortJsonToMatchOrderAsDisplayedOnWebsite(transformedJson);

                        string result = ConvertValuesIntoCorrectDataTypes(sortedResultJson);

                        string resultFinal = RemoveTopLevelDataValues(result);








                        Console.WriteLine(parser.LineNumber);
                        writer.WriteLine($"({(fields[0])}, {SurroundWithCastAsDatetime(fields[1])}, {N(CreatedBy)}, {SurroundWithCastAsDatetime(fields[3])}, {N(fields[4])}, {N(resultFinal)}, {N(fields[6])}),");

                    }

            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
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

        public static string ConvertValuesIntoCorrectDataTypes(string input)
    {
        if (input == "NULL")
            return input;
        JObject data = JObject.Parse(input);

        foreach (JProperty property in data["DataValues"].Children())
        {
            JObject section = (JObject)property.Value;

            // Convert "Complete" to boolean
            if (section["Complete"] != null && !string.IsNullOrEmpty(section["Complete"].ToString()))
            {
                section["Complete"] = (bool)section["Complete"];
            }

            // Convert "Answers" to JArray for specific sections
            if (property.Name == "Verbal" || property.Name == "Numeric" || property.Name == "Mechanical" || property.Name == "Spatial" || property.Name == "Abstract")
            {
                if (section["Answers"] != null)
                {
                    string answersString = section["Answers"].ToString();
                    section["Answers"] = !string.IsNullOrEmpty(answersString) ? new JArray(answersString.Split(',')) : new JArray();
                }
                else
                {
                    section["Answers"] = new JArray();
                }
            }
            //// Convert "Answers" to integer array for specific sections
            //else if (property.Name == "Checking" || property.Name == "Motivation" || property.Name == "Personal" || property.Name == "Interest" || property.Name == "SkillAreas")
            //{
            //    if (section["Answers"] != null)
            //    {
            //        string answersString = section["Answers"].ToString();
            //        section["Answers"] = !string.IsNullOrEmpty(answersString) ? new JArray(Array.ConvertAll(answersString.Split(','), int.Parse)) : new JArray();
            //    }
            //    else
            //    {
            //        section["Answers"] = new JArray();
            //    }
            //}

            // Convert "Ease", "Timing", and "Enjoyment" to integers
            if (section["Ease"] != null && !string.IsNullOrEmpty(section["Ease"].ToString()))
                section["Ease"] = int.Parse(section["Ease"].ToString());
            if (section["Timing"] != null && !string.IsNullOrEmpty(section["Timing"].ToString()))
                section["Timing"] = int.Parse(section["Timing"].ToString());
            if (section["Enjoyment"] != null && !string.IsNullOrEmpty(section["Enjoyment"].ToString()))
            {
                section["Enjoyment"] = int.Parse(section["Enjoyment"].ToString());
            }
        }

        return JsonConvert.SerializeObject(data, Formatting.Indented);
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

    public static string TransformJsonToMatchNewAssessmentDataValuesModel(string input)
    {

        if (input == "NULL")
            return input;
        JObject obj = JObject.Parse(input);

        // Create a new JObject to store the modified structure
        JObject newDataValues = new JObject();

        // Iterate over properties of original "DataValues"
        foreach (JProperty property in obj["DataValues"])
        {
            // Exclude properties with specified names
            if (!property.Name.Contains("CandidateFullName") && !property.Name.Contains("Qualification") && !property.Name.EndsWith(".Type"))
            {
                // Split property name to get nested structure
                string[] names = property.Name.Split('.');
                string topLevelName = names[0];
                string subNodeName = names[1];

                // Create sub-object if it doesn't exist
                if (newDataValues[topLevelName] == null)
                {
                    newDataValues[topLevelName] = new JObject();
                }

                // For SkillAreas, if the property name starts with "ExcludedJobFamilies", add it to an array
                if (topLevelName == "SkillAreas" && subNodeName.StartsWith("ExcludedJobFamilies"))
                {
                    // Add non-null and non-empty object values to the array

                    // Create ExcludedJobFamilies array if it doesn't exist
                    if (newDataValues[topLevelName]["ExcludedJobFamilies"] == null)
                    {
                        newDataValues[topLevelName]["ExcludedJobFamilies"] = new JArray();
                    }
                    if (property.Value != null && property.Value.Type != JTokenType.Null && property.Value.Type != JTokenType.Object)
                    {
                        // Add non-null values to the array
                        JObject excludedJobFamily = new JObject();
                        ((JArray)newDataValues[topLevelName]["ExcludedJobFamilies"]).Add(property.Value);
                    }
                }
              
                else
                {
                    // Add or overwrite sub-node
                    newDataValues[topLevelName][subNodeName] = property.Value;
                }
            }
        }



        // Create final result object with modified structure
        JObject result = new JObject();
        result["DataValues"] = newDataValues;

        return JsonConvert.SerializeObject(result/*, Formatting.Indented*/);
    }

    public static string SortJsonToMatchOrderAsDisplayedOnWebsite(string input)
    {
        if (input == "NULL")
            return input;
        // Parse JSON string to JObject
        JObject resultObj = JObject.Parse(input);

        // Define the desired order of categories
        string[] order = {
                            "SkillAreas",
                            "Interest",
                            "Personal",
                            "Motivation",
                            "Numeric",
                            "Verbal",
                            "Checking",
                            "Mechanical",
                            "Spatial",
                            "Abstract"
                        };
        // Sort the properties based on the desired order
        var sortedProperties = resultObj["DataValues"]
            .OrderBy(p => Array.IndexOf(order, ((JProperty)p).Name));

        // Create a new JObject with sorted properties
        JObject newSortedDataValues = new JObject(sortedProperties.Cast<JProperty>());

        // Create final result object with modified structure
        JObject sortedResult = new JObject();
        sortedResult["DataValues"] = newSortedDataValues;

        // Serialize JObject to JSON string
        return JsonConvert.SerializeObject(sortedResult/*, Formatting.Indented*/);
    }
}


