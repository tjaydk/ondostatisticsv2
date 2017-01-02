using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblUserTransactions
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? BenefitId { get; set; }
        public DateTime? DateTime { get; set; }
        public int? Points { get; set; }
        public int? OndoId { get; set; }
        public int? Balance { get; set; }
        public string CardNr { get; set; }
        public int? AdminId { get; set; }
        public string Text { get; set; }
        public int ActivityId { get; set; }
        public string Reference { get; set; }
    }
}
