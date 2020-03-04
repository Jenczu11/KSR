﻿using System;
using System.Collections.Generic;
using System.Linq;
using KSR.Model;

namespace KSR.Tools.Features
{
    public class KeyWords20PercentArticleBodyFeature : IFeature
    {
        public decimal Calc(Article article, List<string> keyWords)
        {
            var count = 0;
            var words = article.GetAllWords();
            var words20Percent = words.Take(Convert.ToInt32(Math.Floor(words.Count * 0.2)));
            foreach (var item in keyWords)
            {
                if (words20Percent.Contains(item))
                {
                    count++;
                }
            }
            return count;
        }
    }
}
