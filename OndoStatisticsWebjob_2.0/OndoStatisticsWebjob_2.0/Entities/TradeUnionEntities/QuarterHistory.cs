using System.Collections.Generic;

namespace OndoStatisticsWebjob_2._0.Entities.TradeUnionEntities
{
    class QuarterHistory
    {
        public string quarterLabel { get; set; }
        public int subscriptions { get; set; }
        public int transactions { get; set; }
        public int points { get; set; }
        public List<ShopDTO> shops { get; set; }
    }
}
