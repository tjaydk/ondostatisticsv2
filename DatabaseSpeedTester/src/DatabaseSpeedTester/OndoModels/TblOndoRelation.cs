using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblOndoRelation
    {
        public int RelationId { get; set; }
        public int? AppId { get; set; }
        public int? OndoId { get; set; }
    }
}
