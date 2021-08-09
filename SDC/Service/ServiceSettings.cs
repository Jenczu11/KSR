using SDC.Tools.Enums;
using System.Collections.Generic;

namespace SDC
{
    public class ServiceSettings
    {
        public ClassifiersEnum classifiers { get; set; }
        public FrequencyEnum frequency { get; set; }
        public MetricsEnum metrics { get; set; }
        public PartsEnum parts { get; set; }
        public bool onwSets { get; set; }
        public bool stoplist { get; set; }
        public bool stemmize { get; set; }
        public bool normalization { get; set; }
        public HashSet<FeaturesEnum> features { get; set; }
        public int k { get; set; }
        public string stoplistPath { get; set; }
    }
}
