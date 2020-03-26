using System;
using System.Collections.Generic;
using DecisionTree;
using KSR.Model;
using KSR.Tools.Metrics;
using Lang;

namespace KSR.Tools.Classifier
{
    public class DecisionTreeClasifier2:IClassifier
    {
        C45<DDataRecord> algorithm;
        public DecisionTreeClasifier2()
        {
        }

        public string Classify(Article article, int k, IMetric metric)
        {
            var rec = new DDataRecord();
            for (int i = 0; i < article.FeaturesD.Length; i++)
            {
                rec[string.Format("F{0}", i)] = article.FeaturesD[i].ToString();
            }
            //string[] feature_names = rec.FindFeatures();
            return algorithm.Predict(rec);
        }

        public void Train(List<Article> reference)
        {
            if (reference.Count > 0)
            {
                List<DDataRecord> records = LoadSample(reference);
                algorithm = new C45<DDataRecord>();
                for (var i = 0; i < reference[0].FeaturesD.Length; i++)
                {
                    algorithm.UpdateContinuousAttributes(records, string.Format("F{0}", i));
                }
                algorithm.Train(records);
            }
        }

        public List<DDataRecord> LoadSample(List<Article> reference)
        {
            var resutl = new List<DDataRecord>();
            foreach (var item in reference)
            {
                var rec = new DDataRecord();
                for (int i = 0; i < item.FeaturesD.Length; i++)
                {
                    rec[string.Format("F{0}", i)] = item.FeaturesD[i].ToString();
                }
                rec.Label = item.Label;
            }
            return resutl;
        }
    }
}
