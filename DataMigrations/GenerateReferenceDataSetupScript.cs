using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class GenerateReferenceDataSetupScript
{
    //input filename(s) must match the strings below
    public const string assessments = "assessments";
    public const string questionsanswers = "questionsanswers";

    public static int globalNewAnswerId = 0;
    public static List<string> globalHistoricQuestionIds = new List<string>();

    public static void Main(string[] args)
    {   
        CreateSQLScript(assessments);
        CreateSQLScript(questionsanswers);
    }

    //replaces null, empty, or similar strings consistently: not to be used on integer values
    //method name N short for NullCheck, improves readability when called on strings
    public static string N(string input)
    {
        if (string.IsNullOrEmpty(input) || input.ToUpper().Contains("NULL"))
        { return "NULL"; }
        else
        { return $"'{input}'"; }
    }

    //maps historic assessment IDs to the new values 1 to 10
    public static string MapAssessmentId(string input)
    {
        string[] historicAssessmentIds = ["20", "4", "17", "8", "11", "22", "3", "7", "21", "1"];
        int newAssessmentId = 1 + Array.IndexOf(historicAssessmentIds, input);
        return newAssessmentId.ToString();
    }

    //generates new sequential answer ID
    public static string GenerateNewAnswerId()
    {
        globalNewAnswerId++;
        return globalNewAnswerId.ToString();
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

                    switch (input)
                    {
                    case assessments:
                        using (StreamWriter writer = new StreamWriter($"INSERT_{input}.sql"))
                        while (!parser.EndOfData)
                        {
                                int id = 1;
                                string[] fields = parser.ReadFields();
                            string[] escapedStrings = fields.Select(str => str.Replace("'", "''")).ToArray();
                            {
                                    writer.WriteLine($"({MapAssessmentId(escapedStrings[0])}, {N(escapedStrings[1])}, {N(escapedStrings[4])}, {N(escapedStrings[5])}, {N(escapedStrings[6])}),");
                            }
                        }
                        break;

                    case questionsanswers:
                       
                        //create two streamwriters to support writing data found in the single questions/answers (and answer headings) file to two separate files 
                        using (StreamWriter questionWriter = new StreamWriter($"INSERT_questions.sql"))
                        using (StreamWriter answerWriter = new StreamWriter($"INSERT_answers.sql"))

                        while (!parser.EndOfData)
                        {
                            string[] fields = parser.ReadFields();
                            string[] escapedStrings = fields.Select(str => str.Replace("'", "''")).ToArray();
                            {
                                //expected locations of question and answer text values
                                int questionTextIndex = 5;
                                int answerTextIndex = 12;

                                //if assessment type is in the list of 'strange' assessment types, move data into correct/expeted columns
                                //these assessment types (historically) have question and answer data in unexpected columns/tables
                                string[] strangeAssessmentTypes = ["4", "8", "17"];
                                if (strangeAssessmentTypes.Contains(escapedStrings[1]))
                                {
                                    questionTextIndex = 12;     //i.e. read from answers table
                                    answerTextIndex = 15;       //i.e. read from answerheadings table
                                }

                                if (!globalHistoricQuestionIds.Contains(escapedStrings[10])) 
                                { questionWriter.WriteLine($"({escapedStrings[10]}, {MapAssessmentId(escapedStrings[1])}, {escapedStrings[4]}, {N(escapedStrings[3])}, {N(escapedStrings[questionTextIndex])}, {N(escapedStrings[6])}, {N(escapedStrings[7])}, {N(escapedStrings[8])}),"); }
                                globalHistoricQuestionIds.Add(escapedStrings[10]);

                                answerWriter.WriteLine($"({GenerateNewAnswerId()}, {escapedStrings[10]}, {N(escapedStrings[11])}, {999}, {N(escapedStrings[answerTextIndex])}, NULL, NULL, {N(escapedStrings[13].ToString())}),");

                                //ImageTitle and ImageCaption are always null as the fields do not (historically) exist on the answers table, they are added for future accessibility
                                //Placeholder 999 to be replaced once values are determined
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Default case reached, input filename(s) do not match expected range or values - reference data generation cannot proceed.");
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        } 
    }




}
