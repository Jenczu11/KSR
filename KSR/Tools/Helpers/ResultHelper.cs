using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace KSR.Tools.Helpers
{
    public class ResultHelper
    {
        public Dictionary<string, Dictionary<string, int>> result;

        public ResultHelper(Dictionary<string, Dictionary<string, int>> result)
        {
            this.result = result;
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
            {   sb.Append(String.Format("{0,-13}", a.Key.ToString()));
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
            {   Console.Write(a.Key.ToString()); Console.Write(";");
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
            sb.Append("\\begin{table}[]\n");
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
            {   sb.Append(String.Format("{0,-13}&", a.Key.ToString()));
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