using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblUserLocation
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? Time { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
