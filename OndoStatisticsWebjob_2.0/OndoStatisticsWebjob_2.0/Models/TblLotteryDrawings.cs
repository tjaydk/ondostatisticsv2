using System;

namespace OndoStatisticsWebjob_2._0.Models
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
