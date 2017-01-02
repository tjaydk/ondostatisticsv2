using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblOndoAdminRelation
    {
        public int Id { get; set; }
        public int? OndoId { get; set; }
        public int? UserId { get; set; }
        public int? Role { get; set; }
        public int? AdminRights { get; set; }
    }
}
