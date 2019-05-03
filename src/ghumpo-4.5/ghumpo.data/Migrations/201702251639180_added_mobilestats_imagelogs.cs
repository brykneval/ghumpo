using System.Data.Entity.Migrations;

namespace ghumpo.data.Migrations
{
    public partial class added_mobilestats_imagelogs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageLogs",
                c => new
                {
                    ImageLogsId = c.Long(false, true),
                    FkImageId = c.Guid(false),
                    FkProfileIdViewedBy = c.Guid(false),
                    ViewedTs = c.DateTime(false),
                    EApiCallBy = c.Int(false)
                })
                .PrimaryKey(t => t.ImageLogsId);

            CreateTable(
                "dbo.MobileStats",
                c => new
                {
                    MobileStatsId = c.Long(false, true),
                    EApiCallBy = c.Int(false),
                    EMobileStatInteractionType = c.Int(false),
                    Parameters = c.String(maxLength: 500),
                    CreatedBy = c.Guid(false),
                    CreatedTs = c.DateTime(false)
                })
                .PrimaryKey(t => t.MobileStatsId);
        }

        public override void Down()
        {
            DropTable("dbo.MobileStats");
            DropTable("dbo.ImageLogs");
        }
    }
}