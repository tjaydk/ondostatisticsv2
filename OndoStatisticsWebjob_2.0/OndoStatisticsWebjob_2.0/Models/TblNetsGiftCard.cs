namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblNetsGiftCard
    {
        public int NetsGiftCardId { get; set; }
        public int? UserId { get; set; }
        public string Cardnr { get; set; }
        public string BarCodePicture { get; set; }
        public int? BenefitId { get; set; }
    }
}
