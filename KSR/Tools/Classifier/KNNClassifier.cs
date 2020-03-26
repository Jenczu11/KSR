using System;
using System.Collections.Generic;
using System.Linq;
using KSR.Model;
using KSR.Tools.Metrics;

namespace KSR.Tools.Classifier
{
    public class KNNClassifier : IClassifier
    {
        List<Article> reference = new List<Article>();
        public string Classify(Article article, int k, IMetric metric)
        {
            //var result = reference.Select(item => metric.GetDistance)
            var elements = new Dictionary<Article, double>();
            foreach (var item in reference)
            {
                elements.Add(item, metric.GetDistance(item.FeaturesD, article.FeaturesD));
            }
            var sorted = elements.OrderBy(item => item.Value).Take(k).ToDictionary(item => item.Key, item => item.Value);
            var min = sorted.ElementAt(0);
            var result = sorted.GroupBy(item => item.Key.Label).ToDictionary(item => item.Key, item => item.Count());
            var count = result.OrderBy(item => item.Value).Last().Value;
            var labels = (from item in result
                          where item.Value == count
                          select item).ToDictionary(item => item.Key, item => item.Value);
            if (labels.Count() > 1)
            {
                if (labels.ContainsKey(min.Key.Label))
                {
                    return min.Key.Label;
                }
                else
                {
                    return labels.ElementAt(0).Key;
                }

            }
            else
            {
                return labels.ElementAt(0).Key;
            }
        }

        public void Train(List<Article> reference)
        {
            this.reference = new List<Article>(reference);
        }
    }
}
