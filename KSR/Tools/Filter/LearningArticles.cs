using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using KSR.Model;

namespace KSR.Tools.Filter
{
    public class LearningArticles
    {
        private List<Article> learningArticles { get; set; }
        private int percentage;
        public int Count { get; set; }
        public LearningArticles(int percentage,FilteredArticles articles)
        {
            learningArticles = new List<Article>();
            int temp = (int)Math.Floor(articles.Count()*(percentage/100.0));
            for (int i = 0; i < temp; i++)
            {
                learningArticles.Add(articles.getArticle(i));
            }

            Count = temp;
        }

        public void PrintNumberOfArticles()
        {
            Console.WriteLine(string.Format("Number of learning articles: {0}",Count));
        }
        
        
    }
}