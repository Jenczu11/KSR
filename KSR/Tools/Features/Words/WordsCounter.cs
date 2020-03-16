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
        public double Calc(Article article, List<string> keyWords)
        {
            article.AllWords.ForEach(w =>
            {
                Console.Write(w); Console.Write(" ");
            } ); Console.WriteLine();
            return article.AllWords.Length;
        }
    }
}