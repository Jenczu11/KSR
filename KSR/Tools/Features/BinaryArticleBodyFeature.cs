using System;
using System.Collections.Generic;
using KSR.Model;

namespace KSR.Tools.Features
{
    public class BinaryArticleBodyFeature : IFeature
    {
        public decimal Calc(Article article, List<string> keyWords)
        {
            var count = 0;
            var limit = keyWords.Count;
            var words = article.GetAllWords();
            foreach (var item in keyWords)
            {
                if (words.Contains(item))
                {
                    count++;
                }
            }
            return limit == count ? 100 : 0;
        }
    }
}
