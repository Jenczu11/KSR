using System;
using TwinFinder.Matching.StringFuzzyCompare.Common;

namespace SDC.Tools.SimliarityFunctions
{
    public class BinaryFunction : ISimilarityFunction
    {
        // If first == second return true
        public double CalculateSimilarity(string firstWord, string secondWord)
        {
            Identity identityDistance = new Identity();
            return identityDistance.Compare(firstWord, secondWord);
        }

        public override string ToString()
        {
            return "Binary Function";
        }
    }
}