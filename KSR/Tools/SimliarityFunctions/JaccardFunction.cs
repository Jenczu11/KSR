using System;
using TwinFinder.Matching.StringFuzzyCompare.Common;

namespace KSR.Tools.SimliarityFunctions
{
    public class JaccardFunction : ISimilarityFunction
    {
        public double CalculateSimilarity(string firstWord, string secondWord)
        {
            Jaccard jaccardDistance = new Jaccard();
            return jaccardDistance.Compare(firstWord, secondWord);
        }

        public override string ToString()
        {
            return "Jaccard Function";
        }
    }
}
