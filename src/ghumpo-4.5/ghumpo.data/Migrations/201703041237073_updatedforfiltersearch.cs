using System.Data.Entity.Migrations;

namespace ghumpo.data.Migrations
{
    public partial class updatedforfiltersearch : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SearchListingMapper", "AddressRoute", c => c.String());
            AddColumn("dbo.SearchListingMapper", "AddressSubLocality", c => c.String());
            AddColumn("dbo.SearchListingMapper", "AddressLocality", c => c.String());
            AddColumn("dbo.SearchListingMapper", "AddressCountry", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.SearchListingMapper", "AddressCountry");
            DropColumn("dbo.SearchListingMapper", "AddressLocality");
            DropColumn("dbo.SearchListingMapper", "AddressSubLocality");
            DropColumn("dbo.SearchListingMapper", "AddressRoute");
        }
    }
}