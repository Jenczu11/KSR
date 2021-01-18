using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SDC.Tools.Helpers.ResultHelper;

namespace SDC.Tools.Helpers
{
    public static class LogHelper
    {
        private static string logFilePath = string.Empty;
        private static string logFilePathCsv = string.Empty;
        public static void BeginLog()
        {
            Console.WriteLine("BeginLog");
            if (!Directory.Exists(Settings.DirectoryForResults))
            {
                Console.WriteLine("Creating directory for results");
                Directory.CreateDirectory(Settings.DirectoryForResults);
            }
            var fileName = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".log";
            var fileNameCsv = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".csv";
            logFilePath = Path.Combine(Settings.DirectoryForResults, fileName);
            logFilePathCsv = Path.Combine(Settings.DirectoryForResults, fileNameCsv);
            var fs = new StreamWriter(logFilePath, true);
            fs.WriteLine(string.Format("Begin log: {0}", DateTime.Now));
            fs.Close();
        }

        public static void InitCSV(List<string> columns)
        {
            string columsRow = string.Empty;
            foreach (var item in columns)
            {
                columsRow += string.Format("\"{0}\"", item);
                if (!item.Equals(columns.Last()))
                {
                    columsRow += ",";
                }
            }
            var fs = new StreamWriter(logFilePathCsv, true);
            fs.WriteLine(columsRow);
            fs.Close();
        }

        public static void WriteSettings(int numberOfTest, int k, int train, string metric, List<string> features)
        {
            var featuresLine = string.Empty;
            features.ForEach(item => featuresLine += (item + ","));
            var fs = new StreamWriter(logFilePath, true);
            fs.WriteLine(string.Format("Test number = {0}", numberOfTest));
            fs.WriteLine(string.Format("k = {0}", k));
            fs.WriteLine(string.Format("Train = {0}", train));
            fs.WriteLine(string.Format("Test = {0}", 100 - train));
            fs.WriteLine(string.Format("Metric = {0}", metric));
            fs.WriteLine(string.Format("Features = [{0}]", featuresLine.Substring(0, featuresLine.Length - 1)));
            fs.Close();
        }

        public static void WriteResults(Dictionary<string, ResultSet> resultSet)
        {
            var fs = new StreamWriter(logFilePath, true);
            foreach (var item in resultSet)
            {
                var label = item.Key;
                var results = item.Value;
                var info = string.Empty;
                info += string.Format("Label = {0}{1}", label, Environment.NewLine);
                info += string.Format("Accuracy = {0:00.00}{1}", results.Accuracy, Environment.NewLine);
                info += string.Format("Precision = {0:00.00}{1}", results.Precision, Environment.NewLine);
                info += string.Format("Recall = {0:00.00}{1}", results.Recall, Environment.NewLine);
                fs.Write(info);
                fs.WriteLine("---------------------------------------------------------------------------");
            }
            fs.Close();
        }

        public static void WriteResultsCSV(int numberOfTest, int k, int train, string metric, string featuresSet, Dictionary<string, ResultSet> resultSet)
        {
            var row = string.Empty;
            row += string.Format("\"{0}\",", numberOfTest);
            row += string.Format("\"{0}\",", k);
            row += string.Format("\"{0} / {1}\",", train, 100-train);
            row += string.Format("\"{0}\",", metric);
            row += string.Format("\"{0}\",", featuresSet);
            var accuracy = "\"accuracy\",";
            var precision = "\"precision\",";
            var recall = "\"recall\",";
            foreach(var item in resultSet)
            {
                accuracy += string.Format("\"{0:0.000}\",", item.Value.Accuracy);
                precision += string.Format("\"{0:0.000}\",", item.Value.Precision);
                recall += string.Format("\"{0:0.000}\",", item.Value.Recall);
            }
            accuracy = accuracy.Substring(0, accuracy.Length - 1);
            precision = precision.Substring(0, precision.Length - 1);
            recall = recall.Substring(0, recall.Length - 1);
            var fs = new StreamWriter(logFilePathCsv, true);
            fs.WriteLine(string.Format("{0}{1}", row, accuracy));
            fs.WriteLine(string.Format("{0}{1}", row, precision));
            fs.WriteLine(string.Format("{0}{1}", row, recall));
            fs.Close();

        }

        public static void WriteResultsCSV(int numberOfTest, int k, int train, string metric, string featuresSet, double accuracyValue)
        {
            var row = string.Empty;
            row += string.Format("\"{0}\",", numberOfTest);
            row += string.Format("\"{0}\",", k);
            row += string.Format("\"{0} / {1}\",", train, 100 - train);
            row += string.Format("\"{0}\",", metric);
            row += string.Format("\"{0}\",", featuresSet);
            var accuracy = "\"accuracy_all\",";
            accuracy += string.Format("\"{0:0.000}\", \"\",\"\",\"\",\"\",\"\"", accuracyValue);
            var fs = new StreamWriter(logFilePathCsv, true);
            fs.WriteLine(string.Format("{0}{1}", row, accuracy));
            fs.Close();

        }

    }
}
