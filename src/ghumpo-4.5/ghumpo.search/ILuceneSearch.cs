using System;
using System.Collections.Generic;

namespace ghumpo.search
{
    public interface ILuceneSearch<T> : IDisposable
    {
        void AddIndex(T listing);
        void RebuildIndex(IList<T> listings);
        void BuildIndex(IList<T> listings);
        void DeleteIndex(string id);
    }
}