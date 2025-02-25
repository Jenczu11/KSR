﻿using System;
using System.Collections.Generic;
using System.Linq;
using SDC.Model;
using SDC.Tools.SimliarityFunctions;

namespace SDC.Tools.Features
{
    public class KeyWordsInNLastPercentArticleBodyFeature : IFeature
    {
        public ISimilarityFunction SimilarityFunction { get; set; }
        public int N { get; set; } = 20;
        public bool IsContinouse { get; set; } = true;
        public double Calc(Article article, List<string> keyWords)
        {
            var count = 0;
            var words = article.AllWords.Reverse();
            var wordsNPercent = words.Take(Convert.ToInt32(Math.Floor(words.Count() * Convert.ToDouble(N)/100)));
            foreach (var item in keyWords)
            {
                if (wordsNPercent.Contains(item))
                {
                    count++;
                }
            }
            return count;
        }

        public override string ToString()
        {
            return string.Format("KeyWordsIn{0}LastPercentArticleBodyFeature", N);
        }
    }
}
