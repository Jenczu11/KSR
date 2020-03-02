using System;
using System.Collections.Generic;
using KSR.Model;

namespace KSR.Tools.Readers
{
    public interface IReader
    {
        public IEnumerable<Article> GetArticles();
    }
}
