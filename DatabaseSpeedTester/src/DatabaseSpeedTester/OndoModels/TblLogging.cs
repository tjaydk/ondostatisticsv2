using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblLogging
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? OndoId { get; set; }
        public int? Type { get; set; }
        public string Text { get; set; }
        public string IpAddress { get; set; }
        public DateTime? DateTime { get; set; }
        public int? AppId { get; set; }
    }
}
