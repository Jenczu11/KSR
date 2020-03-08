using System;
using System.Collections.Generic;
using KSR.Model;
using KSR.Tools.Frequency;
using System.Linq;

namespace KSR.Tools.Helpers
{
    public class KeyWordsHelper
    {
        public static Dictionary<string, List<string>> GetKeyWords(List<Article> articles, decimal percentOfWords, IFrequency frequency, string category)
        {
            var wordsInTags = new Dictionary<string, List<Article>>();
            var result = new Dictionary<string, List<string>>();
            foreach (var article in articles)
            {
                var tag = article.Tags[category][0];
                if (!wordsInTags.ContainsKey(tag))
                {
                    wordsInTags.Add(tag, new List<Article>());
                }
                wordsInTags[tag].Add(article);
            }
            var keys = wordsInTags.Keys;

            foreach (var key in keys)
            {
                var wordsFrequency = frequency.Calc(wordsInTags[key]);
                result.Add(key, wordsFrequency.Keys.Take(Convert.ToInt32(wordsFrequency.Count * percentOfWords)).ToList());
            }
            return result;
        }
    }
}
