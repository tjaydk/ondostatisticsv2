using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblBenefit
    {
        public int BenefitId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Picture { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Conditions { get; set; }
        public int? Type { get; set; }
        public string WinnerText { get; set; }
        public int? NoOfWinners { get; set; }
        public int? Status { get; set; }
        public string AccountId { get; set; }
        public string Stamp { get; set; }
        public string MiniPicture { get; set; }
        public int? Participants { get; set; }
        public int? PicWidth { get; set; }
        public int? PicHeight { get; set; }
        public string NewsLetterPicture { get; set; }
        public int? Items { get; set; }
        public int? Chances { get; set; }
        public int? Curfew { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? Community { get; set; }
        public int? PriceId { get; set; }
        public int? OnlyImage { get; set; }
        public int? PostId { get; set; }
        public int? Distance { get; set; }
        public int? ActionByte { get; set; }
        public int? CategoryByte { get; set; }
        public string StampBackground { get; set; }
        public string OriginalPicture { get; set; }
        public string SocialMediaString { get; set; }
        public int OndoRef { get; set; }
        public int NormalPrice { get; set; }
        public int OfferPrice { get; set; }
        public int Bonus { get; set; }
    }
}
