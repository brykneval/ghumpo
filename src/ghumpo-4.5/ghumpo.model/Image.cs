using System;
using ghumpo.common;

namespace ghumpo.model
{
    public class Image : Common
    {
        public Guid ImageId { get; set; }
        public string Caption { get; set; }
        public string ExifData { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ImageUrl { get; set; }
        public string LocationCreated { get; set; }
        public long Points { get; set; }
        public string RestaurantName { get; set; }

        public Guid? FkRestaurantId { get; set; }
        public EnumHelper.EImageType EImageType { get; set; }
        public EnumHelper.EReportType EImageReport { get; set; }
    }
}