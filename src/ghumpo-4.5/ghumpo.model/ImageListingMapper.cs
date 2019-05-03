using System;
using ghumpo.common;

namespace ghumpo.model
{
    public class ImageListingMapper : Common
    {
        public Guid ImageListingMapperId { get; set; }
        public Guid? FkImageId { get; set; }
        public Guid? FkRestaurantId { get; set; }
        public string Caption { get; set; }
        public string Tags { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbUrl { get; set; }
        public string ProfileName { get; set; }
        public string RestaurantName { get; set; }
        public string MenuSpecial { get; set; }
        public string Address { get; set; }
        public EnumHelper.EStatus EStatus { get; set; }
        public EnumHelper.EImageType EImageType { get; set; }
    }
}