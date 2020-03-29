using System;
using System.Collections.Generic;
using NumSharp;

namespace KSR.Tools.Metrics
{
    public class CanBerraMetric : IMetric
    {
        public double GetDistance(NDArray lhs, NDArray rhs)
        {
            return np.sum(np.divide(np.abs(np.subtract(lhs, rhs)), np.add(np.abs(lhs), np.abs(rhs))));
        }

        public double GetDistance(double[] lhs, double[] rhs)
        {
            var result = 0d;
            for (var i = 0; i < lhs.Length; i++)
            {
                result += (Math.Abs(lhs[i] - rhs[i]) / (lhs[i] + rhs[i]));
            }

            return result;
        }

        public override string ToString()
        {
            return "CanberraMetric";
        }
    }
}