using System;
using System.Threading.Tasks;
using ghumpo.common;

namespace ghumpo.data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory _databaseFactory;
        private GhumpoContext _dataContext;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public GhumpoContext DataContext
        {
            get { return _dataContext ?? (_dataContext = _databaseFactory.Get()); }
        }

        public EnumHelper.EOpStatus Commit()
        {
            DataContext.SaveChanges();
            return EnumHelper.EOpStatus.Success;
        }

        public async Task<EnumHelper.EOpStatus> CommitAsync()
        {
            try
            {
                await DataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return EnumHelper.EOpStatus.Success;
        }
    }
}