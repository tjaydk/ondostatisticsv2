using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblHubNotifications
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? AppId { get; set; }
        public string Posts { get; set; }
        public DateTime? DateTime { get; set; }
        public string Comments { get; set; }
    }
}
