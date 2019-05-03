using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ghumpo.common;
using ghumpo.data.Infrastructure;

namespace ghumpo.data
{
    public class Repository<T> where T : class
    {
        protected readonly DbSet<T> DbSet;
        private GhumpoContext _dataContext;

        public Repository(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            DbSet = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory { get; set; }

        public GhumpoContext DataContext
        {
            get { return _dataContext ?? (_dataContext = DatabaseFactory.Get()); }
        }

        #region IRepository<T> Members

        public EnumHelper.EOpStatus Insert(T entity)
        {
            try
            {
                DbSet.Add(entity);
            }
            catch (Exception ex)
            {
            }
            return EnumHelper.EOpStatus.Success;
        }

        public EnumHelper.EOpStatus Delete(T entity)
        {
            DbSet.Remove(entity);
            return EnumHelper.EOpStatus.Success;
        }

        public EnumHelper.EOpStatus Delete(Expression<Func<T, bool>> predicate)
        {
            DbSet.Remove(GetByPredicate(predicate));
            return EnumHelper.EOpStatus.Success;
        }

        public EnumHelper.EOpStatus Update(T entity)
        {
            DbSet.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
            return EnumHelper.EOpStatus.Success;
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public T GetById(long id)
        {
            return default(T);
        }

        public T GetByPredicate(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).FirstOrDefault();
        }

        public IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return null; //_dataContext.Database.SqlQuery<T>(query, parameters);
        }

        public async Task<EnumHelper.EOpStatus> SaveAsync()
        {
            await DataContext.SaveChangesAsync();
            return EnumHelper.EOpStatus.Success;
        }

        public EnumHelper.EOpStatus Save()
        {
            DataContext.SaveChanges();
            return EnumHelper.EOpStatus.Success;
        }

        #endregion
    }
}