using System;
using TwinFinder.Matching.StringFuzzyCompare.Common;

namespace KSR.Tools.SimliarityFunctions
{
    public class LCSFunction : ISimilarityFunction
    {

        public decimal CalculateSimilarity(string firstWord, string secondWord)
        {
            LongestCommonSubsequence lcsDistance = new LongestCommonSubsequence();
            return Convert.ToDecimal(lcsDistance.Compare(firstWord, secondWord));
        }

        public override string ToString()
        {
            return "Binary Function";
        }

    }
}
