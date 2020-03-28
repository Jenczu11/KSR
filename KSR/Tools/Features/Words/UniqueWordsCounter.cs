using System.Collections.Generic;
using KSR.Model;
using KSR.Tools.SimliarityFunctions;

namespace KSR.Tools.Features
{
    public class UniqueWordsCounter : IFeature
    {
        public ISimilarityFunction SimilarityFunction { get; set; }
        public bool IsContinouse { get; set; } = true;
        public double Calc(Article article, List<string> keyWords)
        {
            var uniqueWords = new List<string>();

            foreach (var word in article.AllWords)
            {
                if (!uniqueWords.Contains(word.ToLower()))
                {
                    uniqueWords.Add(word.ToLower());
                }
            }

            return uniqueWords.Count;
        }
    }
}