using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ghumpo.common;
using ghumpo.model.Mobile;

namespace ghumpo.service
{
    public interface IImageReportCommandService
    {
        Task<EnumHelper.EOpStatus> CreateImageReportAsync(ImageReportMobile oImageReport);
    }

    public class ImageReportCommandService : IImageReportCommandService
    {
        private readonly IDbConnection cnn;

        public ImageReportCommandService(IDbConnection cnn)
        {
            this.cnn = cnn;
        }

        public async Task<EnumHelper.EOpStatus> CreateImageReportAsync(ImageReportMobile oImageReportMobile)
        {
            await cnn.ExecuteAsync("spImageReportPost",
                new
                {
                    @reportStatus = (int) EnumHelper.EReportStatus.New,
                    @reportType = Convert.ToInt16(oImageReportMobile.report_type),
                    oImageReportMobile.message,
                    @imageId = oImageReportMobile.fk_imageid,
                    @ip = CommonHelper.GetIpAddress(),
                    @createdBy = oImageReportMobile.reported_by
                }, commandType: CommandType.StoredProcedure);
            return EnumHelper.EOpStatus.Success;
        }
    }
}