﻿using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblPostNotifications
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? PostId { get; set; }
        public int? OndoId { get; set; }
        public DateTime? DateTime { get; set; }
        public int? Number { get; set; }
        public int? AppId { get; set; }
    }
}
