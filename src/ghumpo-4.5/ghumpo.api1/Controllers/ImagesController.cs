using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ghumpo.common;
using ghumpo.model.Mobile;
using ghumpo.service;
using ghumpo.service.Query;

namespace ghumpo.api1.Controllers
{
    public class ImagesController : ApiController
    {
        private readonly IImageCommandService _imageCommandService;
        private readonly IImageListingQueryService _imageListingQueryService;
        private readonly IImageRestaurantCommandService _imageRestaurantCommandService;

        public ImagesController(IImageCommandService imageCommandService,
            IImageListingQueryService imageListingQueryService,
            IImageRestaurantCommandService imageRestaurantCommandService)
        {
            _imageCommandService = imageCommandService;
            _imageListingQueryService = imageListingQueryService;
            _imageRestaurantCommandService = imageRestaurantCommandService;
        }

        [HttpPost]
        [ResponseType(typeof (HttpResponseMessage))]
        public async Task<HttpResponseMessage> Post(ImagePostMobile oImage)
        {
            if (!ModelState.IsValid)
            {
                var response =
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, "model state is invalid");
                throw new HttpResponseException(response);
            }
            switch (Convert.ToInt32(oImage.type))
            {
                case (int) EnumHelper.EImageType.UserUploads:
                    await _imageCommandService.CreateImageAsync(oImage);
                    break;
                case (int) EnumHelper.EImageType.RestaurantOverview:
                    await _imageRestaurantCommandService.CreateImageRestaurantAsync(oImage);
                    break;
            }

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [HttpDelete]
        [ResponseType(typeof (HttpResponseMessage))]
        public async Task<HttpResponseMessage> Post(string deleted_by, string image_id)
        {
            if (!ModelState.IsValid && string.IsNullOrEmpty(deleted_by) && string.IsNullOrEmpty(image_id))
            {
                var response =
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, "model state is invalid");
                throw new HttpResponseException(response);
            }
            await _imageCommandService.DeleteImageAsync(deleted_by, image_id);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [HttpGet]
        public FormatListApi<ImageListingMobile> Get(string query, int type)
        {
            if (!string.IsNullOrEmpty(query))
            {
                var images = _imageListingQueryService.Images(query, type);
                return new FormatListApi<ImageListingMobile> {message = "", data = images};
            }
            var response =
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, "model state is invalid");
            throw new HttpResponseException(response);
        }
    }
}