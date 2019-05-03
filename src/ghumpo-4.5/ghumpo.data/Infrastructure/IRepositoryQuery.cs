using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ghumpo.data.Infrastructure
{
    public interface IRepositoryQuery<T>
    {
        IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters);
        T GetByPredicate(Expression<Func<T, bool>> predicate);
    }
}