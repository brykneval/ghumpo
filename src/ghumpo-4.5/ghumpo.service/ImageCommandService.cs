using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Dapper;
using ghumpo.common;
using ghumpo.model.Mobile;

namespace ghumpo.service
{
    public interface IImageCommandService
    {
        Task<EnumHelper.EOpStatus> CreateImageAsync(ImagePostMobile oImage);
        Task<EnumHelper.EOpStatus> DeleteImageAsync(string deletedBy, string imageId);
    }

    public class ImageCommandService : IImageCommandService
    {
        private readonly IBlobCore blobCore;
        private readonly IDbConnection cnn;

        public ImageCommandService(IDbConnection cnn, IBlobCore blobCore)
        {
            this.cnn = cnn;
            this.blobCore = blobCore;
        }

        public async Task<EnumHelper.EOpStatus> CreateImageAsync(ImagePostMobile oImage)
        {
            var guid = Guid.NewGuid();
            var imageName = guid + "upload-image";
            var thumbName = guid + "upload-thumb";
            IList<Task> tasks = new List<Task>();

            Task taskImage = blobCore.UploadFromBase64(imageName, oImage.image_byte, 1200, 1920);
            Task taskThumb = blobCore.UploadFromBase64(thumbName, oImage.image_byte, 210, 210);
            Task taskDb =
                cnn.ExecuteAsync("spImagePost",
                    new
                    {
                        @imageId = guid,
                        @restaurantName = oImage.restaurant_name,
                        oImage.caption,
                        @exifData = oImage.exif_data,
                        @thumbnailUrl = thumbName,
                        @imageUrl = imageName,
                        @locationCreated = oImage.location_created,
                        @points = 0,
                        @createdBy = oImage.created_by,
                        @fkRestaurantId = oImage.fk_restaurantid,
                        @ip = CommonHelper.GetIpAddress(),
                        @type = Convert.ToInt32(oImage.type)
                    }, commandType: CommandType.StoredProcedure);
            tasks.AddTask(taskImage);
            tasks.AddTask(taskDb);
            tasks.AddTask(taskThumb);
            await Task.WhenAll(tasks.ToArray());
            return EnumHelper.EOpStatus.Success;
        }

        public async Task<EnumHelper.EOpStatus> DeleteImageAsync(string deletedBy, string imageId)
        {
            deletedBy = new JavaScriptSerializer().Deserialize(deletedBy, null).ToString();
            imageId = new JavaScriptSerializer().Deserialize(imageId, null).ToString();
            await cnn.ExecuteAsync("spDeleteImage",
                new
                {
                    @createdBy = deletedBy,
                    imageId
                }, commandType: CommandType.StoredProcedure);
            return EnumHelper.EOpStatus.Success;
        }
    }
}