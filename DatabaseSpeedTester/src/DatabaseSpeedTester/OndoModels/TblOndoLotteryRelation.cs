using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblOndoLotteryRelation
    {
        public int OndoLotteryRelationId { get; set; }
        public int? OndoId { get; set; }
        public int? BenefitId { get; set; }
        public int? Month { get; set; }
    }
}
