using System.Web.Http;
using ghumpo.model.Mobile;
using ghumpo.service.Query;

namespace ghumpo.api1.Controllers
{
    public class BusinessGroupsController : ApiController
    {
        private readonly IBusinessGroupQueryService _businessGroupQueryService;

        public BusinessGroupsController(IBusinessGroupQueryService businessGroupQueryService)
        {
            _businessGroupQueryService = businessGroupQueryService;
        }

        public FormatListApi<BusinessGroupMobile> Get()
        {
            var groups = _businessGroupQueryService.GetBusinessGroups();
            return new FormatListApi<BusinessGroupMobile> {message = "", data = groups};
        }
    }
}