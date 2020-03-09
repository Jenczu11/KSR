using System.Collections.Generic;
using System.IO;
using KSR.Model;
using Newtonsoft.Json;

namespace KSR.Tools.Serializer
{
    public static class ArticleSerializer
    {
        public static void serialize(IEnumerable<Article> articles)
        {
            string output = JsonConvert.SerializeObject(articles);
            File.WriteAllText(Settings.articleSerializerPath,output);
        }

        public static IEnumerable<Article> deserialize()
        {
            string input = File.ReadAllText(Settings.articleSerializerPath);
            return JsonConvert.DeserializeObject<IEnumerable<Article>>(input);
        }
    }
}