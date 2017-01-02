using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblAutoPost
    {
        public int Id { get; set; }
        public int? BenefitId { get; set; }
        public int? Status { get; set; }
        public DateTime? Time { get; set; }
    }
}
