using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblGameStatistics
    {
        public int StatisticsId { get; set; }
        public int? BenefitId { get; set; }
        public int? Count { get; set; }
        public DateTime? TimeStamp { get; set; }
        public int? State { get; set; }
    }
}
