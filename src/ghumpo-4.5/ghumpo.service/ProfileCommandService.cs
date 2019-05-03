using System;
using System.Threading.Tasks;
using ghumpo.common;
using ghumpo.data.Infrastructure;
using ghumpo.data.Repository;
using ghumpo.model;
using ghumpo.model.Mobile;

namespace ghumpo.service
{
    public interface IProfileCommandService
    {
        Task<EnumHelper.EOpStatus> CreateProfileAsync(ProfileMobile oProfile);
    }

    public class ProfileCommandService : IProfileCommandService
    {
        private readonly IProfileQueryRepository _profileQueryRepository;
        private readonly IProfileCommandRepository _profileRepository;
        private readonly IProfileStatsCommandRepository _profileStatsCommandRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProfileCommandService(IProfileCommandRepository profileRepository,
            IProfileQueryRepository profileQueryRepository, IProfileStatsCommandRepository profileStatsCommandRepository,
            IUnitOfWork unitOfWork)
        {
            _profileRepository = profileRepository;
            _profileQueryRepository = profileQueryRepository;
            _profileStatsCommandRepository = profileStatsCommandRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EnumHelper.EOpStatus> CreateProfileAsync(ProfileMobile oProfileMobile)
        {
            var oProfile = new Profile();
            oProfile.Name = oProfileMobile.name;
            oProfile.About = oProfileMobile.about;
            oProfile.Email = oProfileMobile.email;
            oProfile.ELoginType = (EnumHelper.ELoginType) Convert.ToInt16(oProfileMobile.login_type);
            oProfile.ImageUrl = oProfileMobile.image_url;
            oProfile.Gender = oProfileMobile.gender;
            oProfile.Interest = oProfileMobile.interest;
            oProfile.UserId = oProfileMobile.user_id;
            oProfile.ProfileId = oProfileMobile.profile_id;

            var profile =
                _profileQueryRepository.GetByPredicate(
                    x => x.UserId == oProfile.UserId && x.ELoginType == oProfile.ELoginType);
            if (profile == null)
            {
                oProfile.CreatedTs = DateTime.UtcNow;
                oProfile.Ip = CommonHelper.GetIpAddress();
                oProfile.EFlag = EnumHelper.EFlag.Active;
                oProfile.CreatedBy = oProfile.ProfileId;
                oProfile.ModifiedBy = null;
                oProfile.ModifiedTs = null;
                _profileStatsCommandRepository.Insert(InitializeProfileStats(oProfile.ProfileId));
                _profileRepository.Insert(oProfile);
            }
            else
            {
                profile.About = oProfile.About;
                profile.Email = oProfile.Email;
                profile.ImageUrl = oProfile.ImageUrl;
                profile.Interest = oProfile.Interest;
                profile.Name = oProfile.Name;

                profile.ModifiedTs = DateTime.UtcNow;
                profile.Ip = CommonHelper.GetIpAddress();
                profile.CreatedBy = oProfile.ProfileId;
                _profileRepository.Update(profile);
            }
            await SaveAsync();
            return EnumHelper.EOpStatus.Success;
        }

        private async Task<EnumHelper.EOpStatus> SaveAsync()
        {
            return await _unitOfWork.CommitAsync();
        }

        private ProfileStats InitializeProfileStats(Guid id)
        {
            var oProfileStats = new ProfileStats
            {
                ProfileStatsId = Guid.NewGuid(),
                TotalRatings = 0,
                TotalPosts = 0,
                ConnectedPosts = 0,
                ConnectedRatings = 0,
                UnConnectedPosts = 0,
                UnConnectedRatings = 0,
                Perfects = 0,
                FkProfileId = id
            };
            return oProfileStats;
        }
    }
}