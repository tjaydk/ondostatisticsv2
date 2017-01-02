using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblDropsterLog
    {
        public int Id { get; set; }
        public int? AdminId { get; set; }
        public DateTime? DateTime { get; set; }
        public int? ServerFunction { get; set; }
        public string Text { get; set; }
        public int? Result { get; set; }
        public int? OndoId { get; set; }
        public int? UserId { get; set; }
    }
}
