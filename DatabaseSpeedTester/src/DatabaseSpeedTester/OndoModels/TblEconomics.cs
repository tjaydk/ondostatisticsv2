using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblEconomics
    {
        public int Id { get; set; }
        public string Cvr { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int Payments { get; set; }
        public string Fee { get; set; }
        public string EarnedPoints { get; set; }
        public int Transactions { get; set; }
        public int OndoId { get; set; }
        public string TransferAmount { get; set; }
        public int Week { get; set; }
        public int Year { get; set; }
        public string FeePercent { get; set; }
        public string FeeMin { get; set; }
        public string TotalAmount { get; set; }
        public DateTime? DateTime { get; set; }
        public int Status { get; set; }
        public string SumFrom { get; set; }
        public int EcardCountTotal { get; set; }
        public int TotalEarnedPoints { get; set; }
        public int CardCountTotal { get; set; }
        public int InvoicedTransactions { get; set; }
        public int TotalOnlineTransactions { get; set; }
        public string TotalAmountCharge { get; set; }
    }
}
