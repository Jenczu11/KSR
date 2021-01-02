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
using KSR.Tools.Features;
using static KSR.Tools.Helpers.ResultHelper;
using Settings = KSR.Tools.Settings;
using KSR.Tools.SimliarityFunctions;
using System.Windows.Forms;
using System.ServiceProcess;

namespace KSR
{
    class Program
    {
        public static List<string> PLACES = new List<string>() { "west-germany", "usa", "france", "uk", "canada", "japan" };
        public static List<string> TOPICS = new List<string>() { "earn", "trade", "money-supply", "acq" };
        private static List<Article> articles;     // Musiałem dodać jako pole, jak masz pomysł jak wykonać podeślij ;p

        public const string PLACES_TAG = "places";
        public const string TOPIC_TAG = "topics";

        public static List<string> columns = new List<string>() { "test number", "k", "train/test", "metric", "feature set", "result type" };

        [STAThread]
        public static void Main(string[] args)
        {
            /* StopListHelper.LoadStopWords();
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
             var filteredArticles = new FilteredArticles(articles, PLACES_TAG, PLACES);
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
             var classifier = new KNNClassifier();
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
             */
            //RunApp();
            if (Environment.UserInteractive)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                ServiceBase.Run(new ClassificationService());
            }

        }


        private static Dictionary<string, ResultSet> Run(int k, IMetric metric, List<Article> training, List<Article> testing, List<string> tags)
        {
            IClassifier classifier = new KNNClassifier();
            classifier.Train(training);
            var result = new Dictionary<string, Dictionary<string, int>>();
            foreach (var item in tags)
            {
                result.Add(item, new Dictionary<string, int>());
                foreach (var item2 in tags)
                {
                    result[item].Add(item2, 0);
                }
            }
            //int count = 0;
            foreach (var item in testing)
            {
                item.GuessedLabel = classifier.Classify(item, k, metric);
                result[item.Label][item.GuessedLabel]++;
                /*if(count % 100 == 0)
                {
                    Console.WriteLine(string.Format("Classify step for {0} {1}", count, DateTime.Now));
                }
                count++;*/
            }
            var rh = new ResultHelper(result);
            rh.CalculateResults();
            return rh.resultSet;
        }

