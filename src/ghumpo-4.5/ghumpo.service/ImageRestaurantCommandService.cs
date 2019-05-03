using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ghumpo.common;
using ghumpo.model.Mobile;

namespace ghumpo.service
{
    public interface IImageRestaurantCommandService
    {
        Task<EnumHelper.EOpStatus> CreateImageRestaurantAsync(ImagePostMobile oImageRestaurant);
    }

    public class ImageRestaurantCommandService : IImageRestaurantCommandService
    {
        private readonly IBlobCore blobCore;
        private readonly IDbConnection cnn;

        public ImageRestaurantCommandService(IDbConnection cnn, IBlobCore blobCore)
        {
            this.cnn = cnn;
            this.blobCore = blobCore;
        }

        public async Task<EnumHelper.EOpStatus> CreateImageRestaurantAsync(ImagePostMobile oImageRestaurant)
        {
            var guid = Guid.NewGuid();
            var imageRestaurantName = guid + "upload-imagerestaurant";
            var thumbName = guid + "upload-thumbrestaurant";
            IList<Task> tasks = new List<Task>();

            Task taskImageRestaurant = blobCore.UploadFromBase64(imageRestaurantName, oImageRestaurant.image_byte, 1200,
                1920, "upload-reference-restaurant-image");
            Task taskThumb = blobCore.UploadFromBase64(thumbName, oImageRestaurant.image_byte, 210, 210,
                "upload-reference-restaurant-image");
            Task taskDb =
                cnn.ExecuteAsync("spRestaurantImagePost",
                    new
                    {
                        @imageId = guid,
                        @thumbnailUrl = thumbName,
                        @imageUrl = imageRestaurantName,
                        @fkRestaurantId = oImageRestaurant.fk_restaurantid,
                        @ip = CommonHelper.GetIpAddress(),
                        @type = Convert.ToInt32(oImageRestaurant.type)
                    }, commandType: CommandType.StoredProcedure);
            tasks.AddTask(taskImageRestaurant);
            tasks.AddTask(taskDb);
            tasks.AddTask(taskThumb);
            try
            {
                await Task.WhenAll(tasks.ToArray());
            }
            catch (Exception ex)
            {
            }
            return EnumHelper.EOpStatus.Success;
        }
    }
}