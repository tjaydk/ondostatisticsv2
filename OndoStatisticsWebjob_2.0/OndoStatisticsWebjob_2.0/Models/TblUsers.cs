using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblUsers
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Picture { get; set; }
        public int? Gender { get; set; }
        public int? Auth { get; set; }
        public int? BirthYear { get; set; }
        public string PushUri { get; set; }
        public string UserName { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? InformationLevel { get; set; }
        public string AccessToken { get; set; }
        public int? Subscription { get; set; }
        public string Phone { get; set; }
        public string Culture { get; set; }
        public int? IsVirtual { get; set; }
        public int? ActionByte { get; set; }
        public string PhonePrefix { get; set; }
        public string Token { get; set; }
    }
}
