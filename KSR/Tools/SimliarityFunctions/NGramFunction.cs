﻿using System;
using TwinFinder.Matching.StringFuzzyCompare.Common;

namespace KSR.Tools.SimliarityFunctions
{
    public class NGramFunction : ISimilarityFunction
    {
        public int n { get; set; } = 3;
        public decimal CalculateSimilarity(string firstWord, string secondWord)
        {
            NGramDistance nGramDistance = new NGramDistance
            {
                NGramLength = n
            };
            return Convert.ToDecimal(nGramDistance.Compare(firstWord, secondWord));
        }
        public override string ToString()
        {
            return "N Gramm Function";
        }
    }
}