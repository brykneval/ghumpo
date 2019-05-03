using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ghumpo.model;

namespace ghumpo.data.Configuration
{
    public class MobileStatsConfiguration : EntityTypeConfiguration<MobileStats>
    {
        public MobileStatsConfiguration()
        {
            HasKey(x => x.MobileStatsId);
            Property(x => x.MobileStatsId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Parameters).HasMaxLength(500);
        }
    }
}