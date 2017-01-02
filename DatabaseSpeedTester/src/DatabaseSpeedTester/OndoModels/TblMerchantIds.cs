using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblMerchantIds
    {
        public int Id { get; set; }
        public int? AppId { get; set; }
        public string MobilePay { get; set; }
        public decimal? MinimumAmount { get; set; }
    }
}
