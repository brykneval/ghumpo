using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using ghumpo.model;
using ghumpo.search;

namespace ghumpo.service.Query
{
    public interface ISearchListingMapperQueryService
    {
        IList<SearchListingMapper> SearchListingsMappers();
        void RebuildIndex();
    }

    public class SearchListingMapperQueryService : ISearchListingMapperQueryService
    {
        private readonly IDbConnection cnn;

        public SearchListingMapperQueryService(IDbConnection cnn)
        {
            this.cnn = cnn;
        }

        public IList<SearchListingMapper> SearchListingsMappers()
        {
            var searchListings = cnn.Query<SearchListingMapper>("SELECT * FROM SearchListingMapper", null,
                commandType: CommandType.Text);
            return searchListings.ToList();
        }

        public void RebuildIndex()
        {
            var luceneSearch = LuceneSearch.RestaurantSearch();
            luceneSearch.RebuildIndex(SearchListingsMappers());
        }
    }
}