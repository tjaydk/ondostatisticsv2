using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblComments
    {
        public int CommentId { get; set; }
        public int? ForeignId { get; set; }
        public int? ForeignIdType { get; set; }
        public int? RecordType { get; set; }
        public int? UserId { get; set; }
        public int? FromUser { get; set; }
        public string GeoTag { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }
        public DateTime? Time { get; set; }
        public string Text { get; set; }
        public int? Voting { get; set; }
        public int? Visibility { get; set; }
        public int? Community { get; set; }
        public int? InfoByte { get; set; }
        public string MiniPicture { get; set; }
        public int? Type { get; set; }
        public int? BenefitId { get; set; }
        public int? ContWidth { get; set; }
        public int? ContHeight { get; set; }
        public string Title { get; set; }
        public string NotStatus { get; set; }
    }
}
