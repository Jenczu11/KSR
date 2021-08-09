using SDC.Model;
using SDC.Tools.SimliarityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDC.Tools.Features
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
