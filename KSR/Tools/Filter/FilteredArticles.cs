using System;
using System.Collections.Generic;
using System.Linq;
using KSR.Model;

namespace KSR.Tools.Filter
{

    public class FilteredArticles
    {
        public static List<string> PLACES = new List<string>()
            {"west-germany", "usa", "france", "uk", "canada", "japan"};

        public const string PLACES_TAG = "places";
        public List<Article> selectedArticles { get; set; }

        public FilteredArticles(IEnumerable<Article> articles)
        {
            selectedArticles = articles
                .Where(item => item.Tags.ContainsKey(PLACES_TAG))
                .Where(item => item.Tags[PLACES_TAG].Count == 1)
                .Where(item => PLACES.Contains(item.Tags[PLACES_TAG][0]))
                .ToList();
            selectedArticles.ForEach(item => item.Label = item.Tags[PLACES_TAG][0]);
        }

        public FilteredArticles()
        {
            selectedArticles = new List<Article>();
        }

        public Article getArticle(int index)
        {
            return selectedArticles[index];
        }

        public List<List<string>> GetArticleManyParagraphs(int articleIndex)
        {
            return selectedArticles[articleIndex].Paragraphs;
        }

        public List<string> GetArticleSingleParagraph(int articleIndex, int paragraphIndex)
        {
            return selectedArticles[articleIndex].Paragraphs[paragraphIndex];
        }

        public int Count()
        {
            return selectedArticles.Count;
        }

        public void printArticle(int articleIndex)
        {
            selectedArticles[articleIndex].Paragraphs
                .ForEach(paragraph => paragraph.ForEach(word =>
                {
                    Console.Write(word);
                    Console.Write(" ");
                }));
        }

        public void printSingleParagraph(int articleIndex, int paragraphIndex)
        {
            selectedArticles[articleIndex].Paragraphs[paragraphIndex].ForEach(word =>
            {
                Console.Write(word);
                Console.Write(" ");
            });
        }

        public List<string> getListOfAllWords()
        {
            var listOfWords = new List<string>();
            selectedArticles.ForEach(a => a.Paragraphs.ForEach(p => p.ForEach(w => listOfWords.Add(w))));
            return listOfWords;
        }
    }
}