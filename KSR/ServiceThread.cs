using KSR.Model;
using KSR.Tools.Classifier;
using KSR.Tools.Factories;
using KSR.Tools.Features;
using KSR.Tools.Filter;
using KSR.Tools.Helpers;
using KSR.Tools.Metrics;
using KSR.Tools.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;
using static KSR.Tools.Helpers.ResultHelper;

namespace KSR
{
    public class ServiceThread
    {
        private ServiceSettings settings { get; set; }
        private List<Article> articles { get; set; }
        private int k = 0;
        private IClassifier classifier { get; set; }
        private IMetric metric { get; set; }
        private List<IFeature> features { get; set; }

        private Dictionary<string, List<string>> keyWordsDict { get; set; }
        private List<string> keyWordsSingle { get; set; }

        private bool isNormalization { get; set; }
        private bool isStopList { get; set; }
        private bool isStemmization { get; set; }
        private bool ownSets { get; set; }
        private List<string> tags = new List<string>() { "1", "2", "3", "guess" };

        public ServiceThread()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var settingsPath = Path.Combine(path, "settings.xml");
            BaseLogs.WriteLog("Load settings");
            settings = loadXML(settingsPath);
            if (settings.stoplist)
            {
                BaseLogs.WriteLog("Init stoplist");
                StopListHelper.LoadStopWords(settings.stoplistPath);
            }
            IReader reader = new DatabaseReader();
            features = FeatureFactory.GetFeatures(settings.features);
            classifier = ClassifierFactory.GetClassifier(settings.classifiers);
            metric = MetricsFactory.GetMetric(settings.metrics);
            var frequency = FrequencyFactory.GetFrequency(settings.frequency);
            var parts = PartsFactory.GetParts(settings.parts);
            isNormalization = settings.normalization;
            isStopList = settings.stoplist;
            isStemmization = settings.stemmize;
            ownSets = settings.onwSets;

            var tag = "level";
            var kList = new List<int>() { 12, 14, 16, 18, 20, 22, 24, 26 };
            var trainDivide = new List<int>() { parts.Item1 };
            BaseLogs.WriteLog("Load articles");
            articles = reader.GetArticles(true, false).ToList();
            BaseLogs.WriteLog("Get keywords");
            keyWordsDict = KeyWordsHelper.GetKeyWordsDict(articles, 20, frequency, tag, true);
            keyWordsSingle = KeyWordsHelper.GetKeyWords(articles, 20, frequency, tag, true);
            BaseLogs.WriteLog("Extract features");
            if (ownSets)
            {
                articles.ForEach(item =>
                {
                    FeatureExtractorHelper.ExtractFeatureDict(features, ref item, keyWordsDict);
                });
            }
            else
            {
                articles.ForEach(item =>
                {
                    FeatureExtractorHelper.ExtractFeature(features, ref item, keyWordsSingle);
                });
            }
            BaseLogs.WriteLog("Begin train");
            foreach (var testTraining in trainDivide)
            {
                var learning = new LearningArticles(testTraining, articles);
                var testing = new TestingArticles(100 - testTraining, articles);
                if (isNormalization)
                {
                    NormalizeHelper.NormalizeVertical(ref learning, ref testing);
                }
                var max_accuracy = 0d;
                foreach (var kitem in kList)
                {
                    var result = Run(classifier, kitem, metric, learning.articles, testing.articles, tags);
                    var globalResult = SaveGlobalAccuracy(testing);
                    if (globalResult > max_accuracy)
                    {
                        k = kitem;
                        max_accuracy = globalResult;
                    }
                    BaseLogs.WriteLog(string.Format("Accuracy = {0} for k = {1}", globalResult, kitem));
                }
                BaseLogs.WriteLog(string.Format("Max accuracy = {0} for k = {1}", max_accuracy, k));
            }

            BaseLogs.WriteLog("End train");

        }
        private static Dictionary<string, ResultSet> Run(IClassifier classifier, int k, IMetric metric, List<Article> training, List<Article> testing, List<string> tags)
        {
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
            foreach (var item in testing)
            {
                item.GuessedLabel = classifier.Classify(item, k, metric);
                result[item.Label][item.GuessedLabel]++;
            }
            var rh = new ResultHelper(result);
            rh.CalculateResults();
            return rh.resultSet;
        }
        private static double SaveGlobalAccuracy(TestingArticles testing)
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
            return positive * 100 / (positive + negetive);
        }

        private ServiceSettings loadXML(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception("Settings file not found");
            }
            var result = new ServiceSettings();
            XmlSerializer xs = new XmlSerializer(typeof(ServiceSettings));
            using (var sr = new StreamReader(path))
            {
                result = (ServiceSettings)xs.Deserialize(sr);
            }
            return result;
        }

        public void classify()
        {
            var startDate = DateTime.Now;
            while (startDate.AddHours(24) > DateTime.Now && Thread.CurrentThread.ThreadState != ThreadState.AbortRequested)
            {
                BaseLogs.WriteLog("Begin classification");
                IReader reader = new DatabaseOnlyNewReader();
                var articlesToClassify = reader.GetArticles(isStemmization, isStopList).ToList();
                if (ownSets)
                {
                    articlesToClassify.ForEach(item =>
                    {
                        FeatureExtractorHelper.ExtractFeatureDict(features, ref item, keyWordsDict);
                    });
                }
                else
                {
                    articlesToClassify.ForEach(item =>
                    {
                        FeatureExtractorHelper.ExtractFeature(features, ref item, keyWordsSingle);
                    });
                }
                foreach (var item in articlesToClassify)
                {
                    item.GuessedLabel = classifier.Classify(item, k, metric);
                }
                BaseLogs.WriteLog("End classification");
                Thread.Sleep(1000 * 60 * 5);
            }

        }
    }
}
