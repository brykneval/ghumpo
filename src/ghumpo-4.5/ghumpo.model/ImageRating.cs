using System;

namespace ghumpo.model
{
    public class ImageRating : Common
    {
        public Guid RatingId { get; set; }
        public Guid? FkImageId { get; set; }
        public short RatingValue { get; set; }
        public string Comment { get; set; }
    }
}