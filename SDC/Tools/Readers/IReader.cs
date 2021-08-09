using System;
using System.Collections.Generic;
using SDC.Model;

namespace SDC.Tools.Readers
{
    public interface IReader
    {
        IEnumerable<Article> GetArticles(bool stemmization, bool stoplist);
    }
}
