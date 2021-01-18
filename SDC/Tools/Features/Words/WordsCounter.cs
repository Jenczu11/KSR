using System;
using System.Collections.Generic;
using SDC.Model;
using SDC.Tools.SimliarityFunctions;
using TwinFinder.Base.Extensions;

namespace SDC.Tools.Features
{
    public class WordsCounter : IFeature
    {
        public ISimilarityFunction SimilarityFunction { get; set; }
        public bool IsContinouse { get; set; } = true;
        public double Calc(Article article, List<string> keyWords)
        {
            return article.AllWords.Length;
        }
    }
}