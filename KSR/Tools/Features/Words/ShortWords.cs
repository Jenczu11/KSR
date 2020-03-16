﻿using System.Collections.Generic;
using System.Linq;
using KSR.Model;
using KSR.Tools.SimliarityFunctions;

namespace KSR.Tools.Features
{
    public class ShortWords : IFeature
    
    {
        public ISimilarityFunction SimilarityFunction { get; set; }
        public double Calc(Article article, List<string> keyWords)
        {
            return article.AllWords.ToList().Count(w => w.Length <= 3 );
        }
    }
}