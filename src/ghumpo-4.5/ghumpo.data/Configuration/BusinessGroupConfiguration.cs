using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ghumpo.model;

namespace ghumpo.data.Configuration
{
    public class BusinessGroupConfiguration : EntityTypeConfiguration<BusinessGroup>
    {
        public BusinessGroupConfiguration()
        {
            HasKey(x => x.BusinessGroupId);
            Property(x => x.BusinessGroupId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.BusinessGroupName).HasMaxLength(200);
        }
    }
}