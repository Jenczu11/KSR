using System;
using KSR.Tools.Readers;
using System.Linq;
using KSR.Model;
using System.Collections.Generic;

namespace KSR
{
    class Program
    {
        public static List<string> PLACES = new List<string>() { "west-germany", "usa", "france", "uk", "canada", "japan" };
        public const string PLACES_TAG = "places";

        public static void Main(string[] args)
        {

            var reader = new ReutersReader();
            var articles = reader.GetArticles();
            var selected = articles
                .Where(item => item.Tags.ContainsKey(PLACES_TAG))
                .Where(item => item.Tags[PLACES_TAG].Count == 1)
                .Where(item => PLACES.Contains(item.Tags[PLACES_TAG][0]))
                .ToList();
            Console.WriteLine("Po filtrowaniu"); 
            selected[0].Paragraphs[0].ForEach(word => Console.WriteLine(word));
            Console.ReadLine();
            // selected.ForEach(item => Console.WriteLine(string.Format("Title: {0}, DateLine: {1}, Place: {2}", item.Title, item.DateLine, item.Tags[PLACES_TAG][0])));
        }
    }
}
