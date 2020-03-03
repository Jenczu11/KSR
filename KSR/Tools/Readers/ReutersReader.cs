using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Annytab.Stemmer;
using HtmlAgilityPack;
using KSR.Model;
using StopWord;

namespace KSR.Tools.Readers
{
    public class ReutersReader : IReader
    {
        private static Stemmer stemmer = new EnglishStemmer();
        private List<string> sources { get; set; }
        public ReutersReader()
        {
            sources = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                sources.Add(string.Format(@"../../Data/reut2-{0:000}.sgm", i));
            }
        }

        public IEnumerable<Article> GetArticles()
        {
            var regex = new Regex("[^a-zA-Z]");
            foreach (var item in sources)
            {
                var raw = File.ReadAllText(item);
                var html = new HtmlDocument();
                html.LoadHtml(raw);
                foreach (var article in html.DocumentNode.Descendants("REUTERS"))
                {
                    var body = article.Descendants("BODY").FirstOrDefault();
                    var tags = new Dictionary<string, List<string>>();
                    foreach (var tag in article.ChildNodes.Where(node => node.ChildNodes.Any(htmlNode => htmlNode.Name == "d")))
                    {
                        tags[tag.Name] = tag.Descendants("D").Select(node => node.InnerText).ToList();
                    }
                    if (body != null && tags.Count > 0)
                    {
                        yield return new Article()
                        {
                            Title = article.Descendants("TITLE").First().InnerText.Replace("&lt;", "<"),
                            DateLine = article.Descendants("DATELINE").First().InnerText,
                            Paragraphs = body
                                .InnerText
                                .Split(new string[] { "    " }, StringSplitOptions.RemoveEmptyEntries)
                                .ToList()
                                .Select(
                                    words => regex
                                        .Replace(words, " ")
                                        //.ToLower()
                                        .RemoveStopWords("en") //Usign https://github.com/hklemp/dotnet-stop-words
                                        .Split(' ')
                                        .Where(item => item.Length > 2)
                                        .Select(item => stemmer.GetSteamWord(item)) //Using https://github.com/annytab/a-stemmer
                                        .ToList()
                                    )
                                .ToList(),
                            Tags = tags
                        };
                    }
                }
            }
        }
    }
}
