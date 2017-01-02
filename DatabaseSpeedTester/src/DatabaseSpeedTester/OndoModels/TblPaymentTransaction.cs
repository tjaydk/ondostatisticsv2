using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblPaymentTransaction
    {
        public int OrderId { get; set; }
        public string MerchantId { get; set; }
        public string TransactionId { get; set; }
        public string Amount { get; set; }
        public int? ErrorCode { get; set; }
        public int? OndoId { get; set; }
        public int? IsSynchronized { get; set; }
        public string ReceiptMessage { get; set; }
        public DateTime? DateTime { get; set; }
        public int? UserId { get; set; }
        public int? BenefitId { get; set; }
        public string Dbstatus { get; set; }
        public DateTime? DbdateTime { get; set; }
    }
}
