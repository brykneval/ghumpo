using System.Data.Entity.Migrations;

namespace ghumpo.data.Migrations
{
    public partial class AddedProfileStats : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfileStats",
                c => new
                {
                    ProfileStatsId = c.Guid(false),
                    TotalRatings = c.Double(false),
                    TotalPosts = c.Long(false),
                    ConnectedPosts = c.Long(false),
                    ConnectedRatings = c.Double(false),
                    UnConnectedPosts = c.Long(false),
                    UnConnectedRatings = c.Double(false),
                    Perfects = c.Int(false),
                    FkProfileId = c.Guid()
                })
                .PrimaryKey(t => t.ProfileStatsId);
        }

        public override void Down()
        {
            DropTable("dbo.ProfileStats");
        }
    }
}