using System.Data.Entity.Migrations;

namespace ghumpo.data.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GeoCoordinates",
                c => new
                {
                    GeoCoordinatesId = c.Guid(false),
                    Address = c.String(maxLength: 200),
                    AddressByGoogle = c.String(),
                    Latitude = c.String(maxLength: 20),
                    Longitude = c.String(maxLength: 20),
                    Elevation = c.String(maxLength: 20),
                    PostalCode = c.String(maxLength: 20),
                    ECountry = c.Int(false),
                    Ip = c.String(),
                    CreatedBy = c.Guid(false),
                    ModifiedBy = c.Guid(),
                    CreatedTs = c.DateTime(false),
                    ModifiedTs = c.DateTime(),
                    EFlag = c.Int(false)
                })
                .PrimaryKey(t => t.GeoCoordinatesId);

            CreateTable(
                "dbo.LocalBusiness",
                c => new
                {
                    LocalBusinessId = c.Guid(false),
                    Name = c.String(maxLength: 100),
                    Description = c.String(maxLength: 200),
                    Image = c.String(maxLength: 100),
                    Thumbnail = c.String(maxLength: 100),
                    Url = c.String(maxLength: 150),
                    Email = c.String(maxLength: 150),
                    Logo = c.String(maxLength: 100),
                    CurrenciesAccepted = c.String(maxLength: 100),
                    PaymentAccepted = c.String(maxLength: 100),
                    OpeningHours = c.String(maxLength: 100),
                    Ip = c.String(),
                    CreatedBy = c.Guid(false),
                    ModifiedBy = c.Guid(),
                    CreatedTs = c.DateTime(false),
                    ModifiedTs = c.DateTime(),
                    EFlag = c.Int(false)
                })
                .PrimaryKey(t => t.LocalBusinessId);

            CreateTable(
                "dbo.Rating",
                c => new
                {
                    RatingId = c.Guid(false),
                    Author = c.Guid(false),
                    RatingValue = c.Short(false),
                    Comment = c.String(maxLength: 80),
                    Ip = c.String(),
                    CreatedBy = c.Guid(false),
                    ModifiedBy = c.Guid(),
                    CreatedTs = c.DateTime(false),
                    ModifiedTs = c.DateTime(),
                    EFlag = c.Int(false)
                })
                .PrimaryKey(t => t.RatingId);

            CreateTable(
                "dbo.Restaurant",
                c => new
                {
                    RestaurantId = c.Guid(false),
                    HasReservation = c.Boolean(false),
                    HasCard = c.Boolean(false),
                    HasParking = c.Boolean(false),
                    HasAlcohol = c.Boolean(false),
                    HasSmoking = c.Boolean(false),
                    HasWifi = c.Boolean(false),
                    Cuisine = c.String(maxLength: 200),
                    MenuSpecial = c.String(maxLength: 400),
                    Contact = c.String(maxLength: 100),
                    FkLocalBusinessId = c.Guid(false),
                    FkGeoCoordinatesId = c.Guid(),
                    Ip = c.String(),
                    CreatedBy = c.Guid(false),
                    ModifiedBy = c.Guid(),
                    CreatedTs = c.DateTime(false),
                    ModifiedTs = c.DateTime(),
                    EFlag = c.Int(false)
                })
                .PrimaryKey(t => t.RestaurantId)
                .ForeignKey("dbo.GeoCoordinates", t => t.FkGeoCoordinatesId)
                .Index(t => t.FkGeoCoordinatesId);

            CreateTable(
                "dbo.SearchListingMapper",
                c => new
                {
                    SearchListingMapperId = c.Guid(false),
                    RestaurantName = c.String(),
                    Description = c.String(),
                    Thumbnail = c.String(),
                    MenuSpecial = c.String(),
                    Address = c.String(),
                    HasReservation = c.Boolean(false),
                    HasCard = c.Boolean(false),
                    HasParking = c.Boolean(false),
                    HasAlcohol = c.Boolean(false),
                    HasSmoking = c.Boolean(false),
                    HasWifi = c.Boolean(false),
                    Longitude = c.String(),
                    Latitude = c.String(),
                    FkLocalBusinessId = c.Guid(false),
                    Ip = c.String(),
                    CreatedBy = c.Guid(false),
                    ModifiedBy = c.Guid(),
                    CreatedTs = c.DateTime(false),
                    ModifiedTs = c.DateTime(),
                    EFlag = c.Int(false)
                })
                .PrimaryKey(t => t.SearchListingMapperId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Restaurant", "FkGeoCoordinatesId", "dbo.GeoCoordinates");
            DropIndex("dbo.Restaurant", new[] {"FkGeoCoordinatesId"});
            DropTable("dbo.SearchListingMapper");
            DropTable("dbo.Restaurant");
            DropTable("dbo.Rating");
            DropTable("dbo.LocalBusiness");
            DropTable("dbo.GeoCoordinates");
        }
    }
}