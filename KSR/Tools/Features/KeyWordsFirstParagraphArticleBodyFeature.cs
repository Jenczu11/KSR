using System;
using System.Collections.Generic;
using KSR.Model;
using KSR.Tools.SimliarityFunctions;

namespace KSR.Tools.Features
{
    public class KeyWordsFirstParagraphArticleBodyFeature : IFeature
    {
        public ISimilarityFunction SimilarityFunction { get; set; }
        public bool IsContinouse { get; set; } = true;
        public double Calc(Article article, List<string> keyWords)
        {
            var count = 0;
            var words = article.Paragraphs[0];
            foreach (var item in keyWords)
            {
                if (words.Contains(item))
                {
                    count++;
                }
            }
            return count;
        }

        public override string ToString()
        {
            return "KeyWordsFirstParagraphArticleBodyFeature";
        }
    }
}
