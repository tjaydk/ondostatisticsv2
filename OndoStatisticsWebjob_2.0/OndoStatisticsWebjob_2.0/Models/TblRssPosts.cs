using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblRssPosts
    {
        public int Id { get; set; }
        public int? OndoId { get; set; }
        public string Rssid { get; set; }
        public string StoryUrl { get; set; }
        public DateTime? DateTime { get; set; }
    }
}
