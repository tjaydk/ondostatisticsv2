using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblBlockedIp
    {
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public string Text { get; set; }
        public DateTime? DateTime { get; set; }
        public int? Count { get; set; }
    }
}
