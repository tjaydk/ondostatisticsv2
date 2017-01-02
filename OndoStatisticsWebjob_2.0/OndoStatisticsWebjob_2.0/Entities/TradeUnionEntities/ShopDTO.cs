using OndoStatisticsWebjob_2._0.Entities.shopEntities;
using System.Collections.Generic;

namespace OndoStatisticsWebjob_2._0.Entities.TradeUnionEntities
{
    class ShopDTO
    {
        public string title { get; set; }
        public string profilePicture { get; set; }
        public string category { get; set; }
        public int transactions { get; set; }
        public int subscriptions { get; set; }
        public int points { get; set; }
        public List<WeekData> weeksArray { get; set; }
    }
}