        public static void RunApp()
        {
            //var tags = new List<string>() { "west-germany", "usa", "france", "uk", "canada", "japan" };
            var tags = new List<string>() { "1", "2", "3" };
            //var tags = new List<string>() { "earn", "trade", "money-supply", "acq" };
            //var tag = "places";
            var tag = "level";
            //var tag = "topics";
            var articles = new List<Article>();
            var extractor = new TDFrequency();
            var manhattanMetric = new ManhattanMetric();
            var euclidesMetric = new EuclidesMetric();
            var chebyshevMatric = new ChebyshevMatric();
            var customMetric1 = new CustomMetric1();
            var customMetric2 = new CustomMetric2();
            var customMetric3 = new MinkowskiMetric();
            var canberraMetric = new CanBerraMetric();
            var hammingMetric = new HammingMetric();
            //var kList = new List<int>() { 6, 20 };
            //var kList = new List<int>() { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 };
            var kList = new List<int>() { 12, 14, 16, 18, 20, 22, 24, 26};
            //var kList = new List<int>() { 12 };
            //var trainDivide = new List<int>() { 70 };
            var trainDivide = new List<int>() { 30, 40, 50 };
            // var trainDivide = new List<int>() { 70 };

            var featuresList = new List<List<IFeature>>();
            var keyWords = new Dictionary<string, List<string>>();
            var columnesLocal = new List<string>();
            columnesLocal.AddRange(columns);
            columnesLocal.AddRange(tags);
            //featuresList.Add(GetFeatures1());
            featuresList.Add(GetFeatures2());
            //featuresList.Add(GetFeatures3());
            //featuresList.Add(GetFeatures4());
            StopListHelper.LoadStopWords();
            LogHelper.BeginLog();
            LogHelper.InitCSV(columnesLocal);
            if (File.Exists(Settings.articleSerializerPath) && !Settings.forceLoadArticles)
            {
                articles = ArticleSerializer.deserialize();
                Console.WriteLine(string.Format("Deserialized articles, number of articles: {0}", articles.Count()));
            }
            else
            {
                var reader = new DatabaseReader();
                articles = reader.GetArticles(true, false).ToList();
                foreach (var article in articles)
                {
                    var words = new List<string>();
                    article.Paragraphs.ForEach(item => words.AddRange(item));
                    article.AllWords = words.ToArray();
                }
                ArticleSerializer.serialize(articles);
                Console.WriteLine(string.Format("Serialized articles to {0}", Settings.articleSerializerPath));
            }

            var filteredPlaces = new FilteredArticles(articles, tag, tags);
            Console.WriteLine(string.Format("Filtered articles {0}", filteredPlaces.selectedArticles.Count));
            Console.WriteLine("Extract keywords");
            keyWords = KeyWordsHelper.GetKeyWordsDict(filteredPlaces.selectedArticles, 20, extractor, tag, true);
            int testNumber = 0;
            int featuresSet = 0;
            foreach (var features in featuresList)
            {
                Console.WriteLine(string.Format("Start extracting features {0}", DateTime.Now));
                filteredPlaces.selectedArticles.ForEach(item =>
                {
                    FeatureExtractorHelper.ExtractFeatureDict(features, ref item, keyWords);
                });
                Console.WriteLine(string.Format("End extracting features {0}", DateTime.Now));
                foreach (var testTraining in trainDivide)
                {
                    var learning = new LearningArticles(testTraining, filteredPlaces.selectedArticles);
                    var testing = new TestingArticles(100 - testTraining, filteredPlaces.selectedArticles);
                    foreach (var kitem in kList)
                    {
                        testNumber++;
                        IMetric metric = customMetric3;
                        Console.WriteLine(string.Format("Classification part 1 {0}", DateTime.Now));
                        LogHelper.WriteResultsCSV(testNumber, kitem, testTraining, metric.ToString(),
                            string.Format("featuresSet{0}", featuresSet),
                            Run(kitem, metric, learning.articles, testing.articles, tags));
                        SaveGlobalAccuracy(testNumber, kitem, testTraining, metric, featuresSet, testing);
                        metric = chebyshevMatric;
                        Console.WriteLine(string.Format("Classification part 2 {0}", DateTime.Now));
                        LogHelper.WriteResultsCSV(testNumber, kitem, testTraining, metric.ToString(),
                            string.Format("featuresSet{0}", featuresSet),
                            Run(kitem, metric, learning.articles, testing.articles, tags));
                        SaveGlobalAccuracy(testNumber, kitem, testTraining, metric, featuresSet, testing);
                        metric = manhattanMetric;
                        Console.WriteLine(string.Format("Classification part 3 {0}", DateTime.Now));
                        LogHelper.WriteResultsCSV(testNumber, kitem, testTraining, metric.ToString(),
                            string.Format("featuresSet{0}", featuresSet),
                            Run(kitem, metric, learning.articles, testing.articles, tags));
                        SaveGlobalAccuracy(testNumber, kitem, testTraining, metric, featuresSet, testing);
                        //metric = customMetric3;
                        //Console.WriteLine(string.Format("Classification part 4 {0}", DateTime.Now));
                        //LogHelper.WriteResultsCSV(testNumber, kitem, testTraining, metric.ToString(),
                        //    string.Format("featuresSet{0}", featuresSet),
                        //    Run(kitem, metric, learning.articles, testing.articles, tags));
                        //SaveGlobalAccuracy(testNumber, kitem, testTraining, metric, featuresSet, testing);
                        metric = hammingMetric;
                        Console.WriteLine(string.Format("Classification part 4 {0}", DateTime.Now));
                        LogHelper.WriteResultsCSV(testNumber, kitem, testTraining, metric.ToString(),
                            string.Format("featuresSet{0}", featuresSet),
                            Run(kitem, metric, learning.articles, testing.articles, tags));
                        SaveGlobalAccuracy(testNumber, kitem, testTraining, metric, featuresSet, testing);
                        // Console.WriteLine(string.Format("Classification part 2 {0}", DateTime.Now));
                        // LogHelper.WriteResultsCSV(testNumber, kitem, testTraining, manhattanMetric.ToString(), 
                        //     string.Format("featuresSet{0}", featuresSet), 
                        //     Run(kitem, manhattanMetric, learning.articles, testing.articles, tags));
                        //
                        // Console.WriteLine(string.Format("Classification part 3 {0}", DateTime.Now));
                        // LogHelper.WriteResultsCSV(testNumber, kitem, testTraining, euclidesMetric.ToString(), 
                        //     string.Format("featuresSet{0}", featuresSet), 
                        //     Run(kitem, euclidesMetric, learning.articles, testing.articles, tags));
                        //
                        // Console.WriteLine(string.Format("Classification part 4 {0}", DateTime.Now));
                        // LogHelper.WriteResultsCSV(testNumber, kitem, testTraining, customMetric1.ToString(), 
                        //     string.Format("featuresSet{0}", featuresSet), 
                        //     Run(kitem, customMetric1, learning.articles, testing.articles, tags));
                        //
                        // Console.WriteLine(string.Format("Classification part 5 {0}", DateTime.Now));
                        // LogHelper.WriteResultsCSV(testNumber, kitem, testTraining, customMetric2.ToString(), 
                        //     string.Format("featuresSet{0}", featuresSet), 
                        //     Run(kitem, customMetric2, learning.articles, testing.articles, tags));
                    }
                }
                featuresSet++;
            }
            Console.WriteLine("End!");

        }
        private static void SaveGlobalAccuracy(int testNumber, int kitem, int testTraining, IMetric metric, int featuresSet, TestingArticles testing)
        {
            var positive = 0.0d;
            var negetive = 0.0d;
            foreach (var item in testing.articles)
            {
                if (item.Label.Equals(item.GuessedLabel))
                {
                    positive++;
                }
                else
                {
                    negetive++;
                }
            }
            LogHelper.WriteResultsCSV(testNumber, kitem, testTraining, metric.ToString(),
                string.Format("featuresSet{0}", featuresSet), positive * 100 / (positive + negetive));
        }
        public static List<IFeature> GetFeatures1()
        {
            var result = new List<IFeature>();
            result.Add(new KeyWordsArticleBodyFeature());
            result.Add(new KeyWordsLastParagraphArticleBodyFeature());
            result.Add(new KeyWordsInNLastPercentArticleBodyFeature() { N = 10 });
            result.Add(new KeyWordsInNLastPercentArticleBodyFeature() { N = 20 });
            result.Add(new KeyWordsInNLastPercentArticleBodyFeature() { N = 50 });
            result.Add(new KeyWordsInNLastWordsArticleBodyFeature() { N = 10 });
            result.Add(new KeyWordsInNLastWordsArticleBodyFeature() { N = 20 });
            result.Add(new KeyWordsInNLastWordsArticleBodyFeature() { N = 50 });
            return result;
        }

