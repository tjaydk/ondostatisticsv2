using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblSms
    {
        public int Id { get; set; }
        public int? OndoId { get; set; }
        public int? BenefitId { get; set; }
        public int? Count { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
    }
}
