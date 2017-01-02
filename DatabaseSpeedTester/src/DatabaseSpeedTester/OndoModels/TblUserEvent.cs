using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblUserEvent
    {
        public long EventIndex { get; set; }
        public int? UserId { get; set; }
        public int? OndoId { get; set; }
        public int? EventType { get; set; }
        public DateTime? DateTime { get; set; }
    }
}
