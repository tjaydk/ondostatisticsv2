using OndoStatisticsWebjob_2._0.Entities.clubEntities;

namespace OndoStatisticsWebjob_2._0.Entities
{
    class ClubEntity
    {
        public int ondoId { get; set; }
        public string title { get; set; }
        public string profilePicture { get; set; }
        public int prognose { get; set; }
        public int estimatedPrognose { get; set; }
        public int eightyPercentPrognose { get; set; }
        public int eightyPercentEstimatedPrognose { get; set; }
        public int percentActiveUsers { get; set; }
        public int activeUsers { get; set; }
        public int inactiveUsers { get; set; }
        public int percentAppUsers { get; set; }
        public int noAppUsers { get; set; }
        public int appUsers { get; set; }
        public ClubQuarterHistory[] history { get; set; } //Make sure later that history only go 4 quarters back
        public int target { get; set; }
        public int daysLeftInQuarter { get; set; }


    }
}
