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
using KSR.Tools.Classifier;
using KSR.Tools.Metrics;
using System.Drawing;
using KSR.Tools.CSVReader;
using NDesk.Options;
using Settings = KSR.Tools.Settings;

namespace KSR
{
    class Program
    {
        public static List<string> PLACES = new List<string>() { "west-germany", "usa", "france", "uk", "canada", "japan" };
        private static List<Article> articles;     // Musiałem dodać jako pole, jak masz pomysł jak wykonać podeślij ;p

        public const string PLACES_TAG = "places";

        public static void Main(string[] args)
        {
            StopListHelper.LoadStopWords();
            // ArgsParser argsParser = new ArgsParser(args);
            // argsParser.setSettings();
            var scsv = new SettingsCSVReader();
                scsv.readSettingsFromCSV();
                scsv.parseToArgs(0);
            new ArgsParser(scsv.parseToArgs(0));

            new ArgsParser(args);

            Console.WriteLine(DateTime.Now);
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
            if (File.Exists(Settings.articleSerializerPath) && !Settings.forceLoadArticles)
            {
                articles = ArticleSerializer.deserialize();
                Console.WriteLine(string.Format("Deserialized articles, number of articles: {0}", articles.Count()));
            }
            else
            {
                var reader = new ReutersReader();
                articles = reader.GetArticles().ToList();
                foreach (var article in articles)
                {
                    var words = new List<string>();
                    article.Paragraphs.ForEach(item => words.AddRange(item));
                    article.AllWords = words.ToArray();
                }
                ArticleSerializer.serialize(articles);
                Console.WriteLine(string.Format("Serialized articles to {0}", Settings.articleSerializerPath));
            }
            var filteredArticles = new FilteredArticles(articles);
            Console.WriteLine(string.Format("Filtered articles, number of filtered articles: {0}", filteredArticles.Count()));

            LearningArticles la = new LearningArticles(Settings.learningPercentage, filteredArticles.selectedArticles);
            TestingArticles ta = new TestingArticles(Settings.testingPercentage, filteredArticles.selectedArticles);
            la.PrintNumberOfArticles();
            ta.PrintNumberOfArticles();
            Console.WriteLine(la.Count + ta.Count);

            Console.WriteLine(string.Format("Extrace keywords start, Time = {0}", DateTime.Now));
            var keyWords = KeyWordsHelper.GetKeyWords(filteredArticles.selectedArticles, Settings.keyWords, Settings.keyWordsExtractor, PLACES_TAG, true);
            var keyWordsDict = KeyWordsHelper.GetKeyWordsDict(filteredArticles.selectedArticles, Settings.keyWords, Settings.keyWordsExtractor, PLACES_TAG, true);
            Console.WriteLine(string.Format("Extrace keywords end, Time = {0}", DateTime.Now));
            keyWords.ForEach(item => Console.WriteLine(item));
            foreach (var keyWord in keyWordsDict)
            {
                Console.WriteLine(keyWord.Key);
                keyWord.Value.ForEach(item => Console.Write(item + " "));
                Console.WriteLine();
            }
            var features = Settings.featuresSettings
                            .Where(item => item.Value)
                            .Select(item => item.Key)
                            .ToList();
            Console.WriteLine(string.Format("Start learnig extraction, Time = {0}", DateTime.Now));
            int count = 0;
            la.articles.ForEach(item =>
            {
                count++;
                if (Settings.divideToLabels)
                {
                    FeatureExtractorHelper.ExtractFeatureDict(features, ref item, keyWordsDict);
                }
                else
                {
                    FeatureExtractorHelper.ExtractFeature(features, ref item, keyWords);
                }
                if (count % 100 == 0)
                {
                    Console.WriteLine(string.Format("Learning articles extracted {0}, Time = {1}", count, DateTime.Now));
                }
            });
            Console.WriteLine(string.Format("Learnig extracted, Time = {0}", DateTime.Now));
            Console.WriteLine(string.Format("Start training extraction, Time = {0}", DateTime.Now));
            count = 0;
            ta.articles.ForEach(item =>
            {
                count++;
                if (Settings.divideToLabels)
                {
                    FeatureExtractorHelper.ExtractFeatureDict(features, ref item, keyWordsDict);
                }
                else
                {
                    FeatureExtractorHelper.ExtractFeature(features, ref item, keyWords);
                }
                if (count % 100 == 0)
                {
                    Console.WriteLine(string.Format("Training articles extracted {0}, Time = {1}", count, DateTime.Now));
                }
            });
            Console.WriteLine(string.Format("Training extracted, Time = {0}", DateTime.Now));
            if (Settings.normalizeVertical)
            {
                Console.WriteLine(string.Format("Normalize fratures vertical start, Time = {0}", DateTime.Now));
                NormalizeHelper.NormalizeVertical(ref la, ref ta);
                Console.WriteLine(string.Format("Normalize fratures vertical end, Time = {0}", DateTime.Now));
            }
            Console.WriteLine("Start clasify");
            var classifier = new DecisionTreeClasifier2();
            var metric = new ManhattanMetric();
            classifier.Train(la.articles);

            var result = new Dictionary<string, Dictionary<string, int>>();
            foreach (var item in PLACES)
            {
                result.Add(item, new Dictionary<string, int>());
                foreach (var item2 in PLACES)
                {
                    result[item].Add(item2, 0);
                }
            }

            var positive = 0m;
            var negative = 0m;

            foreach (var item in ta.articles)
            {
                item.GuessedLabel = classifier.Classify(item, Settings.kNNNeighbours, metric);
                result[item.Label][item.GuessedLabel]++;
                if (item.Label == item.GuessedLabel)
                {
                    positive++;
                }
                else
                {
                    negative++;
                }
                if ((positive + negative) % 100 == 0)
                {
                    Console.WriteLine(string.Format("Positive {0}, Negative {1}, Result {2:00.00}%, Time {3}", positive, negative, 100 * positive / (positive + negative), DateTime.Now));
                }
            }

            Console.Write("         ");
            foreach (var VARIABLE in result)
            {
                Console.Write(VARIABLE.Key.ToString());
                Console.Write("    ");
            }
            Console.WriteLine();
            foreach (var a in result)
            {
                Console.Write(a.Key.ToString()); Console.Write("        ");
                foreach (var b in a.Value)
                {
                    Console.Write(b.Value); Console.Write("            ");
                }
                Console.WriteLine();
            }

            Console.WriteLine(string.Format("Positive {0}, Negative {1}, Result {2:00.00}%, Time {3}", positive, negative, 100 * positive / (positive + negative), DateTime.Now));
            Console.WriteLine("Finish");

            var rh = new ResultHelper(result);
            rh.CalculateResults();
            rh.Print();
            rh.PrintToCSV();
            rh.PrintToLaTeX();
            Console.ReadLine();

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
