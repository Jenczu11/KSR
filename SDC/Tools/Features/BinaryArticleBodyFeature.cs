using System;
using System.Collections.Generic;
using System.Linq;
using SDC.Model;
using SDC.Tools.SimliarityFunctions;

namespace SDC.Tools.Features
{
    public class BinaryArticleBodyFeature : IFeature
    {
        public ISimilarityFunction SimilarityFunction { get; set; }

        public bool IsContinouse { get; set; } = false;

        public double Calc(Article article, List<string> keyWords)
        {
            var count = 0;
            var limit = keyWords.Count;
            var words = article.AllWords;
            foreach (var item in keyWords)
            {
                if (words.Contains(item))
                {
                    count++;
                }
            }
            return limit == count ? 100 : 0;
        }

        public override string ToString()
        {
            return "BinaryArticleBodyFeature";
        }
        
    }
}
