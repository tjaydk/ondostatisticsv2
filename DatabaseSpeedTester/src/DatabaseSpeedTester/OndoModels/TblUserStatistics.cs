using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblUserStatistics
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? Time { get; set; }
        public int? Action { get; set; }
    }
}
