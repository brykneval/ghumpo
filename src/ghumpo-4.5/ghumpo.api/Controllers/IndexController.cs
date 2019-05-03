using System.Web.Http;
using ghumpo.model.Mobile;
using ghumpo.service.Query;

namespace ghumpo.api.Controllers
{
    public class IndexController : ApiController
    {
        private readonly ISearchListingMapperQueryService _searchListingMapperQueryService;

        public IndexController(ISearchListingMapperQueryService searchListingMapperQueryService)
        {
            _searchListingMapperQueryService = searchListingMapperQueryService;
        }

        public FormatApi<string> Get()
        {
            _searchListingMapperQueryService.RebuildIndex();
            return new FormatApi<string> {message = "", data = "Done"};
        }
    }
}