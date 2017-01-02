using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblBenefitRelation
    {
        public int RelationId { get; set; }
        public int? OndoId { get; set; }
        public int? BenefitId { get; set; }
    }
}
