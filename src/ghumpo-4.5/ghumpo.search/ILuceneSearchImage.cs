using System.Collections.Generic;

namespace ghumpo.search
{
    public interface ILuceneSearchImage<T, T1> : ILuceneSearch<T>
    {
        IList<T1> SearchIndex(string searchQuery);
    }
}