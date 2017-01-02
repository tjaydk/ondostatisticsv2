using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblUserSettlementBalance
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CardNr { get; set; }
        public int Balance { get; set; }
        public int BenefitId { get; set; }
    }
}
