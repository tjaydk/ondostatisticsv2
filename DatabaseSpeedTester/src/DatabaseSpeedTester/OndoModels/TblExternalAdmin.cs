using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblExternalAdmin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
        public int OndoId { get; set; }
    }
}
