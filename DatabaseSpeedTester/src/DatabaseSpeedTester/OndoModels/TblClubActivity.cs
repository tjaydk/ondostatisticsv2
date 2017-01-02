using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblClubActivity
    {
        public int ActivityId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? OndoId { get; set; }
        public int? Amount { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ClubOndoId { get; set; }
    }
}
