using System.Data.Entity.ModelConfiguration;
using ghumpo.model;

namespace ghumpo.data.Configuration
{
    public class ImageConfiguration : EntityTypeConfiguration<Image>
    {
        public ImageConfiguration()
        {
            HasKey(x => x.ImageId);
            Property(x => x.RestaurantName).HasMaxLength(200);
            Property(x => x.Caption).HasMaxLength(100);
            Property(x => x.ExifData).HasMaxLength(200);
            Property(x => x.ThumbnailUrl).HasMaxLength(100);
            Property(x => x.ImageUrl).HasMaxLength(100);
            Property(x => x.LocationCreated).HasMaxLength(200);
        }
    }
}