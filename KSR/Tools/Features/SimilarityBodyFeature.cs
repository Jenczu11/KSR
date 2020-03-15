using System;
using System.Collections.Generic;
using KSR.Model;
using KSR.Tools.SimliarityFunctions;

namespace KSR.Tools.Features
{
    public class SimilarityBodyFeature : IFeature
    {
        public ISimilarityFunction SimilarityFunction { get; set; }

        public double Calc(Article article, List<string> keyWords)
        {
            var count = 0d;
            var words = article.AllWords;
            foreach (var word in keyWords)
            {
                foreach (var item in words)
                {
                    count += SimilarityFunction.CalculateSimilarity(item, word);
                }
            }
            return count;
        }

        public override string ToString()
        {
            return "SimilarityBodyFeature";
        }
    }
}
