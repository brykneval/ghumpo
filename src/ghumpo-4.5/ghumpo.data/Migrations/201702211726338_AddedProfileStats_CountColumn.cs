using System.Data.Entity.Migrations;

namespace ghumpo.data.Migrations
{
    public partial class AddedProfileStats_CountColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProfileStats", "TotalRatingCount", c => c.Long(false));
            AddColumn("dbo.ProfileStats", "TotalConnectedRatingCount", c => c.Long(false));
            AddColumn("dbo.ProfileStats", "TotalUnConnectedRatingCount", c => c.Long(false));
        }

        public override void Down()
        {
            DropColumn("dbo.ProfileStats", "TotalUnConnectedRatingCount");
            DropColumn("dbo.ProfileStats", "TotalConnectedRatingCount");
            DropColumn("dbo.ProfileStats", "TotalRatingCount");
        }
    }
}