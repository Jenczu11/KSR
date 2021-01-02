using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSR.Tools.Helpers
{
    public static class StopListHelper
    {
        private static List<string> stopList = new List<string>();
        public static void LoadStopWords(string path = "")
        {
            var text = File.ReadAllText(path == "" ? Settings.stoplistfile : path);
            stopList = text.Split(new string[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        public static bool StopWord(string word)
        {
            if (stopList.Count == 0)
            {
                throw new Exception("StopList not inicialized");
            }
            else
            {
                return !stopList.Contains(word);
            }
        }
    }
}
