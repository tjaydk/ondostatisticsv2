using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblLotteryDrawings
    {
        public int DrawingId { get; set; }
        public int? Id { get; set; }
        public DateTime? DrawingTime { get; set; }
        public string DrawingWinnerText { get; set; }
        public int? DrawingWinnerUserId { get; set; }
        public int? InformWinner { get; set; }
    }
}
