using System.Data.Entity.Migrations;

namespace ghumpo.data.Migrations
{
    public partial class added_AddressByGoogleRoute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GeoCoordinates", "AddressByGoogleRoute", c => c.String());
            AddColumn("dbo.GeoCoordinates", "AddressByGoogleSubLocality", c => c.String());
            AddColumn("dbo.GeoCoordinates", "AddressByGoogleLocality", c => c.String());
            AddColumn("dbo.GeoCoordinates", "AddressByGoogleCountry", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.GeoCoordinates", "AddressByGoogleCountry");
            DropColumn("dbo.GeoCoordinates", "AddressByGoogleLocality");
            DropColumn("dbo.GeoCoordinates", "AddressByGoogleSubLocality");
            DropColumn("dbo.GeoCoordinates", "AddressByGoogleRoute");
        }
    }
}