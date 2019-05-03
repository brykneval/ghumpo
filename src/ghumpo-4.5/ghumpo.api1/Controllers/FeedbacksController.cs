using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ghumpo.model.Mobile;
using ghumpo.service;

namespace ghumpo.api1.Controllers
{
    public class FeedbacksController : ApiController
    {
        private readonly IFeedbackCommandService _feedbackCommandService;

        public FeedbacksController(IFeedbackCommandService feedbackCommandService)
        {
            _feedbackCommandService = feedbackCommandService;
        }

        [HttpPost]
        [ResponseType(typeof (HttpResponseMessage))]
        public async Task<HttpResponseMessage> Post(FeedbackMobile oData)
        {
            if (!ModelState.IsValid)
            {
                var response =
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, "model state is invalid");
                throw new HttpResponseException(response);
            }
            await _feedbackCommandService.CreateFeedbackAsync(oData);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}