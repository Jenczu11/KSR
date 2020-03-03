using System;
using NumSharp;

namespace KSR.Tools.Metrics
{
    public class ManhattanMetric : IMetric
    {
        public decimal GetDistance(NDArray lhs, NDArray rhs)
        {
            return np.sum(np.abs(np.subtract(lhs, rhs)));
        }
    }
}
