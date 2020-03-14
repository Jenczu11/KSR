using System;
using System.Collections.Generic;
using System.Linq;
using KSR.Model;

namespace KSR.Tools.Frequency
{
    public class TDFrequency : IFrequency
    {
        public Dictionary<string, double> Calc(List<Article> articles)
        {
            var result = new Dictionary<string, double>();
            foreach (var article in articles)
            {
                var words = article.AllWords;
                var temp = new List<string>();
                foreach (var word in words)
                {
                    if (!temp.Contains(word))
                    {
                        temp.Add(word);
                    }
                }
                foreach (var item in temp)
                {
                    if (!result.ContainsKey(item))
                    {
                        result.Add(item, 0);
                    }
                    result[item]++;
                }
            }
            return result.OrderByDescending(item => item.Value).ToDictionary(item => item.Key, item => item.Value);
        }
    }
}
