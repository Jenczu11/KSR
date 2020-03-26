using System;
using System.Collections.Generic;
using KSR.Model;
using KSR.Tools.Metrics;
using SharpLearning;
using SharpLearning.DecisionTrees.Learners;

namespace KSR.Tools.Classifier
{
    public class DecisionTreeClassifier : IClassifier
    {
        ClassificationDecisionTreeLearner learner = new ClassificationDecisionTreeLearner();

        public DecisionTreeClassifier()
        {

        }
        public void Train(List<Article> reference)
        {
        }
        public string Classify(Article article, int k, IMetric metric)
        {
            return null;
        }
    }
}
