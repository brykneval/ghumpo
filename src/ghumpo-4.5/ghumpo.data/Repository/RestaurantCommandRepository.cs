using ghumpo.data.Infrastructure;
using ghumpo.model;

namespace ghumpo.data.Repository
{
    public class RestaurantCommandRepository : Repository<Restaurant>, IRestaurantCommandRepository
    {
        public RestaurantCommandRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IRestaurantCommandRepository : IRepositoryCommand<Restaurant>
    {
    }
}