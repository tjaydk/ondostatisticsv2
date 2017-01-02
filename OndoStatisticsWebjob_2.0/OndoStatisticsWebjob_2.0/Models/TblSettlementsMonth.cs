using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblSettlementsMonth
    {
        public int Id { get; set; }
        public string ActivityId { get; set; }
        public DateTime? DateTime { get; set; }
        public int Points { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int OndoId { get; set; }
        public int ClubOndoId { get; set; }
    }
}
