using System;
using System.Collections.Generic;
using KSR.Tools.Features;
using KSR.Tools.Frequency;
using KSR.Tools.SimliarityFunctions;

namespace KSR.Tools
{
    public static class Settings
    {
        public static int testingPercentage = 60;
        public static int learningPercentage = 40;
        public static int kNNNeighbours = 6;
        public static string articleSerializerPath = "data.txt";
        public static string filteredArticleSerializerPath = "dataFiltered.txt";
        public static string DirectoryForResults = "results";
        public static Dictionary<IFeature, bool> featuresSettings = new Dictionary<IFeature, bool>()
        {
            //Other
            {new BinaryArticleBodyFeature(), false },
            {new KeyWordsArticleBodyFeature(), true },
            {new KeyWordsPercentageArticleBodyFeature(), true },
            //Paragraphs
            {new KeyWordsFirstParagraphArticleBodyFeature(), true },
            {new KeyWordsLastParagraphArticleBodyFeature(), true },
            //First %
            {new KeyWordsInNPercentArticleBodyFeature(){N = 10 }, true },
            {new KeyWordsInNPercentArticleBodyFeature(){N = 20 }, true },
            {new KeyWordsInNPercentArticleBodyFeature(){N = 50 }, true },
            //First n
            {new KeyWordsInNWordsArticleBodyFeature(){ N = 20}, false },
            {new KeyWordsInNWordsArticleBodyFeature(){ N = 50}, true },
            {new KeyWordsInNWordsArticleBodyFeature(){ N = 70}, false },
            //Last %
            {new KeyWordsInNLastPercentArticleBodyFeature(){ N = 10}, true },
            {new KeyWordsInNLastPercentArticleBodyFeature(){ N = 20}, false },
            {new KeyWordsInNLastPercentArticleBodyFeature(){ N = 50}, false },
            //Last n
            {new KeyWordsInNLastWordsArticleBodyFeature(){ N = 20}, false },
            {new KeyWordsInNLastWordsArticleBodyFeature(){ N = 50}, true },
            {new KeyWordsInNLastWordsArticleBodyFeature(){ N = 70}, false },
            //Words lenght
            {new WordsCounter(), false },
            {new UniqueWordsCounter(), false },
            {new ShortWords(), false },
            {new LongWords(), false },
            //Similarity 30%
            {new Simliarity30PercentBody(){ SimilarityFunction = new BinaryFunction()}, false },
            {new Simliarity30PercentBody(){ SimilarityFunction = new JaccardFunction()}, false },
            {new Simliarity30PercentBody(){ SimilarityFunction = new LCSFunction()}, false },
            {new Simliarity30PercentBody(){ SimilarityFunction = new LevenshteinFunction()}, false },
            {new Simliarity30PercentBody(){ SimilarityFunction = new NGramFunction()}, false },
            {new Simliarity30PercentBody(){ SimilarityFunction = new NiewiadomskiFunction()}, false },
            //Similarity whole
            {new SimilarityBodyFeature(){ SimilarityFunction = new NGramFunction()}, false },
        };
        public static int keyWords = 40;
        public static string stoplistfile = @"../../Data/stoplist.txt";
        public static IFrequency keyWordsExtractor = new TDFrequency();
        public static bool normalize = false;
        public static bool normalizeVertical = true;
        public static bool divideToLabels = true;
        public static bool stemmization = false;
        public static bool stopListLib = false;
        public static bool forceLoadArticles = false;

        public static void printCurrentSettings()
        {
            Console.WriteLine("-------------------Printing settings------------------------------");

            Console.WriteLine("{");
            foreach (var fe in Settings.featuresSettings)
            {
                var t = fe.Key.ToString();
                if (fe.Key.ToString() == "SimilarityBodyFeature" && fe.Value == true)
                    Console.WriteLine(string.Format("SimilarityBodyFeature -> {0},", fe.Key.SimilarityFunction));
                else if (fe.Key.ToString() == "Simliarity30PercentBody" && fe.Value == true)
                    Console.WriteLine(string.Format("Simliarity30PercentBody -> {0},", fe.Key.SimilarityFunction));
                else if (fe.Key.ToString() == "SimliarityFirstParagraph" && fe.Value == true)
                    Console.WriteLine(string.Format("SimliarityFirstParagraph -> {0},", fe.Key.SimilarityFunction));
                else if (fe.Value)
                    Console.WriteLine(string.Format("{0},", fe.Key));
            }
            Console.WriteLine("}");
            Console.WriteLine(string.Format("Testing percentage: {0}, Learning percentage: {1}", Settings.testingPercentage, Settings.learningPercentage));
            Console.WriteLine(string.Format("kNN neighbors: {0}", Settings.kNNNeighbours));
            Console.WriteLine(string.Format("Chosen keyWordsExtractor: {0}", Settings.keyWordsExtractor.ToString()));
            Console.WriteLine("------------------------------------------------------------------");
        }
    }
}