using System;
using System.Collections.Generic;
using KSR.Model;

namespace KSR.Tools.Readers
{
    public interface IReader
    {
        IEnumerable<Article> GetArticles(bool stemmization, bool stoplist);
    }
}
