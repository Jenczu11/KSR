using System;
using System.Collections.Generic;
using SDC.Model;
using SDC.Tools.Metrics;

namespace SDC.Tools.Classifier
{
    public interface IClassifier
    {
        void Train(List<Article> reference);
        string Classify(Article article, int k, IMetric metric);
    }
}
