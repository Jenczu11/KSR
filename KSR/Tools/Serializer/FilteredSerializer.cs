using System.Collections.Generic;
using System.IO;
using KSR.Model;
using KSR.Tools.Filter;
using Newtonsoft.Json;

namespace KSR.Tools.Serializer
{
    public static class FilteredSerializer
    {
        public static void serialize(FilteredArticles filteredArticles)
        {
            string output = JsonConvert.SerializeObject(filteredArticles);
            File.WriteAllText("dataFiltered.txt",output);
        }

        public static FilteredArticles deserialize()
        {
            string input = File.ReadAllText("dataFiltered.txt");
            return JsonConvert.DeserializeObject<FilteredArticles>(input);
        }
    }
}