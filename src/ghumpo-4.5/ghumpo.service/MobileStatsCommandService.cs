using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ghumpo.common;
using ghumpo.model.Mobile;

namespace ghumpo.service
{
    public interface IMobileStatsCommandService
    {
        Task<EnumHelper.EOpStatus> CreateMobileStatsAsync(MobileStatsMobile oMobileStats);
    }

    public class MobileStatsCommandService : IMobileStatsCommandService
    {
        private readonly IDbConnection cnn;

        public MobileStatsCommandService(IDbConnection cnn)
        {
            this.cnn = cnn;
        }

        public async Task<EnumHelper.EOpStatus> CreateMobileStatsAsync(MobileStatsMobile oMobileStatsMobile)
        {
            await cnn.ExecuteAsync("spMobileStatsPost",
                new
                {
                    @apiCallBy = Convert.ToInt16(oMobileStatsMobile.api_callby),
                    @mobileStatInteractionType = Convert.ToInt16(oMobileStatsMobile.mobilestat_interactiontype),
                    oMobileStatsMobile.parameters,
                    @createdBy = oMobileStatsMobile.created_by
                }, commandType: CommandType.StoredProcedure);
            return EnumHelper.EOpStatus.Success;
        }
    }
}