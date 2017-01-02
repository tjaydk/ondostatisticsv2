using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblDropsterDevices
    {
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public int? Count { get; set; }
        public int? UserId { get; set; }
    }
}
