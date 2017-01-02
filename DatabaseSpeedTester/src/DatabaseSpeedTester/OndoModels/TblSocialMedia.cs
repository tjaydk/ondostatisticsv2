using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblSocialMedia
    {
        public int SocialId { get; set; }
        public int? Type { get; set; }
        public string AccessToken { get; set; }
        public DateTime? Expires { get; set; }
        public int? InUse { get; set; }
        public int? OndoId { get; set; }
        public string PageId { get; set; }
        public string AccessSecret { get; set; }
        public string Title { get; set; }
        public DateTime? DateTime { get; set; }
        public string Reason { get; set; }
    }
}
