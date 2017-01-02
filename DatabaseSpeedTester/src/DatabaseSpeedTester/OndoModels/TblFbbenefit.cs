using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblFbbenefit
    {
        public int Id { get; set; }
        public int? BenefitId { get; set; }
        public string Fbpost { get; set; }
        public int? OndoId { get; set; }
    }
}
