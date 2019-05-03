using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ghumpo.model.Mobile;
using ghumpo.service;
using ghumpo.service.Query;

namespace ghumpo.api1.Controllers
{
    public class ImageRatingsController : ApiController
    {
        private readonly IImageListingQueryService _imageListingQueryService;
        private readonly IImageRatingCommandService _imageRatingCommandService;

        public ImageRatingsController(IImageRatingCommandService ImageRatingCommandService,
            IImageListingQueryService imageListingQueryService)
        {
            _imageRatingCommandService = ImageRatingCommandService;
            _imageListingQueryService = imageListingQueryService;
        }

        [HttpPost]
        [ResponseType(typeof (HttpResponseMessage))]
        public async Task<HttpResponseMessage> Post(ImageRatingPostMobile oImageRating)
        {
            if (!ModelState.IsValid && oImageRating == null)
            {
                var response =
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, "model state is invalid");
                throw new HttpResponseException(response);
            }
            await _imageRatingCommandService.CreateImageRatingAsync(oImageRating);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [HttpGet]
        public FormatApi<ImagePointGetMobile> Get(string created_by, string image_id)
        {
            if (!string.IsNullOrEmpty(created_by) && !string.IsNullOrEmpty(image_id))
            {
                var oImagePointGetMobile = _imageListingQueryService.GetImagePoint(created_by, image_id);
                return new FormatApi<ImagePointGetMobile> {message = "", data = oImagePointGetMobile};
            }
            var response =
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, "model state is invalid");
            throw new HttpResponseException(response);
        }
    }
}