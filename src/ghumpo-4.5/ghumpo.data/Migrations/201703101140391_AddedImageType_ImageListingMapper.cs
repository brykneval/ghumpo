using System.Data.Entity.Migrations;

namespace ghumpo.data.Migrations
{
    public partial class AddedImageType_ImageListingMapper : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageListingMapper", "EImageType", c => c.Int(false));
        }

        public override void Down()
        {
            DropColumn("dbo.ImageListingMapper", "EImageType");
        }
    }
}