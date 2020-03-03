using System;
using System.Collections.Generic;

namespace KSR.Tools.Frequency
{
    public interface IFrequency
    {
        public Dictionary<string, decimal> Calc(List<string> words);
    }
}
