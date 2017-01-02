using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblDiceStatistics
    {
        public int Id { get; set; }
        public int? BenefitId { get; set; }
        public int? One { get; set; }
        public int? Two { get; set; }
        public int? Three { get; set; }
        public int? Four { get; set; }
        public int? Five { get; set; }
        public int? Six { get; set; }
    }
}
