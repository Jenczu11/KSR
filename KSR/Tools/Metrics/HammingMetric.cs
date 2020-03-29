using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumSharp;

namespace KSR.Tools.Metrics
{
    class HammingMetric : IMetric
    {
        public double GetDistance(NDArray lhs, NDArray rhs)
        {
            throw new NotImplementedException();
        }

        public double GetDistance(double[] lhs, double[] rhs)
        {
            var result = 0d;
            for (var i = 0; i < lhs.Length; i++)
            {
                result += (Math.Abs(lhs[i] - rhs[i]) > 0.001 ? 1 : 0);
            }

            return result;
        }

        public override string ToString()
        {
            return "HammingMetric";
        }
    }
}
