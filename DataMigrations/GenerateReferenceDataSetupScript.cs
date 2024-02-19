﻿using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

internal class GenerateReferenceDataSetupScript
{
    //input filename(s) must match the strings below
    public const string assessments = "assessments";
    public const string questionsanswers = "questionsanswers";

    private static void Main(string[] args)
    {
        CreateSQLScript(assessments);
        CreateSQLScript(questionsanswers);
    }
    //generates sql snippet which is inserted into the post deployment script
    public static void CreateSQLScript(string input)
    {
        try
        {
            //read .csv file from \dfc-app-skillshealthcheck\DataMigrations\bin\Debug\net8.0\ and write an insert script into file
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
                            string[] fields = parser.ReadFields();
                            string[] escapedStrings = fields.Select(str => str.Replace("'", "''")).ToArray();
                            {
                                writer.WriteLine($"(Type, Title, Subtitle, Introduction) VALUES ('{escapedStrings[1]}', '{escapedStrings[4]}', '{escapedStrings[5]}', '{escapedStrings[6]}')");
                            }
                        }
                        break;

                    case questionsanswers:

                        const string questionWriterPrefix = "(AssessmentId, Number, Text, DataHTML, ImageTitle, ImageCaption, ImageURL) ";
                        const string answerWriterPrefix = "(QuestionId, Value, IsCorrect, Text, ImageTitle, ImageCaption, ImageURL) ";

                        using (StreamWriter questionWriter = new StreamWriter($"INSERT_questions.sql"))
                        using (StreamWriter answerWriter = new StreamWriter($"INSERT_answers.sql"))

                        while (!parser.EndOfData)
                        {
                            string[] fields = parser.ReadFields();
                            string[] escapedStrings = fields.Select(str => str.Replace("'", "''")).ToArray();
                            {
                                int questionTextIndex = 5;
                                int answerTextIndex = 12;
                                string[] strangeAssessmentTypes = ["4", "8", "17"];

                                //if assessment type is in the list of strange assessment types, move data into correct/expeted columns
                                if (strangeAssessmentTypes.Contains(escapedStrings[1]))
                                {
                                    questionTextIndex = 12;
                                    answerTextIndex = 15;
                                }
                                questionWriter.WriteLine(questionWriterPrefix +
                                    $"VALUES ({escapedStrings[1]}, {escapedStrings[4]}, '{escapedStrings[3]}', '{escapedStrings[questionTextIndex]}'), '{escapedStrings[6]}'), '{escapedStrings[7]}'), '{escapedStrings[8]}')");

                                answerWriter.WriteLine(answerWriterPrefix +
                                    $"VALUES ('{escapedStrings[10]}', '{escapedStrings[11]}', {999}, '{escapedStrings[answerTextIndex]}', '', '', '{escapedStrings[13]}')");
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Default case, input filename(s) do not match expected range");
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
