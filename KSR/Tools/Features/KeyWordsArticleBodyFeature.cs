using System;
using System.Collections.Generic;
using System.Linq;
using KSR.Model;
using KSR.Tools.SimliarityFunctions;

namespace KSR.Tools.Features
{
    public class KeyWordsArticleBodyFeature : IFeature
    {
        public ISimilarityFunction SimilarityFunction { get; set; }

        public double Calc(Article article, List<string> keyWords)
        {
            var count = 0;
            var words = article.AllWords;
            foreach (var item in keyWords)
            {
                if (words.Contains(item))
                {
                    count++;
                }
            }
            return count;
        }
    }
}
