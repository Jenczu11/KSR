using System;
using System.Collections.Generic;
using KSR.Model;
using KSR.Tools.SimliarityFunctions;

namespace KSR.Tools.Features
{
    public class KeyWordsArticleBodyFeature : IFeature
    {

        public decimal Calc(Article article, List<string> keyWords, ISimilarityFunction similarityFunction)
        {
            var count = 0;
            var words = article.GetAllWords();
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
