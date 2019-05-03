using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ghumpo.data.Configuration;
using ghumpo.model;

namespace ghumpo.data
{
    public class GhumpoContext : DbContext
    {
        public GhumpoContext() : base("DefaultConnection")
        {
        }

        public DbSet<LocalBusiness> LocalBusiness { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<ImageRating> ImageRating { get; set; }
        public DbSet<GeoCoordinates> GeoCoordinates { get; set; }
        public DbSet<SearchListingMapper> SearchListingMapper { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<ImageListingMapper> ImageListingMapper { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<ProfileStats> ProfileStats { get; set; }
        public DbSet<ImageReport> ImageReport { get; set; }
        public DbSet<ImageLogs> ImageLogs { get; set; }
        public DbSet<MobileStats> MobileStats { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<BusinessGroup> BusinessGroup { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new LocalBusinessConfiguration());
            modelBuilder.Configurations.Add(new RestaurantConfiguration());
            modelBuilder.Configurations.Add(new ImageRatingConfiguration());
            modelBuilder.Configurations.Add(new GeoCoordinatesConfiguration());
            modelBuilder.Configurations.Add(new ImageConfiguration());
            modelBuilder.Configurations.Add(new ImageLogsConfiguration());
            modelBuilder.Configurations.Add(new MobileStatsConfiguration());
            modelBuilder.Configurations.Add(new FeedbackConfiguration());
            modelBuilder.Configurations.Add(new BusinessGroupConfiguration());
        }
    }
}