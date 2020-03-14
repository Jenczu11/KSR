using System;
namespace KSR.Tools.SimliarityFunctions
{
    public class NiewiadomskiFunction : ISimilarityFunction
    {
        public double CalculateSimilarity(string firstWord, string secondWord)
        {
            var result = 0d;
            var fwl = firstWord.Length;
            var swl = secondWord.Length;
            var max = Math.Max(firstWord.Length, secondWord.Length);
            var fractional = 2 / (Math.Pow(max, 2) + max);
            if (fwl < swl)
            {
                var temp = firstWord;
                firstWord = secondWord;
                secondWord = temp;
            }
            for (int i = 0; i < fwl; i++)
            {
                for (int j = 0; j < fwl - i; j++)
                {
                    if (secondWord.Contains(firstWord.Substring(j, i)))
                    {
                        result++;
                    }
                }
            }

            return result * fractional;
        }
    }
}
