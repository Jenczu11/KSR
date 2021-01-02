using KSR.Tools.Enums;
using KSR.Tools.Frequency;
using System;

namespace KSR.Tools.Factories
{
    public class FrequencyFactory
    {
        public static IFrequency GetFrequency(FrequencyEnum frequency)
        {
            switch (frequency)
            {
                case FrequencyEnum.document_frequency:
                    return new TDFrequency();
                case FrequencyEnum.term_frequency:
                    return new TFFrequency();
                default:
                    throw new Exception("Unsexpected value");
            }
        }
    }
}
