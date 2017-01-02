namespace OndoStatisticsWebjob_2._0.Entities.TradeUnionEntities
{
    class UserHistory
    {
        public string quarterLabel { get; set; }
        public int cardUsers { get; set; }
        public int cardUsersPercent { get; set; }
        public int activeCardUsers { get; set; }
        public int activeCardUsersPercent { get; set; }
        public int inactiveCardUsers { get; set; }
        public int inactiveCardUsersPercent { get; set; }
        public int appUsers { get; set; }
        public int appUsersPercent { get; set; }
        public int cardUsersWithNoClub { get; set; }
        public int cardUsersWithNoClubPercent { get; set; }
    }
}
