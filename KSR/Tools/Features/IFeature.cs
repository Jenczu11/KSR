using System;
using System.Collections.Generic;
using KSR.Model;
using KSR.Tools.SimliarityFunctions;
using NumSharp;
namespace KSR.Tools.Features
{
    public interface IFeature
    {
        bool IsContinouse { get; set; }
        ISimilarityFunction SimilarityFunction { get; set; }
        double Calc(Article article, List<string> keyWords);
        
    }
}
