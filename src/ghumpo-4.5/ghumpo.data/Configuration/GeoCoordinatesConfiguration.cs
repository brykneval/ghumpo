using System.Data.Entity.ModelConfiguration;
using ghumpo.model;

namespace ghumpo.data.Configuration
{
    public class GeoCoordinatesConfiguration : EntityTypeConfiguration<GeoCoordinates>
    {
        public GeoCoordinatesConfiguration()
        {
            HasKey(x => x.GeoCoordinatesId);
            Property(x => x.Address).HasMaxLength(200);
            Property(x => x.Latitude).HasMaxLength(20);
            Property(x => x.Longitude).HasMaxLength(20);
            Property(x => x.Elevation).HasMaxLength(20);
            Property(x => x.PostalCode).HasMaxLength(20);
        }
    }
}