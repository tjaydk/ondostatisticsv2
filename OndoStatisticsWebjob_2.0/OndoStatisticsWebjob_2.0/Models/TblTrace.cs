using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblTrace
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int? TraceMinutes { get; set; }
        public DateTime? Time { get; set; }
    }
}
