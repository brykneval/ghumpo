using System.Data.Entity.ModelConfiguration;
using ghumpo.model;

namespace ghumpo.data.Configuration
{
    public class RestaurantConfiguration : EntityTypeConfiguration<Restaurant>
    {
        public RestaurantConfiguration()
        {
            HasKey(x => x.RestaurantId);
            Property(x => x.Cuisine).HasMaxLength(200);
            Property(x => x.MenuSpecial).HasMaxLength(400);
            Property(x => x.Contact).HasMaxLength(100);
            Property(x => x.FkGeoCoordinatesId).IsOptional();
            HasOptional(x => x.GeoCoordinates).WithMany().HasForeignKey(x => x.FkGeoCoordinatesId);
        }
    }
}