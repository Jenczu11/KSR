using System;
using System.Collections.Generic;
using NumSharp;

namespace SDC.Tools.Metrics
{
    public class ManhattanMetric : IMetric
    {
        public double GetDistance(NDArray lhs, NDArray rhs)
        {
            return np.sum(np.abs(np.subtract(lhs, rhs)));
        }

        public double GetDistance(double[] lhs, double[] rhs)
        {
            var result = 0d;
            for (var i = 0; i < lhs.Length; i++)
            {
                result += Math.Abs(lhs[i] - rhs[i]);
            }
            return result;
        }
        public override string ToString()
        {
            return "ManhattanMetric";
        }
    }
}
