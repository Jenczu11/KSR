using System;
using System.Collections.Generic;
using System.Linq;
using KSR.Model;

namespace KSR.Tools.Filter
{
    public class TestingArticles
    {
        private FilteredArticles testingArticles { get; set; }
        private int percentage;
        public int Count { get; set; }
        public TestingArticles(int percentage, FilteredArticles articles)
        {
            testingArticles = new FilteredArticles();
            int temp = (int)Math.Floor(articles.Count() * (1 - (percentage / 100.0)));
            testingArticles.selectedArticles = articles.selectedArticles.Skip(temp).Take(articles.Count() - temp).ToList();
            /* for (int i = temp; i < articles.Count(); i++)
             {
                 testingArticles.selectedArticles.Add(articles.getArticle(i));
             }*/

            Count = articles.Count() - temp;
        }
        public void PrintNumberOfArticles()
        {
            Console.WriteLine(string.Format("Number of testing articles: {0}", Count));
        }
    }
}