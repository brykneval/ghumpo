using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ghumpo.data.Repository;
using ghumpo.model.Mobile;
using ghumpo.service;
using ghumpo.service.Query;

namespace ghumpo.api.Controllers
{
    public class ProfilesController : ApiController
    {
        private readonly IProfileCommandService _profileCommandService;
        private readonly IProfileQueryRepository _profileQueryRepository;
        private readonly ISearchListingMapperQueryService _searchListingMapperQueryService;

        public ProfilesController(IProfileCommandService profileCommandService,
            IProfileQueryRepository profileQueryRepository,
            ISearchListingMapperQueryService searchListingMapperQueryService)
        {
            _profileCommandService = profileCommandService;
            _profileQueryRepository = profileQueryRepository;
            _searchListingMapperQueryService = searchListingMapperQueryService;
        }

        [HttpPost]
        [ResponseType(typeof (HttpResponseMessage))]
        public async Task<HttpResponseMessage> Post(ProfileMobile oProfile)
        {
            if (!ModelState.IsValid && oProfile == null)
            {
                var response =
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, "model state is invalid");
                throw new HttpResponseException(response);
            }
            await _profileCommandService.CreateProfileAsync(oProfile);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        public FormatApi<ProfileMobile> Get(string id)
        {
            var oProfile = _profileQueryRepository.GetByPredicate(x => x.UserId == id);
            var oProfileMobile = new ProfileMobile
            {
                profile_id = oProfile.ProfileId,
                name = oProfile.Name,
                about = oProfile.About,
                email = oProfile.Email,
                gender = oProfile.Gender,
                image_url = oProfile.ImageUrl,
                user_id = oProfile.UserId,
                interest = oProfile.Interest,
                created_ts = oProfile.CreatedTs.ToString("MMM dd, yyyy")
            };
            return new FormatApi<ProfileMobile> {message = "", data = oProfileMobile};
        }
    }
}