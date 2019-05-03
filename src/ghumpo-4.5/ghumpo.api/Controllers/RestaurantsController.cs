using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ghumpo.model;
using ghumpo.model.Mobile;
using ghumpo.service;
using ghumpo.service.Query;

namespace ghumpo.api.Controllers
{
    public class RestaurantsController : ApiController
    {
        private readonly IRestaurantCommandService restaurantCommandService;
        private readonly IRestaurantListingQueryService restaurantListingQueryService;

        public RestaurantsController(IRestaurantListingQueryService restaurantListingQueryService,
            IRestaurantCommandService restaurantCommandService)
        {
            this.restaurantListingQueryService = restaurantListingQueryService;
            this.restaurantCommandService = restaurantCommandService;
        }

        public FormatListApi<RestaurantListingMobile> Get(string query, string filter_city, string lat, string lon,
            string sort, string type, string offset)
        {
            var restaurants = restaurantListingQueryService.Resturants(query, filter_city, lat, lon, sort, type, offset);
            return new FormatListApi<RestaurantListingMobile> {message = "", data = restaurants};
        }

        public FormatListApi<RestaurantHomeMobile> Get(string query)
        {
            var restaurants = restaurantListingQueryService.HomeResturants(query);
            return new FormatListApi<RestaurantHomeMobile> {message = "", data = restaurants};
        }

        public FormatApi<RestaurantMobile> Get(string id, string restaurant_id)
        {
            var restaurant = restaurantListingQueryService.Restaurant(restaurant_id);
            return new FormatApi<RestaurantMobile> {message = "", data = restaurant};
        }

        [HttpPost]
        [ResponseType(typeof (HttpResponseMessage))]
        public async Task<HttpResponseMessage> Post(Restaurant oRestaurant)
        {
            if (!ModelState.IsValid)
            {
                var response =
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, "model state is invalid");
                throw new HttpResponseException(response);
            }
            await restaurantCommandService.CreateRestaurantAsync(oRestaurant);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}