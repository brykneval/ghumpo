using ghumpo.data.Infrastructure;
using ghumpo.model;

namespace ghumpo.data.Repository
{
    public class LocalBusinessCommandRepository : Repository<LocalBusiness>, ILocalBusinessCommandRepository
    {
        public LocalBusinessCommandRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface ILocalBusinessCommandRepository : IRepositoryCommand<LocalBusiness>
    {
    }
}