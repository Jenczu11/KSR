using System;
using NumSharp;

namespace KSR.Tools.Metrics
{
    public class EuclidesMetric : IMetric
    {
        public decimal GetDistance(NDArray lhs, NDArray rhs)
        {
            return np.sqrt(np.power(np.subtract(lhs, rhs), 2).sum());
        }
    }
}
