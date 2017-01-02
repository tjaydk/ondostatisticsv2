using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblSettlements
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public DateTime? DateTime { get; set; }
        public int Points { get; set; }
        public int Quarter { get; set; }
        public int Year { get; set; }
        public int OndoId { get; set; }
        public int ClubOndoId { get; set; }
    }
}
