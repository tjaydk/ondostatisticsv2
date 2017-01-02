namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblOndoLotteryRelation
    {
        public int OndoLotteryRelationId { get; set; }
        public int? OndoId { get; set; }
        public int? BenefitId { get; set; }
        public int? Month { get; set; }
    }
}
