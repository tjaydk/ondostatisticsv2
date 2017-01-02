using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblAutoPost
    {
        public int Id { get; set; }
        public int? BenefitId { get; set; }
        public int? Status { get; set; }
        public DateTime? Time { get; set; }
    }
}
