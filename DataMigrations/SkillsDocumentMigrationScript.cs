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
    public static int globalNewQuestionId = 4;
    public static int globalNewAnswerValue = 0;
    public static List<string> globalHistoricQuestionIds = new List<string>();
    public static List<string> globalHistoricAnswerText = new List<string>();


    public static void Main(string[] args)
    {   
        CreateSQLScript(assessments);
        CreateSQLScript(questionsanswers);
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
                                string[] mechanicalAssessment = ["7"];
                                var newQuestionIdForMechanical = int.Parse(escapedStrings[10]) +10;
                                //if assessment type is in the list of 'strange' assessment types, move data into correct/expeted columns
                                //these assessment types (historically) have question and answer data in unexpected columns/tables
                                string[] strangeAssessmentTypes = ["4", "8", "17"];
                                    if (strangeAssessmentTypes.Contains(escapedStrings[1]))
                                    {
                                        questionTextIndex = 12;     //i.e. read from answers table
                                        answerTextIndex = 15;       //i.e. read from answerheadings table
                                        

                                        if (!globalHistoricAnswerText.Contains(escapedStrings[questionTextIndex]))
                                        {
                                            GenerateNewQuestionId();
                                            questionWriter.WriteLine($"({globalNewQuestionId}, {MapAssessmentId(escapedStrings[1])}, {escapedStrings[4]}, {N(escapedStrings[3])}, {N(escapedStrings[questionTextIndex])}, {N(escapedStrings[6])}, {N(escapedStrings[7])}, {N(escapedStrings[8])}),");
                                            globalHistoricAnswerText.Add(escapedStrings[questionTextIndex]);
                                        }
                                        answerWriter.WriteLine($"({GenerateNewAnswerId()}, {globalNewQuestionId}, {GenerateNewAnswerValue()}, {SetIsCorrectValues(escapedStrings[9])}, {N(escapedStrings[answerTextIndex])}, NULL, NULL, {N(escapedStrings[13].ToString())}),");

                                    }
                                    else
                                    {
                                        if (!globalHistoricQuestionIds.Contains(escapedStrings[10]))
                                        {

                                            if (mechanicalAssessment.Contains(escapedStrings[1]))
                                            {
                                                questionWriter.WriteLine($"({newQuestionIdForMechanical}, {MapAssessmentId(escapedStrings[1])}, {escapedStrings[4]}, {N(escapedStrings[3])}, {N(escapedStrings[questionTextIndex])}, {N(escapedStrings[6])}, {N(escapedStrings[7])}, {N(escapedStrings[8])}),");
                                            }
                                            else
                                            {
                                                questionWriter.WriteLine($"({escapedStrings[10]}, {MapAssessmentId(escapedStrings[1])}, {escapedStrings[4]}, {N(escapedStrings[3])}, {N(escapedStrings[questionTextIndex])}, {N(escapedStrings[6])}, {N(escapedStrings[7])}, {N(escapedStrings[8])}),");
                                            }
                                        }
                                        globalHistoricQuestionIds.Add(escapedStrings[10]);
                                    }

                                    if (mechanicalAssessment.Contains(escapedStrings[1]))
                                    {
                                        answerWriter.WriteLine($"({GenerateNewAnswerId()}, {newQuestionIdForMechanical}, {N(escapedStrings[11])}, {SetIsCorrectValues(escapedStrings[9])}, {N(escapedStrings[answerTextIndex])}, NULL, NULL, {N(escapedStrings[13].ToString())}),");
                                    }
                                    else
                                    {
                                        answerWriter.WriteLine($"({GenerateNewAnswerId()}, {escapedStrings[10]}, {N(escapedStrings[11])}, {SetIsCorrectValues(escapedStrings[9])}, {N(escapedStrings[answerTextIndex])}, NULL, NULL, {N(escapedStrings[13].ToString())}),");
                                    }
                                    //ImageTitle and ImageCaption are always null as the fields do not(historically) exist on the answers table, they are added for future accessibility
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

    //generates new sequential answer ID
    public static string GenerateNewQuestionId()
    {
        globalNewQuestionId++;
        return globalNewQuestionId.ToString();
    }
    public static string GenerateNewAnswerValue()
    {
        if (Enumerable.Range(1, 4).Contains(globalNewAnswerValue))
        { globalNewAnswerValue++; }
        else
        { globalNewAnswerValue =1; }
        return globalNewAnswerValue.ToString();
    }

    //set IsCorrect values is the answer reference data using their historic ID
    public static string SetIsCorrectValues(string input)
    {
        string[] correctHistoricAnswerIds = 
            ["1443", "1447", "1455", "1458", "1465", "1466", "1471", "1478", "1484", "1487", //numeric
            "1301", "1304", "1308", "1310", "1315", "1316", "1320", "1324", "1326", "1329",
            "1333", "1335", "1339", "1342", "1343", "1346", "1349", "1352", "1357", "1360", //verbal
            "1", "3", "10", "11", "12", "13", "16", "25", "26", "32", "33", "38", "39", "41", "42", "43", "44", "50",
            "52", "60", "62", "67", "69", "75", "76", "81", "90", "91", "94", "97", "101", "103", "108", "109", "115",
            "118", "125", "127", "128", "135", "136", "142", "144", "150", "155", "156", "165", "166", "168", "175", "178", 
            "182", "184", "186", "188", "189", "193", "194", "200", //checking (multiple choice)

            //checking
            //bitwise operators:
            /*
             * 1  A
             * 2  B
             * 4  C
             * 8  D
             * 16 E
             * === === ===
             * 1  = A
             * 2  = B
             * 3  = A, B
             * 4  = C
             * 5  = A, C
             * 6  = B, C
             * 7  = A, B, C
             * 8  = D
             * 9  = A, D
             * 10 = B, D
             * 11 = A, B, D
             * 12 = C, D
             * 13 = A, C, D
             * 14 = B, C, D
             * 15 = A, B, C, D
             * 16 = E
            */

            //suggest we have a function for the above that can be used when both saving answers and calculating results

            "967", "970", "974", "976", "981", "982", "987", "988", "993", "996", "997", //mechanical
            "1231", "1237", "1241", "1248", "1252", "1256", "1263", "1267", "1274", "1280", "1283", "1289", "1295", "1299", //spatial
            "1362", "1368", "1375", "1378", "1383", "1389", "1391", "1399", "1402", "1406", "1412", "1420", "1421", "1426", "1435", "1439"]; // abstract

        if (correctHistoricAnswerIds.Contains(input))
        { return 1.ToString(); } //true
        return 0.ToString(); //false
    }
}
