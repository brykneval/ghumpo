using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ghumpo.model;
using ghumpo.service;

namespace ghumpo.api1.Controllers
{
    public class LocalBusinessController : ApiController
    {
        private readonly ILocalBusinessCommandService localBusinessCommandService;

        public LocalBusinessController(ILocalBusinessCommandService localBusinessCommandService)
        {
            this.localBusinessCommandService = localBusinessCommandService;
        }


        [HttpPost]
        [ResponseType(typeof (HttpResponseMessage))]
        public async Task<HttpResponseMessage> Post(LocalBusiness oLocalBusiness)
        {
            if (!ModelState.IsValid)
            {
                var response =
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, "model state is invalid");
                throw new HttpResponseException(response);
            }
            await localBusinessCommandService.CreateLocalBusinessAsync(oLocalBusiness);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        public string Get(int id)
        {
            return "value";
        }
    }
}