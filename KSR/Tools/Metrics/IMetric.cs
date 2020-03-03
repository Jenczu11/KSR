using System;
using System.Collections.Generic;
using NumSharp;

namespace KSR.Tools.Metrics
{
    public interface IMetric
    {
        public decimal GetDistance(NDArray lhs, NDArray rhs);
    }
}
