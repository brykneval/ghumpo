using System.Data.Entity.Migrations;

namespace ghumpo.data.Migrations
{
    public partial class added_businessgrouptable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusinessGroup",
                c => new
                {
                    BusinessGroupId = c.Long(false, true),
                    BusinessGroupName = c.String(maxLength: 200)
                })
                .PrimaryKey(t => t.BusinessGroupId);

            AddColumn("dbo.LocalBusiness", "FkBusinessGroupId", c => c.Long(false));
            AddColumn("dbo.LocalBusiness", "ELocalBusinessType", c => c.Int(false));
        }

        public override void Down()
        {
            DropColumn("dbo.LocalBusiness", "ELocalBusinessType");
            DropColumn("dbo.LocalBusiness", "FkBusinessGroupId");
            DropTable("dbo.BusinessGroup");
        }
    }
}