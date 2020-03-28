using System;
using NumSharp;

namespace KSR.Tools.Metrics
{
    public class EuclidesMetric : IMetric
    {
        public double GetDistance(NDArray lhs, NDArray rhs)
        {
            return np.sqrt(np.power(np.subtract(lhs, rhs), 2).sum());
        }

        public double GetDistance(double[] lhs, double[] rhs)
        {
            var result = 0d;
            for (var i = 0; i < lhs.Length; i++)
            {
                result += Math.Pow(lhs[i] - rhs[i], 2);
            }
            return Math.Sqrt(result);
        }

        public override string ToString()
        {
            return "EuclidesMetric";
        }
    }
}
