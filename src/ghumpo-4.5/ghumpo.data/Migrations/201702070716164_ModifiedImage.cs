using System.Data.Entity.Migrations;

namespace ghumpo.data.Migrations
{
    public partial class ModifiedImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageListingMapper", "FkRestaurantId", c => c.Guid());
            AlterColumn("dbo.ImageListingMapper", "FkImageId", c => c.Guid());
            DropColumn("dbo.ImageListingMapper", "FkLocalBusinessId");
        }

        public override void Down()
        {
            AddColumn("dbo.ImageListingMapper", "FkLocalBusinessId", c => c.Guid(false));
            AlterColumn("dbo.ImageListingMapper", "FkImageId", c => c.Guid(false));
            DropColumn("dbo.ImageListingMapper", "FkRestaurantId");
        }
    }
}