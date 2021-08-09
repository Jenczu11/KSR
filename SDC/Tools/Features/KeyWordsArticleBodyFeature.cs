﻿using System;
using System.Collections.Generic;
using System.Linq;
using SDC.Model;
using SDC.Tools.SimliarityFunctions;

namespace SDC.Tools.Features
{
    public class KeyWordsArticleBodyFeature : IFeature
    {
        public ISimilarityFunction SimilarityFunction { get; set; }
        public bool IsContinouse { get; set; } = true;
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

        public override string ToString()
        {
            return "KeyWordsArticleBodyFeature";
        }
    }
}
