using System;
using System.Collections.Generic;
using KSR.Model;

namespace KSR.Tools.Frequency
{
    public interface IFrequency
    {
        Dictionary<string, double> Calc(List<Article> articles);
    }
}
