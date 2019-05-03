using System;

namespace ghumpo.model.Mobile
{
    public class ProfileMobile
    {
        public Guid profile_id { get; set; }
        public string name { get; set; }
        public string about { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string image_url { get; set; }
        public string user_id { get; set; }
        public string interest { get; set; }
        public string login_type { get; set; }
        public string created_ts { get; set; }
    }
}