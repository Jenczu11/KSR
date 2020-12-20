using Annytab.Stemmer;
using KSR.Model;
using KSR.Tools.Helpers;
using StopWord;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KSR.Tools.Readers
{
    public class DatabaseReader : IReader
    {

        private static Stemmer stemmer = new EnglishStemmer();
        private SqlConnection connection { get; set; }

        public DatabaseReader()
        {
            connection = new SqlConnection("Data Source=;Initial Catalog=;Persist Security Info=True;User ID=;Password=");


            
        }

        public IEnumerable<Article> GetArticles()
        {
            var regex = new Regex("[^a-zA-Z]");
            var result = new List<Article>();
            connection.Open();
            SqlCommand command = new SqlCommand("Select TITLE, DESCRIPTION, LEVELID, HASATTACHMENT from [WorkOrderV]", connection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                var counter = 0;
                while (reader.Read())
                {
#if DEBUG
                    if(counter % 100 == 0)
                    {
                        Console.WriteLine(string.Format("Articles readed {0}", counter));
                    }
#endif
                    var article = new Article();
                    article.Label = reader["LEVELID"].ToString();
                    article.Title = reader["TITLE"].ToString();
                    article.Tags = new Dictionary<string, List<string>>();
                    article.Tags.Add("level", new List<string>() { reader["LEVELID"].ToString() });
                    
                    var temp1 = regex.Replace(
                        Encoding.ASCII.GetString(
                            Encoding.GetEncoding("Cyrillic").GetBytes(reader["DESCRIPTION"].ToString())
                        ),
                    " ");
                    temp1 = temp1.ToLower();
                    if (Settings.stopListLib)
                    {
                        //temp1 = temp1.RemoveStopWords("en"); //Usign https://github.com/hklemp/dotnet-stop-words
                    }
                    var temp2 = temp1.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    var temp3 = temp2.Where(item => item.Length > 2);
                    temp3 = temp3.Where(item => StopListHelper.StopWord(item));
                    if (Settings.stemmization)
                    {
                        //temp3 = temp3.Select(item => stemmer.GetSteamWord(item)); //Using https://github.com/annytab/a-stemmer
                    }
                    temp3 = temp3.Where(item => item.Length > 3);
                    article.AllWords = temp3.ToArray();
                    article.Paragraphs = new List<List<string>>();
                    article.Paragraphs.Add(temp3.ToList());
                    result.Add(article);
                    counter++;
                }
            }
            connection.Close();

            return result;
        }
    }
}
