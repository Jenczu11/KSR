using System;
using System.Collections.Generic;
using System.Linq;
using KSR.Model;
using KSR.Tools.SimliarityFunctions;
using NumSharp.Utilities;

namespace KSR.Tools.Features
{
    public class Simliarity30PercentBody : IFeature
    {
        public ISimilarityFunction SimilarityFunction { get; set; }

        public double Calc(Article article, List<string> keyWords)
        {
            var count = 0d;

            var words = article.AllWords.Take(Convert.ToInt32(Math.Floor(article.AllWords.Length*0.3))).ToArray();
            foreach (var word in keyWords)
            {
                foreach (var item in words)
                {
                    count += SimilarityFunction.CalculateSimilarity(item, word);
                }
            }
            return count;
        }
    }
}
