using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Script.Serialization;
using Dapper;
using ghumpo.model;
using ghumpo.model.Mobile;
using ghumpo.search;

namespace ghumpo.service.Query
{
    public interface IRestaurantListingQueryService
    {
        IList<RestaurantListingMobile> Resturants(string query, string filter_city, string lat, string lon,
            string sort, string type, string offset);

        IList<RestaurantHomeMobile> HomeResturants(string query);
        RestaurantMobile Restaurant(string id);
    }

    public class RestaurantListingQueryService : IRestaurantListingQueryService
    {
        private readonly IDbConnection cnn;
        private readonly ILuceneSearchRestaurant<SearchListingMapper, RestaurantListingMobile> search;

        public RestaurantListingQueryService(IDbConnection cnn)
        {
            search = LuceneSearch.RestaurantSearch();
            this.cnn = cnn;
        }

        public IList<RestaurantListingMobile> Resturants(string query, string filter_city, string lat, string lon,
            string sort, string type, string offset)
        {
            var restaurants = search.SearchIndex(query, filter_city, lat, lon, sort, type, offset);
            return restaurants;
        }

        public RestaurantMobile Restaurant(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                id = new JavaScriptSerializer().Deserialize(id, null).ToString();
            }
            var restaurant = cnn.Query<RestaurantMobile>("spGetRestaurant", new {Id = id},
                commandType: CommandType.StoredProcedure).FirstOrDefault();
            return restaurant;
        }

        public IList<RestaurantHomeMobile> HomeResturants(string query)
        {
            var restaurants = search.SearchRestaurantHomeIndex(query);
            return restaurants;
        }
    }
}