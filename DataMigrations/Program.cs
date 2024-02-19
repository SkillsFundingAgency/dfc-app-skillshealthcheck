using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Reflection;

internal class Program
{




    private static void Main(string[] args)
    {
        CreateSQLScript(System.Environment.CurrentDirectory + "\\assessments.csv");
        
    }
    //Convert from csv to dictionary
    public static void CreateSQLScript(string path)
    {
        try
        {
            //read csv file from \dfc-app-skillshealthcheck\DataMigrations\bin\Debug\net8.0\ and write an insert script into file
            using (TextFieldParser parser = new TextFieldParser(path))
            {
                parser.SetDelimiters(new string[] { "," });
                //skips first line of column names
                parser.ReadLine();
                using (StreamWriter writer = new StreamWriter("INSERT_ASSESSMENTS.sql"))
                    {
                while (!parser.EndOfData)
                {

                    
                        string[] fields = parser.ReadFields();
                        string[] escapedStrings = fields.Select(str => str.Replace("'", "''")).ToArray();
                        {
                            writer.WriteLine($"(Type ,Title ,Subtitle ,Introduction) VALUES ('{escapedStrings[1]}', '{escapedStrings[4]}', '{escapedStrings[5]}', '{escapedStrings[6]}')");
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


}
