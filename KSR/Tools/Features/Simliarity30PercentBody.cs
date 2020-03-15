using System;
using System.Collections.Generic;
using System.Linq;
using KSR.Model;
using KSR.Tools.SimliarityFunctions;


namespace KSR.Tools.Features
{
    public class Simliarity30PercentBody : IFeature
    {
        private IFeature _featureImplementation;
        public ISimilarityFunction SimilarityFunction { get; set; }

        public double Calc(Article article, List<string> keyWords)
        {
            var count = 0d;
            List<string> w1 = new List<string>(); 
            for (int i = 0; i < Convert.ToInt32(Math.Floor(article.AllWords.Length * 0.30)); i++)
            {
                w1.Add(article.AllWords[i]);
            }
            // var words = article.AllWords.Take(Convert.ToInt32(Math.Floor(article.AllWords.Length * 0.30))).ToArray();
            var words = w1.ToArray();

            foreach (var word in keyWords)
            {
                foreach (var item in words)
                {
                    count += SimilarityFunction.CalculateSimilarity(item, word);
                }
            }
            return count;
        }

        public override string ToString()
        {
            return "Simliarity30PercentBody";
        }
    }
}
