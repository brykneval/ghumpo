using System.Data.Entity.Migrations;

namespace ghumpo.data.Migrations
{
    public partial class added_feedback : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedback",
                c => new
                {
                    FeedbackId = c.Long(false, true),
                    FeedbackMessage = c.String(maxLength: 500),
                    Ip = c.String(),
                    CreatedBy = c.Guid(false),
                    ModifiedBy = c.Guid(),
                    CreatedTs = c.DateTime(false),
                    ModifiedTs = c.DateTime(),
                    EFlag = c.Int(false)
                })
                .PrimaryKey(t => t.FeedbackId);
        }

        public override void Down()
        {
            DropTable("dbo.Feedback");
        }
    }
}