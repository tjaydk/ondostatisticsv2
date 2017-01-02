using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblInformationLevel
    {
        public int Id { get; set; }
        public int? OndoId { get; set; }
        public int? ActionByte { get; set; }
    }
}
