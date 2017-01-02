using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblFacebookStatistics
    {
        public int Id { get; set; }
        public int? OndoId { get; set; }
        public int? Likes { get; set; }
        public int? NewLikes { get; set; }
        public int? TalkingAbout { get; set; }
        public int? WereHere { get; set; }
        public DateTime? DateTime { get; set; }
    }
}
