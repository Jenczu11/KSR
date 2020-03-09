using System;
using System.Collections.Generic;
using KSR.Model;
using KSR.Tools.SimliarityFunctions;
using NumSharp;
namespace KSR.Tools.Features
{
    public interface IFeature
    {
        decimal Calc(Article article, List<string> keyWords, ISimilarityFunction similarityFunction);
    }
}
