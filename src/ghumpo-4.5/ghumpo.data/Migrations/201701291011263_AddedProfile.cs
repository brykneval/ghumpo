using System.Data.Entity.Migrations;

namespace ghumpo.data.Migrations
{
    public partial class AddedProfile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profile",
                c => new
                {
                    ProfileId = c.Guid(false),
                    Name = c.String(),
                    About = c.String(),
                    Email = c.String(),
                    Gender = c.String(),
                    ImageUrl = c.String(),
                    ELoginType = c.Int(false),
                    UserId = c.String(),
                    Interest = c.String(),
                    Ip = c.String(),
                    CreatedBy = c.Guid(false),
                    ModifiedBy = c.Guid(),
                    CreatedTs = c.DateTime(false),
                    ModifiedTs = c.DateTime(),
                    EFlag = c.Int(false)
                })
                .PrimaryKey(t => t.ProfileId);
        }

        public override void Down()
        {
            DropTable("dbo.Profile");
        }
    }
}