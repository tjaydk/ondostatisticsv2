using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblGameStatistics
    {
        public int StatisticsId { get; set; }
        public int? BenefitId { get; set; }
        public int? Count { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? State { get; set; }
    }
}
