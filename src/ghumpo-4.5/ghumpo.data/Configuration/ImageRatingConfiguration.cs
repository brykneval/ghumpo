using System.Data.Entity.ModelConfiguration;
using ghumpo.model;

namespace ghumpo.data.Configuration
{
    public class ImageRatingConfiguration : EntityTypeConfiguration<ImageRating>
    {
        public ImageRatingConfiguration()
        {
            HasKey(x => x.RatingId);
            Property(x => x.Comment).HasMaxLength(80);
        }
    }
}