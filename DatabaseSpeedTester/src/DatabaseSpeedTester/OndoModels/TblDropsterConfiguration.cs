﻿using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblDropsterConfiguration
    {
        public int Id { get; set; }
        public int? OndoId { get; set; }
        public decimal? Jacketprice { get; set; }
        public decimal? BagPrice { get; set; }
        public string MerchantId { get; set; }
        public string Culture { get; set; }
    }
}
