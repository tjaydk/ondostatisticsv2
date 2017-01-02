namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblDropsterRecord
    {
        public int Id { get; set; }
        public int? OndoId { get; set; }
        public int? Count { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public int? Type { get; set; }
    }
}
