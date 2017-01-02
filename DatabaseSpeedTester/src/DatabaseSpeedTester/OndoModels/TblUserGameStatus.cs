using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblUserGameStatus
    {
        public int GameId { get; set; }
        public int BenefitId { get; set; }
        public int? UserId { get; set; }
        public int? RemainTurns { get; set; }
        public int? Balance { get; set; }
        public DateTime? DateTime { get; set; }
        public int? Choice { get; set; }
        public int? GameStatus { get; set; }
        public int? LastScore { get; set; }
        public int? Collected { get; set; }
    }
}
