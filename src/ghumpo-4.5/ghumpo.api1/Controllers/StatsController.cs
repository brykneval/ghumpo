using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ghumpo.model.Mobile;
using ghumpo.service;
using ghumpo.service.Query;

namespace ghumpo.api1.Controllers
{
    public class StatsController : ApiController
    {
        private readonly IMobileStatsCommandService _mobileStatsCommandService;
        private readonly IProfileStatsQueryService _profileStatsQueryService;

        public StatsController(IProfileStatsQueryService profileStatsQueryService,
            IMobileStatsCommandService mobileStatsCommandService)
        {
            _profileStatsQueryService = profileStatsQueryService;
            _mobileStatsCommandService = mobileStatsCommandService;
        }

        [HttpGet]
        public FormatApi<ProfileStatsGetMobile> Get(string id)
        {
            var oProfileStats = _profileStatsQueryService.ProfileStats(id);
            return new FormatApi<ProfileStatsGetMobile> {message = "", data = oProfileStats};
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Post(MobileStatsMobile oModel)
        {
            if (!ModelState.IsValid)
            {
                var response =
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, "model state is invalid");
                throw new HttpResponseException(response);
            }
            await _mobileStatsCommandService.CreateMobileStatsAsync(oModel);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}