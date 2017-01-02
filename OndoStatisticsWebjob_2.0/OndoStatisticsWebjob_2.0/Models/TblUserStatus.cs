using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblUserStatus
    {
        public int UserStatusIndex { get; set; }
        public int? UserId { get; set; }
        public int? OndoId { get; set; }
        public int? CurrentStatus { get; set; }
        public int? LoyaltyScore { get; set; }
        public DateTime? DateTime { get; set; }
        public int? AppId { get; set; }
    }
}
