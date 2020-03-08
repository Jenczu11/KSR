using System;
using TwinFinder.Matching.StringFuzzyCompare.Common;

namespace KSR.Tools.SimliarityFunctions
{
    public class JaccardFunction : ISimilarityFunction
    {
        public decimal CalculateSimilarity(string firstWord, string secondWord)
        {
            Jaccard jaccardDistance = new Jaccard();
            return Convert.ToDecimal(jaccardDistance.Compare(firstWord, secondWord));
        }

        public override string ToString()
        {
            return "Jaccard Function";
        }
    }
}
