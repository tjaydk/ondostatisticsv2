using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblDropsterUser
    {
        public int UserId { get; set; }
        public string Phone { get; set; }
        public string PhonePrefix { get; set; }
        public string UserName { get; set; }
        public string Picture { get; set; }
        public string Culture { get; set; }
        public DateTime? DateTime { get; set; }
        public string PushChannel { get; set; }
        public string PhoneBarCode { get; set; }
        public string Region { get; set; }
        public string Code { get; set; }
        public int? Status { get; set; }
    }
}
