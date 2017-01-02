using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblReservedTransactions
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OndoId { get; set; }
        public int BenefitId { get; set; }
        public DateTime? DateTime { get; set; }
        public string CardNr { get; set; }
        public int OrderId { get; set; }
        public int Points { get; set; }
        public string Phone { get; set; }
        public int Status { get; set; }
    }
}
