using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblCustomImages
    {
        public int Id { get; set; }
        public int? BenefitId { get; set; }
        public string Uri { get; set; }
        public int? IndexId { get; set; }
        public int? Heigth { get; set; }
        public int? Width { get; set; }
    }
}
