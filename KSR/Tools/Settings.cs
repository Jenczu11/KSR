using System.Collections.Generic;
using KSR.Tools.Features;
using KSR.Tools.SimliarityFunctions;

namespace KSR.Tools
{
    public static class Settings
    {
        public static int testingPercentage = 40;
        public static int learningPercentage = 60;
        public static string articleSerializerPath = "data.txt";
        public static string filteredArticleSerializerPath = "dataFiltered.txt";
        public static string DirectoryForResults = "results";
        public static Dictionary<IFeature, bool> featuresSettings = new Dictionary<IFeature, bool>()
        {
            {new BinaryArticleBodyFeature(), true },
            {new KeyWords20PercentArticleBodyFeature(), false },
            {new KeyWordsArticleBodyFeature(), false },
            {new KeyWordsFirstParagraphArticleBodyFeature(), false },
            {new SimliarityFirstParagraph(){ SimilarityFunction = new BinaryFunction()}, true },
            {new SimliarityFirstParagraph(){ SimilarityFunction = new JaccardFunction()}, false },
            {new SimliarityFirstParagraph(){ SimilarityFunction = new LCSFunction()}, false },
            {new SimliarityFirstParagraph(){ SimilarityFunction = new LevenshteinFunction()}, false },
            {new SimliarityFirstParagraph(){ SimilarityFunction = new NGramFunction()}, true },
            {new SimliarityFirstParagraph(){ SimilarityFunction = new NiewiadomskiFunction()}, true },

        };
        public static int keyWords = 20;
        public static string stoplistfile = @"../../Data/stoplist.txt";
    }
}