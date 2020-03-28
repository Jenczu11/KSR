using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR.Model;
using KSR.Tools.SimliarityFunctions;

namespace KSR.Tools.Features
{
    public class KeyWordsInNLastWordsArticleBodyFeature : IFeature
    {
        public ISimilarityFunction SimilarityFunction { get; set; }
        public int N { get; set; } = 50;
        public bool IsContinouse { get; set; } = true;
        public double Calc(Article article, List<string> keyWords)
        {
            var count = 0;
            var words = article.AllWords.Reverse();
            var wordsToTestTemp = new List<string>();
            if (words.Count() > N)
            {
                wordsToTestTemp = words.Take(N).ToList();

            }
            else
            {
                wordsToTestTemp = words.ToList();
            }
            var wordsToTest = wordsToTestTemp.ToArray();
            foreach (var item in keyWords)
            {
                if (wordsToTest.Contains(item))
                {
                    count++;
                }
            }
            return count;
        }

        public override string ToString()
        {
            return string.Format("KeyWordsIn{0}LastWordsArticleBodyFeature", N);
        }
    }
}
