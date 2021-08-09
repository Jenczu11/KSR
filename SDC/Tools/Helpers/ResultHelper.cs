using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace SDC.Tools.Helpers
{
    public class ResultHelper
    {
        public class ResultSet
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
        public Dictionary<string, ResultSet> resultSet { get; set; }
        private int NumberOfTest { get; set; } = 0;
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
                if (results.TP + results.FP > 0)
                {
                    results.Precision = Convert.ToDecimal(results.TP) * 100m / Convert.ToDecimal(results.TP + results.FP);
                }
                if (results.TP + results.FN > 0)
                {
                    results.Recall = Convert.ToDecimal(results.TP) * 100m / Convert.ToDecimal(results.TP + results.FN);
                }
                resultSet.Add(label, results);
            }
        }

        public void Print()
        {
            var sb = string.Empty;
            sb += string.Format("{0,13}", " ");
            foreach (var VARIABLE in result)
            {
                sb += string.Format("{0,13}", VARIABLE.Key.ToString());
            }

            sb += Environment.NewLine;

            foreach (var a in result)
            {
                sb += string.Format("{0,-13}", a.Key.ToString());
                foreach (var b in a.Value)
                {
                    sb += string.Format("{0,13}", b.Value);
                }

                sb += Environment.NewLine;
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
            var sb = string.Empty;
            var tabularCount = result.Count + 1;
            sb += "\\begin{table}[htb]\n";
            sb += "\\begin{tabular}{";
            for (int i = 0; i < tabularCount; i++)
            {
                sb += "l";
            }
            sb += "}";
            sb += Environment.NewLine;
            sb += string.Format("{0,13}&", " ");
            foreach (var VARIABLE in resultSet)
            {
                sb += string.Format("{0,13}&", VARIABLE.Key.ToString());
            }
            sb = sb.Substring(0, sb.Length - 1);
            sb += "\\\\";
            sb += Environment.NewLine;

            foreach (var a in result)
            {
                sb += string.Format("{0,-13}&", a.Key.ToString());
                foreach (var b in a.Value)
                {
                    sb += string.Format("{0,13}&", b.Value);
                }

                sb = sb.Substring(0, sb.Length - 1);
                sb += "\\\\";
                sb += Environment.NewLine;
            }
            sb += "\\end{tabular}";
            sb += Environment.NewLine;
            sb += "\\caption{Macież pomyłek dla badania nr " + NumberOfTest.ToString() + "}";
            sb += Environment.NewLine;
            sb += "\\label{tab:tab-true-nr-" + NumberOfTest.ToString() + "}";
            sb += Environment.NewLine;
            sb += "\\end{table}";
            sb += Environment.NewLine;

            sb += "\\begin{table}[htb]\n";
            sb += "\\begin{tabular}{llll}";
            sb += Environment.NewLine;
            sb += string.Format("{0,13}&", " ");
            sb += string.Format("{0,13}&", "Accuracy");
            sb += string.Format("{0,13}&", "Precission");
            sb += string.Format("{0,13}", "Recall");
            sb += "\\\\";
            sb += Environment.NewLine;

            foreach (var a in resultSet)
            {
                sb += string.Format("{0,-13}&", a.Key.ToString());
                sb += string.Format("{0:00.000}&", a.Value.Accuracy);
                sb += string.Format("{0:00.000}&", a.Value.Precision);
                sb += string.Format("{0:00.000}", a.Value.Recall);
                sb += "\\\\";
                sb += Environment.NewLine;
            }
            sb += "\\end{tabular}";
            sb += Environment.NewLine;
            sb += "\\caption{Tabela wyników dla badania nr " + NumberOfTest.ToString() + "}";
            sb += Environment.NewLine;
            sb += "\\label{tab:tab-res-nr-" + NumberOfTest.ToString() + "}";
            sb += Environment.NewLine;
            sb += "\\end{table}";
            Console.WriteLine(sb);
        }
    }
}