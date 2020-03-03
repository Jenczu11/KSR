using System;
using System.Collections.Generic;
using System.Linq;

namespace KSR.Tools.Frequency
{
    public class TFFrequency : IFrequency
    {
        public Dictionary<string, decimal> Calc(List<string> words)
        {
            var result = new Dictionary<string, decimal>();
            foreach (var word in words)
            {
                if (!result.ContainsKey(word))
                {
                    result.Add(word, 0);
                }                result[word]++;
            }
            return result.OrderByDescending(item => item.Value).ToDictionary(item => item.Key, item => item.Value);
        }
    }
}
