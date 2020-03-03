using System;
using System.Collections.Generic;

namespace KSR.Tools.Frequency
{
    public class TFFrequency : IFrequency
    {
        public static Dictionary<string, decimal> Calc(List<string> words)
        {
            var result = new Dictionary<string, decimal>();
            foreach (var word in words)
            {
                if (!result.ContainsKey(word))
                {
                    result.Add(word, 0);
                }                result[word]++;
            }
            return result;
        }
    }
}
