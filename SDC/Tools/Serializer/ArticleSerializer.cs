﻿using System.Collections.Generic;
using System.IO;
using SDC.Model;
using Newtonsoft.Json;

namespace SDC.Tools.Serializer
{
    public static class ArticleSerializer
    {
        public static void serialize(IEnumerable<Article> articles)
        {
            string output = JsonConvert.SerializeObject(articles);
            File.WriteAllText(Settings.articleSerializerPath, output);
        }

        public static List<Article> deserialize()
        {
            string input = File.ReadAllText(Settings.articleSerializerPath);
            return JsonConvert.DeserializeObject<List<Article>>(input);
        }
    }
}