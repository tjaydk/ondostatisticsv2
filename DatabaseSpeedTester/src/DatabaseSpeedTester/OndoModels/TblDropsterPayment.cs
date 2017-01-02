using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblDropsterPayment
    {
        public int OrderId { get; set; }
        public string MerchantId { get; set; }
        public string TransactionId { get; set; }
        public decimal? Amount { get; set; }
        public int? ErrorCode { get; set; }
        public int? OndoId { get; set; }
        public int? IsSynchronized { get; set; }
        public string ReceiptMessage { get; set; }
        public DateTime? DateTime { get; set; }
        public int? UserId { get; set; }
        public string PayLink { get; set; }
        public string Tickets { get; set; }
        public string Unit { get; set; }
        public int? PaymentType { get; set; }
    }
}
