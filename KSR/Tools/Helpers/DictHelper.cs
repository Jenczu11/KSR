using System.Collections.Generic;

namespace KSR.Tools.Helpers
{
    public static class DictHelper
    {
        public static string DictionaryToString(Dictionary < string, decimal > dictionary) {  
            string dictionaryString = "{";  
            foreach(KeyValuePair < string, decimal > keyValues in dictionary) {  
                dictionaryString += keyValues.Key + " : " + keyValues.Value + ", ";  
            }  
            return dictionaryString.TrimEnd(',', ' ') + "}";  
        } 
    }
}