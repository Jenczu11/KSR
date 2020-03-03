using System;
using System.Collections.Generic;

namespace KSR.Tools.Frequency
{
    public interface IFrequency
    {
        public static Dictionary<string, decimal> Calc(List<string> words);
    }
}
