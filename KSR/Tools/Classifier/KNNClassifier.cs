using System;
using System.Collections.Generic;
using System.Linq;
using KSR.Model;
using KSR.Tools.Metrics;

namespace KSR.Tools.Classifier
{
    public class KNNClassifier : IClassifier
    {
        public string Classify(List<Article> reference, Article article, int k, IMetric metric)
        {
            List<Article> knn = reference.OrderBy(item => metric.GetDistance(item.Features, article.Features)).ToList();
            return knn.Take(k).GroupBy(item => item.Label).OrderBy(item => item.Count()).Last().Key;
        }
    }
}
