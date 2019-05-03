using System.Data.Entity.Migrations;

namespace ghumpo.data.Migrations
{
    public partial class addedimagereport : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageReport",
                c => new
                {
                    ImageReportId = c.Guid(false),
                    EReportType = c.Int(false),
                    EReportStatus = c.Int(false),
                    Message = c.String(),
                    FkProfileIdReportedBy = c.Guid(),
                    FkImageId = c.Guid(),
                    Ip = c.String(),
                    CreatedBy = c.Guid(false),
                    ModifiedBy = c.Guid(),
                    CreatedTs = c.DateTime(false),
                    ModifiedTs = c.DateTime(),
                    EFlag = c.Int(false)
                })
                .PrimaryKey(t => t.ImageReportId);
        }

        public override void Down()
        {
            DropTable("dbo.ImageReport");
        }
    }
}