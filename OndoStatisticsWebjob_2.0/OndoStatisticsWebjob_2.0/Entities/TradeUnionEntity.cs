using OndoStatisticsWebjob_2._0.Entities.shopEntities;
using OndoStatisticsWebjob_2._0.Entities.TradeUnionEntities;
using System.Collections.Generic;

namespace OndoStatisticsWebjob_2._0.Entities
{
    class TradeUnionEntity
    {
        public int ondoId { get; set; }
        public string title { get; set; }
        public string profilePicture { get; set; }
        public int transactionsQuarter { get; set; }
        public int transactionsWeekShops { get; set; }
        public int subscriptionsQuarter { get; set; }
        public int subscriptionsQuarterShops { get; set; }
        public int subscriptionsQuarterClubs { get; set; }
        public int subscriptionsWeekShops { get; set; }
        public int activityQuarter { get; set; }
        public int activityQuarterShops { get; set; }
        public int activityWeekShops { get; set; }
        public int pointsQuarter { get; set; }
        public int pointsWeekShops { get; set; }
        public List<TopFiveShopsDTO> topFiveShops { get; set; }
        public List<WeekData> weeksArrayQuarter { get; set; }
        public int cardUsersQuarter { get; set; }
        public int cardUsersQuarterPercent { get; set; }
        public int activeCardUsersQuarter { get; set; }
        public int activeCardUsersQuarterPercent { get; set; }
        public int inactiveCardUsersQuarter { get; set; }
        public int inactiveCardUsersQuarterPercent { get; set; }
        public int appUsersQuarter { get; set; }
        public int appUsersQuarterPercent { get; set; }
        public int cardUsersWithNoClub { get; set; }
        public int cardUsersWithNoClubPercent { get; set; }
        public int pointsToClubs { get; set; }
        public int pointsNoClubs { get; set; }
        public int appUsersQuarterInCity { get; set; }
        public int cardUsersQuarterInCity { get; set; }
        public int activeCardUsersQuarterInCity { get; set; }
        public int inactiveCardUsersQuarterInCity { get; set; }
        public int clubsCountWithSubscriptions { get; set; }
        public List<ShopDTO> shops { get; set; }
        public List<ClubDTO> clubs { get; set; }
        public List<QuarterHistory> history { get; set; }
        public List<UserHistory> userHistory { get; set; }
    }
}
