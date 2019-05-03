using System.Data.Entity.Migrations;

namespace ghumpo.data.Migrations
{
    public partial class addeddeletedpostcount_profilestats : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProfileStats", "DeletedPostCount", c => c.Long(false));
        }

        public override void Down()
        {
            DropColumn("dbo.ProfileStats", "DeletedPostCount");
        }
    }
}