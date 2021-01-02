using Annytab.Stemmer;
using KSR.Model;
using KSR.Tools.Helpers;
using StopWord;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
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
            Assembly executingAssembly = Assembly.GetAssembly(typeof(ServiceThread));
            string targetDir = executingAssembly.Location;
            Configuration config = ConfigurationManager.OpenExeConfiguration(targetDir);
            var connectionString = config.ConnectionStrings.ConnectionStrings["KSR.Properties.Settings.ConnectionString"].ConnectionString.ToString();
            connection = new SqlConnection(connectionString);



        }

        public IEnumerable<Article> GetArticles(bool stemmization, bool stoplist)
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
                    if (counter % 100 == 0)
                    {
                        BaseLogs.WriteLog(string.Format("Articles readed {0}", counter));
                    }
#endif
                    var article = new Article();
                    article.Label = reader["LEVELID"].ToString();
                    article.Title = reader["TITLE"].ToString();
                    article.Tags = new Dictionary<string, List<string>>();
                    article.Tags.Add("level", new List<string>() { reader["LEVELID"].ToString() });

                    var temp1 = stemmization ? regex.Replace(
                        Encoding.ASCII.GetString(
                            Encoding.GetEncoding("Cyrillic").GetBytes(reader["DESCRIPTION"].ToString())
                        ),
                    " ") : reader["DESCRIPTION"].ToString();
                    temp1 = temp1.ToLower();
                    var temp2 = temp1.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    var temp3 = temp2.Where(item => item.Length > 2);
                    if (stoplist)
                    {
                        temp3 = temp3.Where(item => StopListHelper.StopWord(item));
                    }
                    temp3 = temp3.Where(item => item.Length > 3);
                    article.AllWords = temp3.ToArray();
                    article.Paragraphs = new List<List<string>>();
                    article.Paragraphs.Add(temp3.ToList());
                    article.HasAttachment = reader["HASATTACHMENT"].ToString() == "1";
                    result.Add(article);
                    counter++;
                }
            }
            connection.Close();

            return result;
        }
    }
}
