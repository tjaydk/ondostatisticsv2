using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblTrace
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int? TraceMinutes { get; set; }
        public DateTime? Time { get; set; }
    }
}
