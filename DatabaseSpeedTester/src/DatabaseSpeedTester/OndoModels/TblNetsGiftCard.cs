using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
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
