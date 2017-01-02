using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblUsersToWelcome
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? OndoId { get; set; }
        public int? PostId { get; set; }
        public int? TimeSpanMinutes { get; set; }
        public DateTime? DateTime { get; set; }
    }
}
