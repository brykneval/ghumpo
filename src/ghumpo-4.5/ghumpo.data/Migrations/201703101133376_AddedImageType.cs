using System.Data.Entity.Migrations;

namespace ghumpo.data.Migrations
{
    public partial class AddedImageType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Image", "EImageType", c => c.Int(false));
        }

        public override void Down()
        {
            DropColumn("dbo.Image", "EImageType");
        }
    }
}