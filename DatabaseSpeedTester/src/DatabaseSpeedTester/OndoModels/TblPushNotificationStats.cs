using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblPushNotificationStats
    {
        public int Id { get; set; }
        public int? Type { get; set; }
        public int? ParentId { get; set; }
        public int? Total { get; set; }
        public int? Failed { get; set; }
    }
}
