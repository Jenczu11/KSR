﻿using System;
using TwinFinder.Matching.StringFuzzyCompare.Common;

namespace KSR.Tools.SimliarityFunctions
{
    public class BinaryFunction : ISimilarityFunction
    {
        // If first == second return true
        public decimal CalculateSimilarity(string firstWord, string secondWord)
        {
            Identity identityDistance = new Identity();
            return Convert.ToDecimal(identityDistance.Compare(firstWord, secondWord));
        }

        public override string ToString()
        {
            return "Binary Function";
        }
    }
}