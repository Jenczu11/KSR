using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumSharp;

namespace KSR.Tools.Metrics
{
    public class MinkowskiMetric : IMetric
    {
        public int Power { get; set; } = 3;
        public double GetDistance(NDArray lhs, NDArray rhs)
        {
            throw new NotImplementedException();
        }

        public double GetDistance(double[] lhs, double[] rhs)
        {
            var result = 0d;
            for (var i = 0; i < lhs.Length; i++)
            {
                result += Math.Pow(Math.Abs(lhs[i] - rhs[i]), Power);
            }
            return Math.Pow(result, 1 / Convert.ToDouble(Power));
        }
        public override string ToString()
        {
            return "MinkowskiMetric";
        }
    }
}
