using System;
using System.Collections.Generic;
using System.Linq;
using SDC.Model;

namespace SDC.Tools.Frequency
{
    public class IDFFrequency : IFrequency
    {

        public Dictionary<string, double> Calc(List<Article> articles)
        {
            /*var count = words.Count;
            var result = new Dictionary<string, double>();
            var tfDict = new TFFrequency().Calc(words);
            foreach (var item in tfDict)
            {
                result.Add(item.Key, item.Value / words.Count);
            }
            return result.OrderByDescending(item => item.Value).ToDictionary(item => item.Key, item => item.Value);*/

            throw new NotImplementedException();
        }
    }
}
