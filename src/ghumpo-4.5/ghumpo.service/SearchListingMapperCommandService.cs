using System;
using System.Threading.Tasks;
using ghumpo.common;
using ghumpo.data.Infrastructure;
using ghumpo.data.Repository;
using ghumpo.model;

namespace ghumpo.service
{
    public interface ISearchListingMapperCommandService
    {
        Task<EnumHelper.EOpStatus> AddSearchListingMapperAsync(SearchListingMapper oSearchListingMapper);
        SearchListingMapper MapSearchListingLocalBusiness(LocalBusiness oLocalBusiness);
        SearchListingMapper MapSearchListingRestaurant(Restaurant oRestaurant);
    }

    public class SearchListingMapperCommandService : ISearchListingMapperCommandService
    {
        private readonly ISearchListingMapperQueryRepository _searchListingMapperQueryRepository;
        private readonly ISearchListingMapperCommandRepository _searchListingMapperRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SearchListingMapperCommandService(ISearchListingMapperCommandRepository searchListingMapperRepository,
            ISearchListingMapperQueryRepository searchListingMapperQueryRepository, IUnitOfWork unitOfWork)
        {
            _searchListingMapperRepository = searchListingMapperRepository;
            _searchListingMapperQueryRepository = searchListingMapperQueryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EnumHelper.EOpStatus> AddSearchListingMapperAsync(SearchListingMapper oSearchListingMapper)
        {
            var searchListingMapper =
                _searchListingMapperQueryRepository.GetByPredicate(
                    x => x.FkLocalBusinessId == oSearchListingMapper.FkLocalBusinessId);
            if (searchListingMapper == null)
            {
                await CreateSearchListingMapperAsync(oSearchListingMapper);
                return EnumHelper.EOpStatus.Success;
            }
            searchListingMapper.RestaurantName = oSearchListingMapper.RestaurantName;
            searchListingMapper.Description = oSearchListingMapper.Description;
            searchListingMapper.HasReservation = oSearchListingMapper.HasReservation;
            searchListingMapper.HasCard = oSearchListingMapper.HasCard;
            searchListingMapper.HasParking = oSearchListingMapper.HasParking;
            searchListingMapper.HasAlcohol = oSearchListingMapper.HasAlcohol;
            searchListingMapper.HasSmoking = oSearchListingMapper.HasSmoking;
            searchListingMapper.HasWifi = oSearchListingMapper.HasWifi;
            searchListingMapper.Longitude = oSearchListingMapper.Longitude;
            searchListingMapper.Latitude = oSearchListingMapper.Latitude;

            if (!string.IsNullOrEmpty(oSearchListingMapper.Thumbnail))
                searchListingMapper.Thumbnail = oSearchListingMapper.Thumbnail;

            searchListingMapper.ModifiedTs = DateTime.UtcNow;
            searchListingMapper.ModifiedBy = oSearchListingMapper.ModifiedBy;
            _searchListingMapperRepository.Update(searchListingMapper);
            await SaveAsync();
            return EnumHelper.EOpStatus.Success;
        }

        public SearchListingMapper MapSearchListingLocalBusiness(LocalBusiness oLocalBusiness)
        {
            var oSearchListingMapper = new SearchListingMapper();
            oSearchListingMapper.SearchListingMapperId = Guid.NewGuid();
            oSearchListingMapper.RestaurantName = oLocalBusiness.Name;
            oSearchListingMapper.Description = oLocalBusiness.Description;
            oSearchListingMapper.Thumbnail = oLocalBusiness.Thumbnail;
            oSearchListingMapper.FkLocalBusinessId = oLocalBusiness.LocalBusinessId;

            oSearchListingMapper.CreatedTs = DateTime.UtcNow;
            oSearchListingMapper.Ip = CommonHelper.GetIpAddress();
            oSearchListingMapper.EFlag = EnumHelper.EFlag.Active;
            oSearchListingMapper.ModifiedBy = null;
            oSearchListingMapper.ModifiedTs = null;
            return oSearchListingMapper;
        }

        public SearchListingMapper MapSearchListingRestaurant(Restaurant oRestaurant)
        {
            var oSearchListingMapper =
                _searchListingMapperQueryRepository.GetByPredicate(
                    x => x.FkLocalBusinessId == oRestaurant.FkLocalBusinessId);
            if (oSearchListingMapper == null)
            {
                return null;
            }
            oSearchListingMapper.HasAlcohol = oRestaurant.HasAlcohol;
            oSearchListingMapper.HasCard = oRestaurant.HasCard;
            oSearchListingMapper.HasWifi = oRestaurant.HasWifi;
            oSearchListingMapper.HasParking = oRestaurant.HasParking;
            oSearchListingMapper.HasReservation = oRestaurant.HasReservation;
            oSearchListingMapper.HasSmoking = oRestaurant.HasSmoking;
            oSearchListingMapper.MenuSpecial = oRestaurant.MenuSpecial;
            oSearchListingMapper.Longitude = oRestaurant.GeoCoordinates.Longitude;
            oSearchListingMapper.Latitude = oRestaurant.GeoCoordinates.Latitude;
            oSearchListingMapper.FkRestaurantId = oRestaurant.RestaurantId;
            oSearchListingMapper.EStatus = EnumHelper.EStatus.New;
            var address = oRestaurant.GeoCoordinates.AddressByGoogle;
            if (string.IsNullOrEmpty(address))
            {
                address = oRestaurant.GeoCoordinates.Address;
            }
            oSearchListingMapper.Address = address;
            oSearchListingMapper.AddressRoute = oRestaurant.GeoCoordinates.AddressByGoogleRoute;
            oSearchListingMapper.AddressSubLocality = oRestaurant.GeoCoordinates.AddressByGoogleSubLocality;
            oSearchListingMapper.AddressLocality = oRestaurant.GeoCoordinates.AddressByGoogleLocality;
            oSearchListingMapper.AddressCountry = oRestaurant.GeoCoordinates.AddressByGoogleCountry;
            return oSearchListingMapper;
        }

        private async Task<EnumHelper.EOpStatus> CreateSearchListingMapperAsync(SearchListingMapper oSearchListingMapper)
        {
            oSearchListingMapper.CreatedTs = DateTime.UtcNow;
            oSearchListingMapper.Ip = CommonHelper.GetIpAddress();
            oSearchListingMapper.EFlag = EnumHelper.EFlag.Active;
            oSearchListingMapper.ModifiedBy = null;
            oSearchListingMapper.ModifiedTs = null;

            _searchListingMapperRepository.Insert(oSearchListingMapper);
            await SaveAsync();

            return EnumHelper.EOpStatus.Success;
        }

        private async Task<EnumHelper.EOpStatus> SaveAsync()
        {
            return await _unitOfWork.CommitAsync();
        }
    }
}