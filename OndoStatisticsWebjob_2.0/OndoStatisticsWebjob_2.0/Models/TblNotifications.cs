using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblNotifications
    {
        public int NotificationId { get; set; }
        public int? UserId { get; set; }
        public int? OndoId { get; set; }
        public int? NewPosts { get; set; }
        public int? NewComments { get; set; }
        public DateTime? Time { get; set; }
        public string Text { get; set; }
        public int? ForeignId { get; set; }
        public int? ForeginIdType { get; set; }
        public int? Number { get; set; }
    }
}
