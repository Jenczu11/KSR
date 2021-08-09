using System;
using System.Collections.Generic;
using SDC.Model;
using SDC.Tools.Metrics;
using SharpLearning;
using SharpLearning.DecisionTrees.Learners;

namespace SDC.Tools.Classifier
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
