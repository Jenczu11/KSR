using System;
using Newtonsoft.Json.Schema;
using NumSharp;

namespace KSR.Tools.Metrics
{
    public class CustomMetric2 : IMetric
    {
        public double GetDistance(NDArray lhs, NDArray rhs)
        {
            return np.sqrt(np.subtract(np.power(lhs, 2),np.power(rhs, 2)).sum());
        }

        public double GetDistance(double[] lhs, double[] rhs)
        {
            var result = 0d;
            for (var i = 0; i < lhs.Length; i++)
            {
                
                result += (Math.Pow(lhs[i],2) - Math.Pow(rhs[i], 2));
            }
            return Math.Sqrt(result);
        }

        public override string ToString()
        {
            return "CustomMetric2 -> sqrt(sigma(a^2-b^2))";
        }
    }
}