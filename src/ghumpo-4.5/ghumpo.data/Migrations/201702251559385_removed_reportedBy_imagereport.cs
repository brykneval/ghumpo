using System.Data.Entity.Migrations;

namespace ghumpo.data.Migrations
{
    public partial class removed_reportedBy_imagereport : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ImageReport", "FkProfileIdReportedBy");
        }

        public override void Down()
        {
            AddColumn("dbo.ImageReport", "FkProfileIdReportedBy", c => c.Guid());
        }
    }
}