using System.Collections.Generic;
using System.IO;
using SDC.Model;
using SDC.Tools.Filter;
using Newtonsoft.Json;

namespace SDC.Tools.Serializer
{
    public static class FilteredSerializer
    {
        public static void serialize(FilteredArticles filteredArticles)
        {
            string output = JsonConvert.SerializeObject(filteredArticles);
            File.WriteAllText(Settings.filteredArticleSerializerPath,output);
        }

        public static FilteredArticles deserialize()
        {
            string input = File.ReadAllText(Settings.filteredArticleSerializerPath);
            return JsonConvert.DeserializeObject<FilteredArticles>(input);
        }
    }
}