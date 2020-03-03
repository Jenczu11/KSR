using System;
using System.Collections.Generic;
using System.Linq;

namespace KSR.Tools.Frequency
{
    public class IDFFrequency : IFrequency
    {

        public Dictionary<string, decimal> Calc(List<string> words)
        {
            var count = words.Count;
            var result = new Dictionary<string, decimal>();
            var tfDict = new TFFrequency().Calc(words);
            foreach (var item in tfDict)
            {
                result.Add(item.Key, item.Value / words.Count);
            }
            return result.OrderByDescending(item => item.Value).ToDictionary(item => item.Key, item => item.Value);
        }
    }
}
