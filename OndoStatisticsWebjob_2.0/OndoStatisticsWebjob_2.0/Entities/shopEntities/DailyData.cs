namespace OndoStatisticsWebjob_2._0.Entities.shopEntities
{
    class DailyData
    {
        public string date { get; set; }
        public int subscriptions { get; set; }
        public int transactions { get; set; }

        public DailyData()
        {

        }

        public DailyData(string date, int subscriptions, int transactions)
        {
            this.date = date;
            this.subscriptions = subscriptions;
            this.transactions = transactions;
        }
    }
}
