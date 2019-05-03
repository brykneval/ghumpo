using System.Data.Entity.ModelConfiguration;
using ghumpo.model;

namespace ghumpo.data.Configuration
{
    public class LocalBusinessConfiguration : EntityTypeConfiguration<LocalBusiness>
    {
        public LocalBusinessConfiguration()
        {
            HasKey(x => x.LocalBusinessId);
            Property(x => x.Name).HasMaxLength(100);
            Property(x => x.Description).HasMaxLength(200);
            Property(x => x.Image).HasMaxLength(100);
            Property(x => x.Thumbnail).HasMaxLength(100);

            Property(x => x.Url).HasMaxLength(150);
            Property(x => x.Email).HasMaxLength(150);
            Property(x => x.Logo).HasMaxLength(100);
            Property(x => x.CurrenciesAccepted).HasMaxLength(100);
            Property(x => x.PaymentAccepted).HasMaxLength(100);
            Property(x => x.OpeningHours).HasMaxLength(100);
        }
    }
}