using System;
using System.Collections;
using KSR.Tools.Readers;
using System.Linq;
using KSR.Model;
using System.Collections.Generic;
using KSR.Tools.Filter;
using KSR.Tools.Frequency;
using KSR.Tools.Helpers;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using KSR.Tools;
using KSR.Tools.Serializer;
using Newtonsoft.Json;
using TwinFinder.Base.Extensions;

namespace KSR
{
    class Program
    {
        public static List<string> PLACES = new List<string>() { "west-germany", "usa", "france", "uk", "canada", "japan" };
        private static IEnumerable<Article> articles;     // Musiałem dodać jako pole, jak masz pomysł jak wykonać podeślij ;p

        public const string PLACES_TAG = "places";

        public static void Main(string[] args)
        {
            Console.WriteLine(Directory.Exists(Settings.DirectoryForResults) ? "Directory for results exists." : "Directory does not exist.");
            if (Directory.Exists(Settings.DirectoryForResults))
            {
                //TODO na resultaty coś i trzeba wynieść do innej klasy (moze jakis setup)   
            }
            else
            {
                Console.WriteLine("Creating directory....");
                Directory.CreateDirectory(Settings.DirectoryForResults);
            }

            Console.WriteLine(File.Exists(Settings.articleSerializerPath) ? "File with articles exists." : "File does not exist.");
            if (File.Exists(Settings.articleSerializerPath))
            {
                articles = ArticleSerializer.deserialize();
                Console.WriteLine(string.Format("Deserialized articles, number of articles: {0}", articles.Count()));
            }
            else
            {
                var reader = new ReutersReader();
                articles = reader.GetArticles();
                ArticleSerializer.serialize(articles);
                Console.WriteLine(string.Format("Serialized articles to {0}", Settings.articleSerializerPath));
            }
            var filteredArticles = new FilteredArticles(articles);
            Console.WriteLine(string.Format("Filtered articles, number of filtered articles: {0}", filteredArticles.Count()));

            LearningArticles la = new LearningArticles(Settings.learningPercentage, filteredArticles);
            TestingArticles ta = new TestingArticles(Settings.testingPercentage, filteredArticles);
            la.PrintNumberOfArticles();
            ta.PrintNumberOfArticles();
            Console.WriteLine(la.Count + ta.Count);


            // var reader = new ReutersReader();
            // var articles = reader.GetArticles();
            // Console.WriteLine(articles.Count());
            // var articles = ArticleSerializer.deserialize();
            // var filteredArticles = new FilteredArticles(articles);
            // string input = "";
            // Console.WriteLine(art.Count());

            // var filteredArticles = new FilteredArticles(la._articles);
            // Console.WriteLine(filteredArticles.Count());

            // var selected = articles
            //     .Where(item => item.Tags.ContainsKey(PLACES_TAG))
            //     .Where(item => item.Tags[PLACES_TAG].Count == 1)
            //     .Where(item => PLACES.Contains(item.Tags[PLACES_TAG][0]))
            //     .ToList();
            //
            // selected.ForEach(item => Console.WriteLine(string.Format("Title: {0}, DateLine: {1}, Place: {2}", item.Title, item.DateLine, item.Tags[PLACES_TAG][0])));
            //


            // var reader = new ReutersReader();
            // var articles = reader.GetArticles();

            // var filteredArticles = new FilteredArticles(articles);
            // Console.WriteLine(filteredArticles.Count());
            // Console.ReadLine();
            /*// Console.WriteLine("Po filtrowaniu");
            // Console.WriteLine(filteredArticles.GetArticleSingleParagraph(0, 1));
            // string ParagraphText = string.Join(" ", filteredArticles.GetArticleSingleParagraph(0, 1).ToArray());
            // filteredArticles.printArticle(0);
            // Console.WriteLine();
            // filteredArticles.printSingleParagraph(0,0);
            // selected[0].Paragraphs[0].ForEach(word => Console.WriteLine(word));
            Console.Write(filteredArticles.getListOfAllWords().Count);
            var tf = new TFFrequency();
            //var KeyWords = KeyWordsHelper.GetKeyWords(filteredArticles.selectedArticles, 1, tf, PLACES_TAG);

            var wordsInTags = new Dictionary<string, List<string>>();
            foreach (var article in filteredArticles.selectedArticles)
            {
                var tag = article.Tags[PLACES_TAG][0];
                if (!wordsInTags.ContainsKey(tag))
                {
                    wordsInTags.Add(tag, new List<string>());
                }
                article.Paragraphs.ForEach(words => wordsInTags[tag].AddRange(words));
            }
            var keys = wordsInTags.Keys;
            // var result = new Dictionary<string, List<string>>();
            //var resultFile = File.Open(@"Logs.csv", FileMode.OpenOrCreate);
            var file = File.AppendText(@"Logs.csv");
            foreach (var key in keys)
            {
                var wordsFrequency = tf.Calc(wordsInTags[key]);
                foreach (var word in wordsFrequency)
                {
                    var line = string.Format("\"{0}\",\"{1}\",\"{2}\"{3}", key, word.Key, word.Value, Environment.NewLine);
                    //Console.Write(line);
                    file.Write(line);
                }
                //result.Add(key, wordsFrequency.Keys.Take(Convert.ToInt32(wordsFrequency.Count * 1)).ToList());
            }
            file.Close();
            //Console.Write(DictHelper.DictionaryToString(tf.Calc(filteredArticles.getListOfAllWords())));
            Console.ReadLine();*/
        }
    }
}
