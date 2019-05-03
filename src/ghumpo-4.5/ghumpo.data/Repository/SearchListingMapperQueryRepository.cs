using ghumpo.data.Infrastructure;
using ghumpo.model;

namespace ghumpo.data.Repository
{
    public class SearchListingMapperQueryRepository : Repository<SearchListingMapper>,
        ISearchListingMapperQueryRepository
    {
        public SearchListingMapperQueryRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface ISearchListingMapperQueryRepository : IRepositoryQuery<SearchListingMapper>
    {
    }
}