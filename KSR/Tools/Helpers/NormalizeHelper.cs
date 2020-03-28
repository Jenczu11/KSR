using KSR.Model;
using KSR.Tools.Features;
using KSR.Tools.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSR.Tools.Helpers
{
    public class NormalizeHelper
    {
        public static void NormalizeVertical(ref LearningArticles learning, ref TestingArticles testing)
        {
            var count = learning.articles[0].FeaturesD.Count();
            var max = 0.0d;
            for (var i = 0; i < count; i++)
            {
                Console.WriteLine(string.Format("Normalize value at index {0}", i));
                foreach (var item in learning.articles)
                {
                    max = Math.Max(max, item.FeaturesD[i]);
                }
                foreach (var item in testing.articles)
                {
                    max = Math.Max(max, item.FeaturesD[i]);
                }
                if (max != 0.0d)
                {
                    foreach (var item in learning.articles)
                    {
                        item.FeaturesD[i] = item.FeaturesD[i] * 100.0 / max;
                    }
                    foreach (var item in testing.articles)
                    {
                        item.FeaturesD[i] = item.FeaturesD[i] * 100.0 / max;
                    }
                }
            }
        }
    }
}
