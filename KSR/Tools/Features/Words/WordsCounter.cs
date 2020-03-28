using System;
using System.Collections.Generic;
using KSR.Model;
using KSR.Tools.SimliarityFunctions;
using TwinFinder.Base.Extensions;

namespace KSR.Tools.Features
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