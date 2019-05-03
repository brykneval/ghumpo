using System;
using System.Threading.Tasks;

namespace ghumpo.web.Core
{
    public interface IWebApiCore<T, T1>
    {
        Task<Tuple<bool, string>> PostAsync(T obj);
        Task<T> GetAsync(T1 id);
        Task<bool> PutAsync(T obj);
    }
}