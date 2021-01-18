using System;
using System.Collections.Generic;
using System.Linq;
using SDC.Model;

namespace SDC.Tools.Frequency
{
    public class TFFrequency : IFrequency
    {
        public Dictionary<string, double> Calc(List<Article> articles)
        {
            var result = new Dictionary<string, double>();
            foreach (var article in articles)
            {
                var words = article.AllWords;
                foreach (var word in words)
                {
                    if (!result.ContainsKey(word))
                    {
                        result.Add(word, 0);
                    }
                    result[word]++;
                }

            }
            return result.OrderByDescending(item => item.Value).ToDictionary(item => item.Key, item => item.Value);
        }
    }
}
