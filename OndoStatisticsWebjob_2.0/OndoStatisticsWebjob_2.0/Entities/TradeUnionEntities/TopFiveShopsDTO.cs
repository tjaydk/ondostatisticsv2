namespace OndoStatisticsWebjob_2._0.Entities.TradeUnionEntities
{
    class TopFiveShopsDTO
    {
        public string title { get; set; }
        public string profilePicture { get; set; }
        public int score { get; set; }

        public TopFiveShopsDTO(string title, string profilePicture, int score)
        {
            this.title = title;
            this.profilePicture = profilePicture;
            this.score = score;
        }
    }
}
