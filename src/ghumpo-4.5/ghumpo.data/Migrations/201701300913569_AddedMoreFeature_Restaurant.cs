using System.Data.Entity.Migrations;

namespace ghumpo.data.Migrations
{
    public partial class AddedMoreFeature_Restaurant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurant", "HasFloorSeating", c => c.Boolean(false));
            AddColumn("dbo.Restaurant", "HasLounge", c => c.Boolean(false));
            AddColumn("dbo.Restaurant", "HasIndoor", c => c.Boolean(false));
            AddColumn("dbo.Restaurant", "HasOutdoor", c => c.Boolean(false));
        }

        public override void Down()
        {
            DropColumn("dbo.Restaurant", "HasOutdoor");
            DropColumn("dbo.Restaurant", "HasIndoor");
            DropColumn("dbo.Restaurant", "HasLounge");
            DropColumn("dbo.Restaurant", "HasFloorSeating");
        }
    }
}