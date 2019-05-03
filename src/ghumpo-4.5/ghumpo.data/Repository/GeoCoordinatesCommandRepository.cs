using ghumpo.data.Infrastructure;
using ghumpo.model;

namespace ghumpo.data.Repository
{
    public class GeoCoordinatesCommandRepository : Repository<GeoCoordinates>, IGeoCoordinatesCommandRepository
    {
        public GeoCoordinatesCommandRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IGeoCoordinatesCommandRepository : IRepositoryCommand<GeoCoordinates>
    {
    }
}