using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblUserVouchers
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BenefitId { get; set; }
        public DateTime? IssuedDateTime { get; set; }
        public int Status { get; set; }
        public DateTime? DueDateTime { get; set; }
        public DateTime? TriggerDateTime { get; set; }
        public int AppId { get; set; }
        public int Information { get; set; }
        public int IssueReason { get; set; }
    }
}
