using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblEncryptedCardsAdmin
    {
        public int Id { get; set; }
        public int? BenefitId { get; set; }
        public string City { get; set; }
        public string CityCode { get; set; }
        public int? RangeFrom { get; set; }
        public int? RangeTo { get; set; }
    }
}
