using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblBenefitProgramRelation
    {
        public int Id { get; set; }
        public int? BenefitId { get; set; }
        public int? OndoId { get; set; }
        public int? Status { get; set; }
        public int? PermitsByte { get; set; }
        public string PercentString { get; set; }
        public int ActionByte { get; set; }
    }
}
