using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblAdmin
    {
        public int AdminId { get; set; }
        public DateTime? TimeStamp { get; set; }
        public string GenerateTime { get; set; }
    }
}
