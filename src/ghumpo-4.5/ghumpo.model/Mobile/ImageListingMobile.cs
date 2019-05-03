using System;

namespace ghumpo.model.Mobile
{
    public class ImageListingMobile
    {
        public Guid? fk_imageid { get; set; }
        public string caption { get; set; }
        public string image_url { get; set; }
        public string thumb_url { get; set; }
        public string restaurant_name { get; set; }
        public string profile_name { get; set; }
    }
}