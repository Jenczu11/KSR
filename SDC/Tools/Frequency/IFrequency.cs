using System;
using System.Collections.Generic;
using SDC.Model;

namespace SDC.Tools.Frequency
{
    public interface IFrequency
    {
        Dictionary<string, double> Calc(List<Article> articles);
    }
}
