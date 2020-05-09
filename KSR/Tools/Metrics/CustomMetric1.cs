using System;
using Newtonsoft.Json.Schema;
using NumSharp;

namespace KSR.Tools.Metrics
{
    public class CustomMetric1 : IMetric
    {

        public double GetDistance(NDArray lhs, NDArray rhs)
        {
            return np.sqrt(np.power(np.add(lhs, rhs), 2).sum());
        }

        public double GetDistance(double[] lhs, double[] rhs)
        {
            var result = 0d;
            for (var i = 0; i < lhs.Length; i++)
            {
                
                result += Math.Pow(lhs[i] + rhs[i], 2);
            }
            return Math.Sqrt(result);
        }

        public override string ToString()
        {
            return "CustomMetric1 -> sqrt(sigma(a+b)^2)";
        }
    }
}