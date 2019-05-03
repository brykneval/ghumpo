using System.Data.Entity.Migrations;

namespace ghumpo.data.Migrations
{
    public partial class AddedColumn_SearchListingMapper : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SearchListingMapper", "EStatus", c => c.Int(false));
            AddColumn("dbo.SearchListingMapper", "FkRestaurantId", c => c.Guid(false));
        }

        public override void Down()
        {
            DropColumn("dbo.SearchListingMapper", "FkRestaurantId");
            DropColumn("dbo.SearchListingMapper", "EStatus");
        }
    }
}