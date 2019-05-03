using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ghumpo.model;

namespace ghumpo.data.Configuration
{
    public class FeedbackConfiguration : EntityTypeConfiguration<Feedback>
    {
        public FeedbackConfiguration()
        {
            HasKey(x => x.FeedbackId);
            Property(x => x.FeedbackId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.FeedbackMessage).HasMaxLength(500);
        }
    }
}