using System;
using NumSharp;

namespace KSR.Tools.Metrics
{
    public class ChebyshevMatric : IMetric
    {
        public double GetDistance(NDArray lhs, NDArray rhs)
        {
            return np.max(np.abs(np.subtract(lhs, rhs)));
        }

        public double GetDistance(double[] lhs, double[] rhs)
        {
            var result = 0d;
            for (var i = 0; i < lhs.Length; i++)
            {
                result = Math.Max(Math.Abs(lhs[i] - rhs[i]), result);
            }
            return result;
        }

        public override string ToString()
        {
            return "ChebyshevMatric";
        }
    }
}
