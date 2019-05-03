using ghumpo.data.Infrastructure;
using ghumpo.model;

namespace ghumpo.data.Repository
{
    public class ProfileCommandRepository : Repository<Profile>, IProfileCommandRepository
    {
        public ProfileCommandRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IProfileCommandRepository : IRepositoryCommand<Profile>
    {
    }
}