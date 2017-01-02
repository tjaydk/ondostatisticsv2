using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblBenefitRelation
    {
        public int RelationId { get; set; }
        public int? OndoId { get; set; }
        public int? BenefitId { get; set; }
    }
}
