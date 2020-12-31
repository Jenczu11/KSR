using KSR.Model;
using KSR.Tools.SimliarityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSR.Tools.Features
{
    public class ArticleHasAttachment : IFeature
    {
        public bool IsContinouse { get; set; } = false;
        public ISimilarityFunction SimilarityFunction { get; set; }

        public double Calc(Article article, List<string> keyWords)
        {
            return article.HasAttachment ? 1 : 0;
        }

        public override string ToString()
        {
            return "ArticleHasAttchement";
        }
    }
}
