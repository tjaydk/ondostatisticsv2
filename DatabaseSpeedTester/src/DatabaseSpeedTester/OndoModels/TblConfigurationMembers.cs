using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblConfigurationMembers
    {
        public int Id { get; set; }
        public int? OndoId { get; set; }
        public int? UserId { get; set; }
        public int? AppId { get; set; }
    }
}
