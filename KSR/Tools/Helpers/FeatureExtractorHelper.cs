using System;
using System.Collections.Generic;
using KSR.Model;
using KSR.Tools.Features;
using NumSharp;

namespace KSR.Tools.Helpers
{
    public class FeatureExtractorHelper
    {
        public static void ExtractFeature(List<IFeature> features, ref Article article, List<string> keyWords)
        {
            article.Features = np.zeros(features.Count);
            int count = 0;
            foreach (var feature in features)
            {
                article.Features[count] = feature.Calc(article, keyWords);
                count++;
            }
            var max = np.max(article.Features);
            article.Features = np.true_divide(article.Features, max);
        }
    }
}
