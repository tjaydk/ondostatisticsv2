using OndoStatisticsWebjob_2._0.Entities.shopEntities;
using System.Collections.Generic;

namespace OndoStatisticsWebjob_2._0.Entities
{
    class ShopEntity
    {
        public int ondoId { get; set; }
        public string title { get; set; }
        public string profilePicture { get; set; }
        public int transactionsCurrentQuarterShop { get; set; }
        public int transactionsCurrentWeekShop { get; set; }
        public int subscriptionsCurrentQuarterShop { get; set; }
        public int subscriptionsCurrentWeekShop { get; set; }
        public int transactionsCurrentQuarterCity { get; set; }
        public int transactionsCurrentWeekCity { get; set; }
        public int subscriptionsCurrentQuarterCity { get; set; }
        public int subscriptionsCurrentWeekCity { get; set; }
        public int subsriptionsQuarterCityAvg { get; set; }
        public int transactionsQuarterCityAvg { get; set; }
        public int pointsCurrentQuarter { get; set; }
        public int weekNo { get; set; }
        public List<WeekData> weekDataArray { get; set; }
        public List<DailyData> dailyDataArray { get; set; }
        public List<HistoryData> historyDataArray { get; set; }
    }
}
