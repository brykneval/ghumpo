using System.Data;
using System.Threading.Tasks;
using Dapper;
using ghumpo.common;
using ghumpo.model.Mobile;

namespace ghumpo.service
{
    public interface IFeedbackCommandService
    {
        Task<EnumHelper.EOpStatus> CreateFeedbackAsync(FeedbackMobile oFeedback);
    }

    public class FeedbackCommandService : IFeedbackCommandService
    {
        private readonly IDbConnection cnn;

        public FeedbackCommandService(IDbConnection cnn)
        {
            this.cnn = cnn;
        }

        public async Task<EnumHelper.EOpStatus> CreateFeedbackAsync(FeedbackMobile oFeedbackMobile)
        {
            await
                cnn.ExecuteAsync("spFeedbackPost",
                    new
                    {
                        oFeedbackMobile.feedback,
                        @createdBy = oFeedbackMobile.created_by,
                        @ip = CommonHelper.GetIpAddress()
                    }, commandType: CommandType.StoredProcedure);
            return EnumHelper.EOpStatus.Success;
        }
    }
}