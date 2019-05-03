using System;
using System.Threading.Tasks;
using ghumpo.common;
using ghumpo.data.Infrastructure;
using ghumpo.data.Repository;
using ghumpo.model;

namespace ghumpo.service
{
    public interface IGeoCoordinatesCommandService
    {
        Task<EnumHelper.EOpStatus> CreateGeoCoordinatesAsync(GeoCoordinates oGeoCoordinates);
    }

    public class GeoCoordinatesCommandService : IGeoCoordinatesCommandService
    {
        private readonly IGeoCoordinatesCommandRepository _GeoCoordinatesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GeoCoordinatesCommandService(IGeoCoordinatesCommandRepository GeoCoordinatesRepository,
            IUnitOfWork unitOfWork)
        {
            _GeoCoordinatesRepository = GeoCoordinatesRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EnumHelper.EOpStatus> CreateGeoCoordinatesAsync(GeoCoordinates oGeoCoordinates)
        {
            oGeoCoordinates.CreatedTs = DateTime.UtcNow;
            oGeoCoordinates.Ip = CommonHelper.GetIpAddress();
            oGeoCoordinates.EFlag = EnumHelper.EFlag.Active;
            oGeoCoordinates.ModifiedBy = null;
            oGeoCoordinates.ModifiedTs = null;

            _GeoCoordinatesRepository.Insert(oGeoCoordinates);
            await SaveAsync();

            return EnumHelper.EOpStatus.Success;
        }

        private async Task<EnumHelper.EOpStatus> SaveAsync()
        {
            return await _unitOfWork.CommitAsync();
        }
    }
}