using System;
using System.Collections.Generic;
using KSR.Model;
using KSR.Tools.SimliarityFunctions;

namespace KSR.Tools.Features
{
    public class SimliarityFirstParagraph : IFeature
    {
        public ISimilarityFunction SimilarityFunction { get; set; }
        public bool IsContinouse { get; set; } = true;
        public double Calc(Article article, List<string> keyWords)
        {
            var count = 0d;
            var words = article.Paragraphs[0].ToArray();
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
            return "SimliarityFirstParagraph";
        }
    }
}
