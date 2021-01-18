using System.Collections.Generic;

namespace SDC.Tools.Helpers
{
    public static class DictHelper
    {
        public static string DictionaryToString(Dictionary<string, double> dictionary)
        {
            string dictionaryString = "{";
            foreach (KeyValuePair<string, double> keyValues in dictionary)
            {
                dictionaryString += keyValues.Key + " : " + keyValues.Value + ", ";
            }
            return dictionaryString.TrimEnd(',', ' ') + "}";
        }
    }
}