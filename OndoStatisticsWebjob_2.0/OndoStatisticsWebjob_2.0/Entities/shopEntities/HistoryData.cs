using System.Collections.Generic;

namespace OndoStatisticsWebjob_2._0.Entities.shopEntities
{
    class HistoryData
    {
        public string quarterLabel { get; set; }
        public int subscriptions { get; set; }
        public int transactions { get; set; }
        public int points { get; set; }
        public List<WeekData> historyWeekDataArray { get; set; }

        public HistoryData()
        {

        }

        public HistoryData(List<WeekData> historyWeekDataArray)
        {
            this.historyWeekDataArray = historyWeekDataArray;
        }

        public HistoryData(string quarterLabel, int subscriptions, int transactions, int points, List<WeekData> historyWeekDataArray)
        {
            this.quarterLabel = quarterLabel;
            this.subscriptions = subscriptions;
            this.transactions = transactions;
            this.points = points;
            this.historyWeekDataArray = historyWeekDataArray;
        }
    }
}
