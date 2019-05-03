using System;

namespace ghumpo.model
{
    public class ProfileStats
    {
        public Guid ProfileStatsId { get; set; }
        public double TotalRatings { get; set; }
        public long TotalPosts { get; set; }
        public long ConnectedPosts { get; set; }
        public double ConnectedRatings { get; set; }
        public long UnConnectedPosts { get; set; }
        public double UnConnectedRatings { get; set; }
        public int Perfects { get; set; }
        public long TotalRatingCount { get; set; }
        public long TotalConnectedRatingCount { get; set; }
        public long TotalUnConnectedRatingCount { get; set; }
        public long DeletedPostCount { get; set; }
        public double DeletedPostRating { get; set; }
        public Guid? FkProfileId { get; set; }
    }
}