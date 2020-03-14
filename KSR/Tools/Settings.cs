using System.Collections.Generic;
using KSR.Tools.Features;
using KSR.Tools.SimliarityFunctions;

namespace KSR.Tools
{
    public static class Settings
    {
        public static int testingPercentage = 30;
        public static int learningPercentage = 70;
        public static string articleSerializerPath = "data.txt";
        public static string filteredArticleSerializerPath = "dataFiltered.txt";
        public static string DirectoryForResults = "results";
        public static Dictionary<IFeature, bool> featuresSettings = new Dictionary<IFeature, bool>()
        {
            {new BinaryArticleBodyFeature(), true },
            {new KeyWords20PercentArticleBodyFeature(), true },
            {new KeyWordsArticleBodyFeature(), true },
            {new KeyWordsFirstParagraphArticleBodyFeature(), true },
            {new SimilarityBodyFeature(){ SimilarityFunction = new BinaryFunction()}, true },
            {new SimilarityBodyFeature(){ SimilarityFunction = new JaccardFunction()}, false },
            {new SimilarityBodyFeature(){ SimilarityFunction = new LCSFunction()}, false },
            {new SimilarityBodyFeature(){ SimilarityFunction = new LevenshteinFunction()}, false },
            {new SimilarityBodyFeature(){ SimilarityFunction = new NGramFunction()}, false },
            {new SimilarityBodyFeature(){ SimilarityFunction = new NiewiadomskiFunction()}, false },

        };
        public static int keyWords = 20;
        public static string stoplistfile = @"../../Data/stoplist.txt";
    }
}