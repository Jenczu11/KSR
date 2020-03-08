using System;
using System.Collections.Generic;
using KSR.Model;
using KSR.Tools.SimliarityFunctions;

namespace KSR.Tools.Features
{
    public class SimilarityBodyFeature : IFeature
    {
        public decimal Calc(Article article, List<string> keyWords, ISimilarityFunction similarityFunction)
        {
            var count = 0m;
            var words = article.GetAllWords();
            foreach (var word in keyWords)
            {
                words.ForEach(item => count += similarityFunction.CalculateSimilarity(item, word));
            }
            return count;
        }
    }
}
