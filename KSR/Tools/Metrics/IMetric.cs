using System;
using System.Collections.Generic;
using NumSharp;

namespace KSR.Tools.Metrics
{
    public interface IMetric
    {
        decimal GetDistance(NDArray lhs, NDArray rhs);
    }
}
