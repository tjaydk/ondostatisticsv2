using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblBenefitProposals
    {
        public int BenefitProposalId { get; set; }
        public string Title { get; set; }
        public string Picture { get; set; }
        public string MiniPicture { get; set; }
        public string Text { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Conditions { get; set; }
        public int NormalPrice { get; set; }
        public int OfferPrice { get; set; }
        public int Bonus { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string OriginalPicture { get; set; }
        public int OndoId { get; set; }
        public int Label { get; set; }
    }
}
