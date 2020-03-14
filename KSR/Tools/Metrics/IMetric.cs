using System;
using System.Collections.Generic;
using NumSharp;

namespace KSR.Tools.Metrics
{
    public interface IMetric
    {
        double GetDistance(NDArray lhs, NDArray rhs);
        double GetDistance(double[] lhs, double[] rhs);
    }
}
