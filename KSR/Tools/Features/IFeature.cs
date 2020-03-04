using System;
using System.Collections.Generic;
using KSR.Model;
using NumSharp;
namespace KSR.Tools.Features
{
    public interface IFeature
    {
        public decimal Calc(Article article, List<string> keyWords);
    }
}
