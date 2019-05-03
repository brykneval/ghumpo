using System;

namespace ghumpo.common
{
    public class EnumHelper
    {
        public enum EApiCallBy
        {
            Android = 1,
            iOS,
            Web
        }

        [Flags]
        public enum ECountry
        {
            Nepal = 1
        }

        public enum EFlag
        {
            Active = 1,
            InActive,
            Delete,
            Halt
        }

        public enum EImageType
        {
            UserUploads = 1,
            RestaurantOverview = 2
        }

        [Flags]
        public enum ELocalBusinessType
        {
            Restaurant = 1,
            LocalRestaurant = 2,
            HotelRestaurant = 4
        }

        public enum ELoginType
        {
            Facebook = 1,
            Google,
            Twitter
        }

        public enum EMobileStatInteractionType
        {
            Home = 0,
            HomeRecommendationClick = 1,
            HomeOfflineRecommendationClick = 2,
            HomeCameraClick = 3,
            HomeToSearch = 4,
            SearchToLastSearchedClick = 5,
            SearchToDetailClick = 6,
            DetailToMapClick = 7,
            DetailToPhoneClick = 8,
            DetailToCameraClick = 9,
            DetailToFavouriteClick = 10,
            HomeToProfile = 11,
            ProfileLogout = 12,
            DetailToPhoto = 13,
            HomeIamHungry = 14,
            SearchToSearch = 15
        }

        public enum EOpStatus
        {
            Success = 1,
            Fail
        }

        public enum EReportStatus
        {
            New = 1,
            Checking,
            TrueReport,
            FalseReport,
            OnHold,
            ActionTaken
        }

        public enum EReportType
        {
            Offensive = 1,
            Fake,
            NotMatching,
            Duplicate
        }

        public enum EStatus
        {
            New = 1,
            Processing = 2,
            Processed = 3,
            Error = 4,
            Delete = 5,
            Deleting = 6,
            Deleted = 7,
            ErrorDeleting
        }
    }
}