using KSR.Tools.Enums;
using KSR.Tools.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSR.Tools.Factories
{
    public class FeatureFactory
    {
        public static List<IFeature> GetFeatures(List<FeaturesEnum> features)
        {
            var result = new List<IFeature>();
            if (features.Contains(FeaturesEnum.feature_1))
            {
                result.Add(new BinaryArticleBodyFeature());
            }
            if (features.Contains(FeaturesEnum.feature_2))
            {
                result.Add(new KeyWordsArticleBodyFeature());
            }
            if (features.Contains(FeaturesEnum.feature_3))
            {
                result.Add(new KeyWordsFirstParagraphArticleBodyFeature());
            }
            if (features.Contains(FeaturesEnum.feature_4))
            {
                result.Add(new KeyWordsInNPercentArticleBodyFeature() { N = 10 });
                result.Add(new KeyWordsInNPercentArticleBodyFeature() { N = 20 });
                result.Add(new KeyWordsInNPercentArticleBodyFeature() { N = 50 });
            }
            if (features.Contains(FeaturesEnum.feature_5))
            {
                result.Add(new KeyWordsInNWordsArticleBodyFeature() { N = 10 });
                result.Add(new KeyWordsInNWordsArticleBodyFeature() { N = 20 });
                result.Add(new KeyWordsInNWordsArticleBodyFeature() { N = 50 });
            }
            if (features.Contains(FeaturesEnum.feature_6))
            {
                result.Add(new ArticleHasAttachment());
            }
            return result;
        }
    }
}
