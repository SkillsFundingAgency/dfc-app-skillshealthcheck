﻿using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

internal class SkillsDocumentMigrationScript
{
    //input filename(s) must match the strings below
    public const string SkillsDocuments = "SkillsDocuments";



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

                        writer.WriteLine($"({(fields[0])}, {N(fields[1])}, {N(fields[2])}, {N(fields[3])}, {N(fields[4])}, {N(sortedResultJson)}, {N(fields[6])}),");

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
    public static string N(string input)
    {
        if (string.IsNullOrEmpty(input))
        { return "NULL"; }
        else
        { return $"'{input}'"; }
    }

    public static string ConvertXmlIntoJson(string input)
    {
        XmlDocument xmlDataValues = new XmlDocument();
        xmlDataValues.LoadXml(input);
        return JsonConvert.SerializeXmlNode(xmlDataValues, Formatting.Indented);
    }

    public static string TransformJsonToMatchNewAssessmentDataValuesModel(string input)
    {
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
                        excludedJobFamily["ExcludedJobFamily"] = property.Value;
                        ((JArray)newDataValues[topLevelName]["ExcludedJobFamilies"]).Add(excludedJobFamily);
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

        return JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
    }

    public static string SortJsonToMatchOrderAsDisplayedOnWebsite(string input)
    {
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
        return JsonConvert.SerializeObject(sortedResult, Formatting.Indented);
    }
}


