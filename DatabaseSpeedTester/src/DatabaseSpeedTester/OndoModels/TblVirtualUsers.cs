using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblVirtualUsers
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? InfoByte { get; set; }
        public int? FromUserId { get; set; }
    }
}
