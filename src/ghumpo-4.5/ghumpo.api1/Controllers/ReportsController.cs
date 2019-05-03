using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ghumpo.model.Mobile;
using ghumpo.service;

namespace ghumpo.api1.Controllers
{
    public class ReportsController : ApiController
    {
        private readonly IImageReportCommandService _imageReportCommandService;

        public ReportsController(IImageReportCommandService imageReportCommandService)
        {
            _imageReportCommandService = imageReportCommandService;
        }

        [HttpPost]
        [ResponseType(typeof (HttpResponseMessage))]
        public async Task<HttpResponseMessage> Post(ImageReportMobile oImage)
        {
            if (!ModelState.IsValid)
            {
                var response =
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, "model state is invalid");
                throw new HttpResponseException(response);
            }
            await _imageReportCommandService.CreateImageReportAsync(oImage);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}