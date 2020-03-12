using System;
using System.Collections.Generic;
using KSR.Model;
using KSR.Tools.Metrics;

namespace KSR.Tools.Classifier
{
    public interface IClassifier
    {
        string Classify(List<Article> reference, Article article, int k, IMetric metric);
    }
}
