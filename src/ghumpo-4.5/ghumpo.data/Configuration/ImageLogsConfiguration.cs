using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ghumpo.model;

namespace ghumpo.data.Configuration
{
    public class ImageLogsConfiguration : EntityTypeConfiguration<ImageLogs>
    {
        public ImageLogsConfiguration()
        {
            HasKey(x => x.ImageLogsId);
            Property(x => x.ImageLogsId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}