using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using ghumpo.model;
using ghumpo.web.Core;
using ghumpo.web.ViewModel;
using Nelibur.ObjectMapper;

namespace ghumpo.web.Controllers
{
    public class RestaurantController : Controller
    {
        private const string URI = "api/restaurants";
        private readonly IWebApiCore<Restaurant, bool> webApiCore;

        public RestaurantController()
        {
            webApiCore = new WebApiCoreRestSharp<Restaurant, bool>(URI);
        }

        public ActionResult Create(Guid id)
        {
            var restaurantViewModel = new RestaurantViewModel();
            restaurantViewModel.Cuisine = "Multicuisine";
            return View(restaurantViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Guid id, RestaurantViewModel oRestaurantViewModel)
        {
            var restaurantId = Guid.NewGuid();
            try
            {
                TinyMapper.Bind<RestaurantViewModel, Restaurant>();
                var restaurant = TinyMapper.Map<Restaurant>(oRestaurantViewModel);
                restaurant.FkLocalBusinessId = id;
                restaurant.RestaurantId = restaurantId;
                restaurant.GeoCoordinates.CreatedBy = id;
                restaurant.CreatedBy = id;
                await webApiCore.PostAsync(restaurant);

                return RedirectToAction("Create", "Photo", new {id = restaurantId});
            }
            catch
            {
                return View();
            }
        }
    }
}