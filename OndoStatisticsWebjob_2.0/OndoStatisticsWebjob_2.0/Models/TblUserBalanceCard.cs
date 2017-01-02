using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblUserBalanceCard
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? BenefitId { get; set; }
        public int? Balance { get; set; }
        public DateTime? DateTime { get; set; }
        public string CardNr { get; set; }
        public string BarCodePicture { get; set; }
        public int? StatusByte { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public int? ActivityId { get; set; }
        public int Status { get; set; }
    }
}
