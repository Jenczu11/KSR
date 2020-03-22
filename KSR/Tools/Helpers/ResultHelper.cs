using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace KSR.Tools.Helpers
{
    public class ResultHelper
    {
        class ResultSet
        {
            public decimal Accuracy = 0m;
            public decimal Recall = 0m;
            public decimal Precision = 0m;
            public int TP = 0;
            public int TN = 0;
            public int FP = 0;
            public int FN = 0;
        }
        public Dictionary<string, Dictionary<string, int>> result;
        private Dictionary<string, ResultSet> resultSet;
        public ResultHelper(Dictionary<string, Dictionary<string, int>> result)
        {
            this.result = result;
            resultSet = new Dictionary<string, ResultSet>();
        }

        public void CalculateResults()
        {
            foreach (var actual in result)
            {
                var label = actual.Key;
                var results = new ResultSet();
                results.TP = actual.Value[label];
                results.FN = actual.Value.Where(item => item.Key != label).Select(item => item.Value).Sum();
                foreach (var item in result.Where(item => item.Key != label).Select(item => item.Value))
                {
                    results.FP += item[label];
                }
                foreach (var item in result.Where(item => item.Key != label))
                {
                    results.TN += item.Value[item.Key];
                }
                results.Accuracy = Convert.ToDecimal(results.TP + results.TN) * 100m / Convert.ToDecimal(results.TP + results.FP + results.FN + results.TN);
                results.Precision = Convert.ToDecimal(results.TP) * 100m / Convert.ToDecimal(results.TP + results.FP);
                results.Recall = Convert.ToDecimal(results.TP) * 100m / Convert.ToDecimal(results.TP + results.FN);
                var info = string.Empty;
                info += string.Format("Label = {0}{1}", label, Environment.NewLine);
                info += string.Format("Accuracy = {0:00.00}{1}", results.Accuracy, Environment.NewLine);
                info += string.Format("Precision = {0:00.00}{1}", results.Precision, Environment.NewLine);
                info += string.Format("Recall = {0:00.00}{1}", results.Recall, Environment.NewLine);
                Console.Write(info);
            }
        }

        public void Print()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append(String.Format("{0,13}", " "));
            foreach (var VARIABLE in result)
            {
                sb.Append(String.Format("{0,13}", VARIABLE.Key.ToString()));
            }

            sb.AppendLine();

            foreach (var a in result)
            {
                sb.Append(String.Format("{0,-13}", a.Key.ToString()));
                foreach (var b in a.Value)
                {
                    sb.Append(String.Format("{0,13}", b.Value));
                }

                sb.AppendLine();
            }
            Console.WriteLine(sb);
        }

        public void PrintToCSV()
        {
            Console.Write("empty;");
            foreach (var VARIABLE in result)
            {
                Console.Write(VARIABLE.Key.ToString());
                Console.Write(";");
            }
            Console.WriteLine();
            foreach (var a in result)
            {
                Console.Write(a.Key.ToString()); Console.Write(";");
                foreach (var b in a.Value)
                {
                    Console.Write(b.Value); Console.Write(";");
                }
                Console.WriteLine();
            }
        }

        public void PrintToLaTeX()
        {
            var sb = new System.Text.StringBuilder();
            var tabularCount = result.Count + 1;
            sb.Append("\\begin{table}[htb]\n");
            sb.Append("\\begin{tabular}{");
            for (int i = 0; i < tabularCount; i++)
            {
                sb.Append("l");
            }
            sb.AppendLine("}");

            sb.Append(String.Format("{0,13}&", " "));
            foreach (var VARIABLE in result)
            {
                sb.Append(String.Format("{0,13}&", VARIABLE.Key.ToString()));
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("\\\\");
            sb.AppendLine();

            foreach (var a in result)
            {
                sb.Append(String.Format("{0,-13}&", a.Key.ToString()));
                foreach (var b in a.Value)
                {
                    sb.Append(String.Format("{0,13}&", b.Value));
                }

                sb.Remove(sb.Length - 1, 1);
                sb.Append("\\\\");
                sb.AppendLine();
            }
            sb.AppendLine("\\end{tabular}");
            sb.AppendLine("\\end{table}");
            Console.WriteLine(sb);
        }
    }
}