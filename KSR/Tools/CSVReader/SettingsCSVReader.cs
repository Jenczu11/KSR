using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;
using TwinFinder.Base.Extensions;

namespace KSR.Tools.CSVReader
{
    public class SettingsCSVReader
    {
        private SettingsCSV[] records;
        public void readSettingsFromCSV()
        {
            using (var reader = new StreamReader("Settings.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {    
                csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();
                records = csv.GetRecords<SettingsCSV>().ToArray();
                Console.WriteLine(records);
            }
        }

        public string[] parseToArgs(int i)
        {
            return records[i].parseToArgs();
        }
        
    }
    
    class SettingsCSV
    {
        public string feature1 { get; set; }
         public string feature2 { get; set; }
         public string feature3 { get; set; }
         public string feature4 { get; set; }
         public string feature5 { get; set; }
         public int testingpercentage { get; set; }
         public int learningpercentage { get; set; }
         public int keywordsExtractor { get; set; }
         public int kneighbors { get; set; }
         public int keywords { get; set; }

         public string[] parseToArgs()
         {
             string[] args = new string[20];
             args[0] = "-f1";
             args[1] = feature1;
             args[2] = "-f2";
             args[3] = feature2;
             args[4] = "-f3";
             args[5] = feature3;
             args[6] = "-f4";
             args[7] = feature4;
             args[8] = "-f5";
             args[9] = feature5;
             args[10] = "t";
             args[11] = testingpercentage.ToString();
             args[12] = "l";
             args[13] = learningpercentage.ToString();
             args[14] = "kwe";
             args[15] = keywordsExtractor.ToString();
             args[16] = "k";
             args[17] = kneighbors.ToString();
             args[18] = "kw";
             args[19] = keywords.ToString();
             return args;
         }
    }
}