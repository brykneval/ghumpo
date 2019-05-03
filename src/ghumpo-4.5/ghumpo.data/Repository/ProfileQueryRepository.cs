using ghumpo.data.Infrastructure;
using ghumpo.model;

namespace ghumpo.data.Repository
{
    public class ProfileQueryRepository : Repository<Profile>, IProfileQueryRepository
    {
        public ProfileQueryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IProfileQueryRepository : IRepositoryQuery<Profile>
    {
    }
}