using System;
using System.Collections.Generic;
using KSR.Model;
using KSR.Tools.SimliarityFunctions;

namespace KSR.Tools.Features
{
    public class KeyWordsFirstParagraphArticleBodyFeature : IFeature
    {
        public decimal Calc(Article article, List<string> keyWords, ISimilarityFunction similarityFunction)
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
    }
}
