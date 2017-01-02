using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblBenefitImages
    {
        public int Id { get; set; }
        public int? Type { get; set; }
        public int? BenefitType { get; set; }
        public int? Count { get; set; }
        public string Picture { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
    }
}
