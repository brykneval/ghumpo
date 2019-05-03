using ghumpo.data.Infrastructure;
using ghumpo.model;

namespace ghumpo.data.Repository
{
    public class ProfileStatsCommandRepository : Repository<ProfileStats>, IProfileStatsCommandRepository
    {
        public ProfileStatsCommandRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IProfileStatsCommandRepository : IRepositoryCommand<ProfileStats>
    {
    }
}