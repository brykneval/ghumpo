using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ghumpo.common;
using ghumpo.model.Mobile;

namespace ghumpo.service
{
    public interface IImageRatingCommandService
    {
        Task<EnumHelper.EOpStatus> CreateImageRatingAsync(ImageRatingPostMobile oImageImageRating);
    }

    public class ImageRatingCommandService : IImageRatingCommandService
    {
        private readonly IDbConnection cnn;

        public ImageRatingCommandService(IDbConnection cnn)
        {
            this.cnn = cnn;
        }

        public async Task<EnumHelper.EOpStatus> CreateImageRatingAsync(ImageRatingPostMobile oImageRating)
        {
            await
                cnn.ExecuteAsync("spImageRatingPost",
                    new
                    {
                        ratingId = Guid.NewGuid(),
                        fkImageId = oImageRating.fk_imageid,
                        ratingValue = Convert.ToDouble(oImageRating.rating_value),
                        oImageRating.comment,
                        ip = CommonHelper.GetIpAddress(),
                        createdBy = oImageRating.created_by,
                        isUpdate = oImageRating.is_update
                    }, commandType: CommandType.StoredProcedure);
            return EnumHelper.EOpStatus.Success;
        }
    }
}