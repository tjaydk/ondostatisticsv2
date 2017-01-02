using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblFbpost
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Fbpost { get; set; }
        public int? OndoId { get; set; }
        public int PlannedPostId { get; set; }
    }
}
