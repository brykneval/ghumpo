using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using ghumpo.model;
using ghumpo.model.Mobile;
using ghumpo.search;

namespace ghumpo.service.Query
{
    public interface ISuggestionsQueryService
    {
        IList<SuggestionMobile> Suggestions();

        IList<LocationSuggestionMobile> LocationSuggestions(string query, string filter_city, string lat,
            string lon,
            string sort, string type, string offset);
    }

    public class SuggestionsQueryService : ISuggestionsQueryService
    {
        private readonly IDbConnection cnn;
        private readonly ILuceneSearchRestaurant<SearchListingMapper, RestaurantListingMobile> search;

        public SuggestionsQueryService(IDbConnection cnn)
        {
            search = LuceneSearch.RestaurantSearch();
            this.cnn = cnn;
        }

        public IList<SuggestionMobile> Suggestions()
        {
            var query = @"SELECT slm.RestaurantName   AS [name],
                               CAST(slm.FkRestaurantId AS VARCHAR(36)) AS [id],
                               CASE 
                                    WHEN slm.AddressRoute IS NULL THEN ''
                                    WHEN LEN(slm.AddressRoute) = 0 THEN ''
                                    ELSE slm.AddressRoute + ' '
                               END 
                               + CASE 
                                      WHEN slm.AddressSubLocality IS NULL THEN ''
                                      WHEN LEN(slm.AddressSubLocality) = 0 THEN ''
                                      ELSE slm.AddressSubLocality + ' '
                                 END 
                               + CASE 
                                      WHEN slm.AddressLocality IS NULL THEN ''
                                      WHEN LEN(slm.AddressLocality) = 0 THEN ''
                                      ELSE slm.AddressLocality
                                 END                AS [address],
                               ISNULL(slm.AddressSubLocality, '') AS [locality]
                        FROM   SearchListingMapper     slm";
            var suggestions = cnn.Query<SuggestionMobile>(query, null, commandType: CommandType.Text);
            return suggestions.ToList();
        }

        public IList<LocationSuggestionMobile> LocationSuggestions(string query, string filter_city, string lat,
            string lon,
            string sort, string type, string offset)
        {
            var restaurants = search.SearchLocationIndex(query, filter_city, lat, lon, sort, type, offset);
            return restaurants;
        }
    }
}