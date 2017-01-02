using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblEconomicsRelation
    {
        public int Id { get; set; }
        public int EconomicsId { get; set; }
        public int OndoId { get; set; }
        public int BenefitId { get; set; }
        public string Name { get; set; }
        public string CardPrice { get; set; }
        public string EcardPrize { get; set; }
        public int EcardCount { get; set; }
        public string TransactionPrice { get; set; }
        public int MaxTransactions { get; set; }
        public int Transactions { get; set; }
        public string PointValue { get; set; }
        public string TransferAmount { get; set; }
        public int Status { get; set; }
        public int Points { get; set; }
        public int CardCount { get; set; }
        public int InvoicedTransactions { get; set; }
        public int OnlineTransactions { get; set; }
        public string AmountCharge { get; set; }
        public string GamePrice { get; set; }
        public int CountAmountCharge { get; set; }
    }
}