        public static List<IFeature> GetFeatures2()
        {
            var result = new List<IFeature>();
            result.Add(new BinaryArticleBodyFeature());
            result.Add(new KeyWordsArticleBodyFeature());
            result.Add(new KeyWordsFirstParagraphArticleBodyFeature());
            result.Add(new KeyWordsInNPercentArticleBodyFeature() { N = 10 });
            result.Add(new KeyWordsInNPercentArticleBodyFeature() { N = 20 });
            result.Add(new KeyWordsInNPercentArticleBodyFeature() { N = 50 });
            result.Add(new KeyWordsInNWordsArticleBodyFeature() { N = 10 });
            result.Add(new KeyWordsInNWordsArticleBodyFeature() { N = 20 });
            result.Add(new KeyWordsInNWordsArticleBodyFeature() { N = 50 });
            result.Add(new ArticleHasAttachment());
            return result;
        }
        public static List<IFeature> GetFeatures3()
        {
            var result = new List<IFeature>();
            result.Add(new KeyWordsArticleBodyFeature());
            result.Add(new BinaryArticleBodyFeature());
            result.Add(new KeyWordsPercentageArticleBodyFeature());
            result.Add(new WordsCounter());
            result.Add(new UniqueWordsCounter());
            result.Add(new ShortWords());
            result.Add(new LongWords());
            return result;
        }
        public static List<IFeature> GetFeatures4()
        {
            var result = new List<IFeature>();
            result.Add(new KeyWordsArticleBodyFeature());
            result.Add(new BinaryArticleBodyFeature());
            result.Add(new KeyWordsPercentageArticleBodyFeature());
            result.Add(new Simliarity30PercentBody() { SimilarityFunction = new NGramFunction() });
            result.Add(new Simliarity30PercentBody() { SimilarityFunction = new NiewiadomskiFunction() });
            result.Add(new Simliarity30PercentBody() { SimilarityFunction = new BinaryFunction() });
            return result;
        }

    }
}
