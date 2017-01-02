using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblDropsterTickets
    {
        public int Id { get; set; }
        public int? TicketId { get; set; }
        public int? Type { get; set; }
        public int? UserId { get; set; }
        public DateTime? DateTime { get; set; }
        public int? OndoId { get; set; }
        public string Uri { get; set; }
        public int? Status { get; set; }
        public int? UserIdRequest { get; set; }
    }
}
