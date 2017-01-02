namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblPushChannels
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? AppId { get; set; }
        public string PushUri { get; set; }
    }
}
