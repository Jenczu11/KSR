using System;
namespace SDC.Tools.SimliarityFunctions
{
    public class LevenshteinFunction : ISimilarityFunction
    {
        public double CalculateSimilarity(string firstWord, string secondWord)
        {
            return Fastenshtein.Levenshtein.Distance(firstWord, secondWord);
        }

        public override string ToString()
        {
            return "Levenshtein Function";
        }
    }
}
