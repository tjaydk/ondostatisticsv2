using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblUserStatistics
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? Time { get; set; }
        public int? Action { get; set; }
    }
}
