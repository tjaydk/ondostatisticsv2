using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblEncryptedCards
    {
        public int Id { get; set; }
        public int? BenefitId { get; set; }
        public string CardNr { get; set; }
        public string UnCrypt { get; set; }
        public string Picture { get; set; }
        public string CityCode { get; set; }
        public int Status { get; set; }
    }
}
