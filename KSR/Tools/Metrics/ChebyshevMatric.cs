using System;
using NumSharp;

namespace KSR.Tools.Metrics
{
    public class ChebyshevMatric : IMetric
    {
        public decimal GetDistance(NDArray lhs, NDArray rhs)
        {
            return np.max(np.abs(np.subtract(lhs, rhs)));
        }
    }
}
