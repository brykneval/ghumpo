using System.Data.Entity.Migrations;

namespace ghumpo.data.Migrations
{
    public partial class AddedImage : DbMigration
    {
        public override void Up()
        {
            RenameTable("dbo.Rating", "ImageRating");
            CreateTable(
                "dbo.Image",
                c => new
                {
                    ImageId = c.Guid(false),
                    ImageName = c.String(maxLength: 100),
                    Caption = c.String(maxLength: 100),
                    ExifData = c.String(maxLength: 200),
                    ThumbnailUrl = c.String(maxLength: 100),
                    ImageUrl = c.String(maxLength: 100),
                    LocationCreated = c.String(maxLength: 200),
                    Points = c.Long(false),
                    FkRestaurantId = c.Guid(),
                    Ip = c.String(),
                    CreatedBy = c.Guid(false),
                    ModifiedBy = c.Guid(),
                    CreatedTs = c.DateTime(false),
                    ModifiedTs = c.DateTime(),
                    EFlag = c.Int(false)
                })
                .PrimaryKey(t => t.ImageId);

            CreateTable(
                "dbo.ImageListingMapper",
                c => new
                {
                    ImageListingMapperId = c.Guid(false),
                    FkImageId = c.Guid(false),
                    FkLocalBusinessId = c.Guid(false),
                    Caption = c.String(),
                    Tags = c.String(),
                    ImageUrl = c.String(),
                    ThumbUrl = c.String(),
                    ProfileName = c.String(),
                    RestaurantName = c.String(),
                    MenuSpecial = c.String(),
                    Address = c.String(),
                    EStatus = c.Int(false),
                    Ip = c.String(),
                    CreatedBy = c.Guid(false),
                    ModifiedBy = c.Guid(),
                    CreatedTs = c.DateTime(false),
                    ModifiedTs = c.DateTime(),
                    EFlag = c.Int(false)
                })
                .PrimaryKey(t => t.ImageListingMapperId);

            AddColumn("dbo.ImageRating", "FkImageId", c => c.Guid());
            DropColumn("dbo.ImageRating", "Author");
        }

        public override void Down()
        {
            AddColumn("dbo.ImageRating", "Author", c => c.Guid(false));
            DropColumn("dbo.ImageRating", "FkImageId");
            DropTable("dbo.ImageListingMapper");
            DropTable("dbo.Image");
            RenameTable("dbo.ImageRating", "Rating");
        }
    }
}