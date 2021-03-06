﻿using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblUserVerification
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? Time { get; set; }
        public string Code { get; set; }
        public string Phone { get; set; }
        public string CardNr { get; set; }
        public int? RunVerification { get; set; }
        public int? BenefitId { get; set; }
    }
}
