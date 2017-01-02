using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblPlannedPosts
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int? Community { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? IsSent { get; set; }
        public string Content { get; set; }
        public string MiniPicture { get; set; }
        public int? UserId { get; set; }
        public int? ContHeight { get; set; }
        public int? ContWidth { get; set; }
        public int? OndoId { get; set; }
        public int? PostId { get; set; }
        public int? Type { get; set; }
        public int? Editable { get; set; }
        public int? SocialMedia { get; set; }
        public int? PostType { get; set; }
        public int? SocialStatement { get; set; }
        public int? BenefitId { get; set; }
        public int? CategoryByte { get; set; }
        public string SocialMediaString { get; set; }
    }
}
