using System;
using TwinFinder.Matching.StringFuzzyCompare.Common;

namespace SDC.Tools.SimliarityFunctions
{
    public class LCSFunction : ISimilarityFunction
    {

        public double CalculateSimilarity(string firstWord, string secondWord)
        {
            LongestCommonSubsequence lcsDistance = new LongestCommonSubsequence();
            return lcsDistance.Compare(firstWord, secondWord);
        }

        public override string ToString()
        {
            return "LCS Function";
        }

    }
}
