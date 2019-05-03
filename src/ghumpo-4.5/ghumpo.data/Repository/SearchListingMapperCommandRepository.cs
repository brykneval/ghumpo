using ghumpo.data.Infrastructure;
using ghumpo.model;

namespace ghumpo.data.Repository
{
    public class SearchListingMapperCommandRepository : Repository<SearchListingMapper>,
        ISearchListingMapperCommandRepository
    {
        public SearchListingMapperCommandRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface ISearchListingMapperCommandRepository : IRepositoryCommand<SearchListingMapper>
    {
    }
}