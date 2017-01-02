using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblUserBalance
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? BenefitId { get; set; }
        public int? Balance { get; set; }
        public DateTime? DateTime { get; set; }
        public string BarCodePicture { get; set; }
    }
}
