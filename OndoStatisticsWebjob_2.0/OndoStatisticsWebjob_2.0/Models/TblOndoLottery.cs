namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblOndoLottery
    {
        public int Id { get; set; }
        public int? PriceId { get; set; }
        public string Title { get; set; }
        public string WinnerText { get; set; }
        public string Conditions { get; set; }
        public string Picture { get; set; }
        public string Text { get; set; }
        public string SocialMediaPicture { get; set; }
    }
}
