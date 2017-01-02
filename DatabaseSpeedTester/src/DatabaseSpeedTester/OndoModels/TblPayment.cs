using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblPayment
    {
        public int Id { get; set; }
        public int? OndoId { get; set; }
        public int? Bonus { get; set; }
        public int? BenefitProgram { get; set; }
        public string ReceiptMessage { get; set; }
        public int? BenefitId { get; set; }
        public int? Amount { get; set; }
        public int? BonusPercent { get; set; }
        public int AmountCharge { get; set; }
    }
}
