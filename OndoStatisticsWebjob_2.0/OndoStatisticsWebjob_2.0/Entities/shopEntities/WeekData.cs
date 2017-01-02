namespace OndoStatisticsWebjob_2._0.Entities.shopEntities
{
    class WeekData
    {
        public string weekLabel { get; set; }
        public int subscriptions { get; set; }
        public int transactions { get; set; }
        public int activity { get; set; }
        public int points { get; set; }

        public WeekData(string weekLabel, int subscriptions, int transactions, int activity, int points)
        {
            this.weekLabel = weekLabel;
            this.subscriptions = subscriptions;
            this.transactions = transactions;
            this.activity = activity;
            this.points = points;
        }
    }
}
