using System;

namespace OndoStatisticsWebjob_2._0.Models
{
    public partial class TblOndos
    {
        public int OndoId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? Type { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? Visibility { get; set; }
        public int? InitiatorHandovers { get; set; }
        public int? ParticipantHandovers { get; set; }
        public int? ReceiveTimes { get; set; }
        public int? Initiator { get; set; }
        public int? Status { get; set; }
        public int? Community { get; set; }
        public string Locality { get; set; }
        public string Country { get; set; }
        public string StatusMessage { get; set; }
        public string MiniPicture { get; set; }
        public int? VotingScale { get; set; }
        public string BarcodeFormat { get; set; }
        public string Barcode { get; set; }
        public string GeoTag { get; set; }
        public string Device { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }
        public string Link { get; set; }
        public int? TagAction { get; set; }
        public int? ParAction { get; set; }
        public int? Splash { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public int? ContWidth { get; set; }
        public int? ContHeight { get; set; }
        public string MapTag { get; set; }
        public string Qrurl { get; set; }
        public string NewsLetterPicture { get; set; }
        public string Sponsor { get; set; }
        public string ProfileName { get; set; }
        public string ProfilePicture { get; set; }
        public int? ParentType { get; set; }
        public string MapIcon { get; set; }
        public int? CategoryByte { get; set; }
        public int? AppId { get; set; }
        public int? ActionByte { get; set; }
        public string OriginalMiniPicture { get; set; }
    }
}
