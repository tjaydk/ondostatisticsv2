using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblBenefitPrograms
    {
        public int Id { get; set; }
        public int? BenefitId { get; set; }
        public int? Type { get; set; }
        public decimal? PointValue { get; set; }
        public string Currency { get; set; }
        public int? ValidDays { get; set; }
        public int? BenefitType { get; set; }
        public decimal? SheetValue { get; set; }
        public string SenderId { get; set; }
        public string Link { get; set; }
        public int? CanDelete { get; set; }
        public int? BarcodeLength { get; set; }
        public int? ActionByte { get; set; }
        public string WalletUri { get; set; }
        public string CodeRangeFrom { get; set; }
        public string CodeRangeTo { get; set; }
        public string PercentString { get; set; }
        public int? TransactionWarningLevel { get; set; }
        public int? TransactionMaxLevel { get; set; }
        public int? UserToWarn { get; set; }
        public string CardLengthByte { get; set; }
        public string CurrentEcard { get; set; }
        public string CardPrice { get; set; }
        public string EcardPrice { get; set; }
        public string TransactionPrice { get; set; }
        public int MaxTransactions { get; set; }
        public int MinTransactions { get; set; }
    }
}
