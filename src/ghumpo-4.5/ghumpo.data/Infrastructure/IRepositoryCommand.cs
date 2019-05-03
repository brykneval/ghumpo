using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ghumpo.common;

namespace ghumpo.data.Infrastructure
{
    public interface IRepositoryCommand<T>
    {
        EnumHelper.EOpStatus Insert(T entity);
        EnumHelper.EOpStatus Delete(T entity);
        EnumHelper.EOpStatus Delete(Expression<Func<T, bool>> predicate);
        EnumHelper.EOpStatus Update(T entity);
        IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters);
        Task<EnumHelper.EOpStatus> SaveAsync();
        EnumHelper.EOpStatus Save();
    }
}