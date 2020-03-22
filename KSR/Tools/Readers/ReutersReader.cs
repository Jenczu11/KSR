using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Annytab.Stemmer;
using HtmlAgilityPack;
using KSR.Model;
using KSR.Tools.Helpers;
using StopWord;

namespace KSR.Tools.Readers
{
    public class ReutersReader : IReader
    {
        private static Stemmer stemmer = new EnglishStemmer();
        private List<string> sources { get; set; }
        public int filesCount { get; set; } = 21;
        public ReutersReader()
        {
            sources = new List<string>();
            for (int i = 0; i < filesCount; i++)
            {
                sources.Add(string.Format(@"../../Data/reut2-{0:000}.sgm", i));
            }
        }

        public IEnumerable<Article> GetArticles()
        {
            var regex = new Regex("[^a-zA-Z]");
#if DEBUG
            int count = 0;
#endif
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
#if DEBUG
                        if (count % 100 == 0)
                        {
                            Console.WriteLine("Article " + count);
                        }
                        count++;
#endif
                        yield return new Article()
                        {
                            Title = article.Descendants("TITLE").First().InnerText.Replace("&lt;", "<"),
                            //DateLine = article.Descendants("DATELINE").First().InnerText,
                            Paragraphs = body
                                .InnerText
                                .Split(new string[] { "    " }, StringSplitOptions.RemoveEmptyEntries)
                                .ToArray()
                                .Select(
                                    words =>
                                    {
                                        var temp1 = regex.Replace(words, " ");
                                        temp1 = temp1.ToLower();
                                        if (Settings.stopListLib)
                                        {
                                            temp1 = temp1.RemoveStopWords("en"); //Usign https://github.com/hklemp/dotnet-stop-words
                                        }
                                        var temp2 = temp1.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                                        var temp3 = temp2.Where(item => item.Length > 2);
                                        temp3 = temp3.Where(item => StopListHelper.StopWord(item));
                                        if (Settings.stemmization)
                                        {
                                            temp3 = temp3.Select(item => stemmer.GetSteamWord(item)); //Using https://github.com/annytab/a-stemmer
                                        }
                                        temp3 = temp3.Where(item => item.Length > 0);
                                        return temp3.ToList();
                                        /*regex.Replace(words, " ")
                                        .ToLower()
                                        //.RemoveStopWords("en") //Usign https://github.com/hklemp/dotnet-stop-words
                                        .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                                        .Where(item => item.Length > 2)
                                        .Where(item => StopListHelper.StopWord(item))
                                        //.Select(item => stemmer.GetSteamWord(item)) //Using https://github.com/annytab/a-stemmer
                                        //.Where(item => item.Length > 0)
                                        .ToList();*/
                                    }
                                    )
                                .ToList(),
                            Tags = tags,
                            AllWords = new List<string>().ToArray()
                        };
                    }
                }
            }
        }
    }
}
/*{
    
}*/
