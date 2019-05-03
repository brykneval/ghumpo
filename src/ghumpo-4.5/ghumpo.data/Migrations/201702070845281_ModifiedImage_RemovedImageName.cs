using System.Data.Entity.Migrations;

namespace ghumpo.data.Migrations
{
    public partial class ModifiedImage_RemovedImageName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Image", "RestaurantName", c => c.String(maxLength: 200));
            DropColumn("dbo.Image", "ImageName");
        }

        public override void Down()
        {
            AddColumn("dbo.Image", "ImageName", c => c.String(maxLength: 100));
            DropColumn("dbo.Image", "RestaurantName");
        }
    }
}