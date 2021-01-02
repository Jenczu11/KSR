using KSR.Tools.Classifier;
using KSR.Tools.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSR.Tools.Factories
{
    public class ClassifierFactory
    {
        public static IClassifier GetClassifier(ClassifiersEnum classifierEnum)
        {
            switch (classifierEnum)
            {
                case ClassifiersEnum.knn_classifier:
                    return new KNNClassifier();
                default:
                    throw new Exception("Unknown type");
            }
        }
    }
}
