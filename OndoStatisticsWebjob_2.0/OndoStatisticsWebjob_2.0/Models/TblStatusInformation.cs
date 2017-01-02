﻿using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblStatusInformation
    {
        public int Id { get; set; }
        public int OndoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public DateTime? DateTime { get; set; }
        public int Status { get; set; }
    }
}
