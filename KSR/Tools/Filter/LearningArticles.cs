using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using KSR.Model;

namespace KSR.Tools.Filter
{
    public class LearningArticles
    {
        public List<Article> articles { get; set; } = new List<Article>();
        public int Count { get { return articles.Count(); } }
        public LearningArticles(int percentage, List<Article> articles)
        {
            var temp = Convert.ToInt32(Math.Floor(articles.Count() * (percentage / 100.0)));
            this.articles = articles.Take(temp).ToList();
        }

        public void PrintNumberOfArticles()
        {
            Console.WriteLine(string.Format("Number of learning articles: {0}", Count));
        }



    }
}