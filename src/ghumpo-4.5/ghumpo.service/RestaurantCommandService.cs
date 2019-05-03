using System;
using System.Threading.Tasks;
using ghumpo.common;
using ghumpo.data.Infrastructure;
using ghumpo.data.Repository;
using ghumpo.model;
using ghumpo.model.Mobile;
using ghumpo.search;

namespace ghumpo.service
{
    public interface IRestaurantCommandService
    {
        Task<EnumHelper.EOpStatus> CreateRestaurantAsync(Restaurant oRestaurant);
    }

    public class RestaurantCommandService : IRestaurantCommandService
    {
        private readonly IRestaurantCommandRepository _restaurantRepository;
        private readonly ILuceneSearchRestaurant<SearchListingMapper, RestaurantListingMobile> _search;
        private readonly ISearchListingMapperCommandRepository _searchListingMapperCommandRepository;
        private readonly ISearchListingMapperCommandService _searchListingMapperCommandService;
        private readonly IUnitOfWork _unitOfWork;

        public RestaurantCommandService(IRestaurantCommandRepository restaurantRepository,
            ISearchListingMapperCommandRepository searchListingMapperCommandRepository,
            ISearchListingMapperCommandService searchListingMapperCommandService,
            IUnitOfWork unitOfWork)
        {
            _restaurantRepository = restaurantRepository;
            _searchListingMapperCommandRepository = searchListingMapperCommandRepository;
            _searchListingMapperCommandService = searchListingMapperCommandService;
            _unitOfWork = unitOfWork;
            _search = LuceneSearch.RestaurantSearch();
        }

        public async Task<EnumHelper.EOpStatus> CreateRestaurantAsync(Restaurant oRestaurant)
        {
            oRestaurant.CreatedTs = DateTime.UtcNow;
            oRestaurant.Ip = CommonHelper.GetIpAddress();
            oRestaurant.EFlag = EnumHelper.EFlag.Active;
            oRestaurant.ModifiedBy = null;
            oRestaurant.ModifiedTs = null;
            oRestaurant.GeoCoordinates = InitializeGeoCoordinate(oRestaurant.GeoCoordinates);

            var oSearchListingMapper = _searchListingMapperCommandService.MapSearchListingRestaurant(oRestaurant);
            _searchListingMapperCommandRepository.Update(oSearchListingMapper);
            _restaurantRepository.Insert(oRestaurant);

            await
                Task.WhenAll(SaveAsync(), Task.Run(() => _search.AddIndex(oSearchListingMapper)));
            return EnumHelper.EOpStatus.Success;
        }

        private async Task<EnumHelper.EOpStatus> SaveAsync()
        {
            return await _unitOfWork.CommitAsync();
        }

        private GeoCoordinates InitializeGeoCoordinate(GeoCoordinates oGeoCoordinates)
        {
            oGeoCoordinates.CreatedTs = DateTime.UtcNow;
            oGeoCoordinates.Ip = CommonHelper.GetIpAddress();
            oGeoCoordinates.EFlag = EnumHelper.EFlag.Active;
            oGeoCoordinates.ModifiedBy = null;
            oGeoCoordinates.ModifiedTs = null;
            return oGeoCoordinates;
        }
    }
}