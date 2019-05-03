using System;
using System.Threading.Tasks;
using ghumpo.common;
using ghumpo.data.Infrastructure;
using ghumpo.data.Repository;
using ghumpo.model;

namespace ghumpo.service
{
    public interface ILocalBusinessCommandService
    {
        Task<EnumHelper.EOpStatus> CreateLocalBusinessAsync(LocalBusiness oLocalBusiness);
    }

    public class LocalBusinessCommandService : ILocalBusinessCommandService
    {
        private readonly ILocalBusinessCommandRepository _localBusinessRepository;
        private readonly ISearchListingMapperCommandRepository _searchListingMapperCommandRepository;
        private readonly ISearchListingMapperCommandService _searchListingMapperCommandService;
        private readonly IUnitOfWork _unitOfWork;

        public LocalBusinessCommandService(ILocalBusinessCommandRepository localBusinessRepository,
            ISearchListingMapperCommandRepository searchListingMapperCommandRepository,
            ISearchListingMapperCommandService searchListingMapperCommandService, IUnitOfWork unitOfWork)
        {
            _localBusinessRepository = localBusinessRepository;
            _searchListingMapperCommandRepository = searchListingMapperCommandRepository;
            _searchListingMapperCommandService = searchListingMapperCommandService;
            _unitOfWork = unitOfWork;
        }

        public async Task<EnumHelper.EOpStatus> CreateLocalBusinessAsync(LocalBusiness oLocalBusiness)
        {
            oLocalBusiness.CreatedTs = DateTime.UtcNow;
            oLocalBusiness.Ip = CommonHelper.GetIpAddress();
            oLocalBusiness.EFlag = EnumHelper.EFlag.Active;
            oLocalBusiness.ModifiedBy = null;
            oLocalBusiness.ModifiedTs = null;

            var oSearchListingMapper = _searchListingMapperCommandService.MapSearchListingLocalBusiness(oLocalBusiness);
            _searchListingMapperCommandRepository.Insert(oSearchListingMapper);
            _localBusinessRepository.Insert(oLocalBusiness);

            await Task.WhenAll(SaveAsync());
            return EnumHelper.EOpStatus.Success;
        }

        private async Task<EnumHelper.EOpStatus> SaveAsync()
        {
            return await _unitOfWork.CommitAsync();
        }
    }
}