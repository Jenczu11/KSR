using System;
namespace KSR.Tools.SimliarityFunctions
{
    public class LevenshteinFunction : ISimilarityFunction
    {
        public decimal CalculateSimilarity(string firstWord, string secondWord)
        {
            return Fastenshtein.Levenshtein.Distance(firstWord, secondWord);
        }

        public override string ToString()
        {
            return "Levenshtein Function";
        }
    }
}
