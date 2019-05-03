using System.Data.Entity.Migrations;

namespace ghumpo.data.Migrations
{
    public partial class addedDeletedRatingCount_ProfileStats : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProfileStats", "DeletedPostRating", c => c.Double(false));
        }

        public override void Down()
        {
            DropColumn("dbo.ProfileStats", "DeletedPostRating");
        }
    }
}