using SDC.Tools.Enums;
using SDC.Tools.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDC.Tools.Factories
{
    public class MetricsFactory
    {
        public static IMetric GetMetric(MetricsEnum metrics)
        {
            switch(metrics)
            {
                case MetricsEnum.canberry:
                    return new CanBerraMetric();
                case MetricsEnum.chebyshev:
                    return new ChebyshevMatric();
                case MetricsEnum.euclides:
                    return new EuclidesMetric();
                case MetricsEnum.hamming:
                    return new HammingMetric();
                case MetricsEnum.manhattan:
                    return new ManhattanMetric();
                case MetricsEnum.minkovski:
                    return new MinkowskiMetric();
                default:
                    throw new Exception("Unexpected type");
            }
        }
    }
}
