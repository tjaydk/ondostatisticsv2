using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblHashTags
    {
        public int Id { get; set; }
        public int? OndoId { get; set; }
        public string Text { get; set; }
    }
}
