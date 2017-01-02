using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.Models
{
    public partial class TblClubData
    {
        public int OndoId { get; set; }
        public string Title { get; set; }
        public string ProfilePicture { get; set; }
        public int Prognose { get; set; }
        public int EstimatedPrognose { get; set; }
        public int EightyPercentPrognose { get; set; }
        public int PercentActiveUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int InactiveUsers { get; set; }
        public int PercentAppUsers { get; set; }
        public int NoAppUsers { get; set; }
        public int AppUsers { get; set; }
        public int Target { get; set; }
        public int DaysLeftInQuarter { get; set; }
        public int EightyPercentEstimatedPrognose { get; set; }
        public int Q1 { get; set; }
        public int Q2 { get; set; }
        public int Q3 { get; set; }
        public int Q4 { get; set; }
        public string Q1label { get; set; }
        public string Q2label { get; set; }
        public string Q3label { get; set; }
        public string Q4label { get; set; }
    }
}
