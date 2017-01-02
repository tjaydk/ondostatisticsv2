namespace DatabaseSpeedTester.ServiceModels.clubEntities
{
    public class ClubQuarterHistory
    {
        public ClubQuarterHistory(string quarterDate, int points)
        {
            this.quarterDate = quarterDate;
            this.points = points;
        }

        public string quarterDate { get; set; }
        public int points { get; set; }
    }
}
