using System;
using System.Collections.Generic;
using System.Linq;
using SDC.Model;

namespace SDC.Tools.Frequency
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
                var set = new SortedSet<string>(words);
/*                foreach (var word in words)
                {
                    if (!temp.Contains(word))
                    {
                        temp.Add(word);
                    }
                }*/
                foreach (var item in set)
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
