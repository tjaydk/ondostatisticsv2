﻿using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblPasswordRequest
    {
        public int Id { get; set; }
        public DateTime? DateTime { get; set; }
        public string Guid { get; set; }
        public int? UserId { get; set; }
        public int? AppId { get; set; }
        public string AppName { get; set; }
    }
}