using System;
using System.Collections.Generic;
using SDC.Model;
using SDC.Tools.SimliarityFunctions;
using NumSharp;
namespace SDC.Tools.Features
{
    public interface IFeature
    {
        bool IsContinouse { get; set; }
        ISimilarityFunction SimilarityFunction { get; set; }
        double Calc(Article article, List<string> keyWords);
        
    }
}
