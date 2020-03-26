using System;
using System.Collections.Generic;
using KSR.Model;
using KSR.Tools.Metrics;

namespace KSR.Tools.Classifier
{
    public interface IClassifier
    {
        void Train(List<Article> reference);
        string Classify(Article article, int k, IMetric metric);
    }
}
