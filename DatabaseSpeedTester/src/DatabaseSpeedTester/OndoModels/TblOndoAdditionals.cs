using System;
using System.Collections.Generic;

namespace DatabaseSpeedTester.OndoModels
{
    public partial class TblOndoAdditionals
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string District { get; set; }
        public int? LocateFromOndo { get; set; }
        public int? MaxParticipants { get; set; }
        public string QrUri { get; set; }
        public int? OndoId { get; set; }
        public int? SerialNumber { get; set; }
        public string SplashTitle { get; set; }
        public string SplashDescription { get; set; }
        public string SplashPicture { get; set; }
        public int? InfoByte { get; set; }
        public string ContactInfo { get; set; }
        public string Phone { get; set; }
        public string OpeningHoursHeader { get; set; }
        public string OpeningHoursText { get; set; }
        public string City { get; set; }
        public string PdfUrl { get; set; }
        public string PdfPicture { get; set; }
        public string PdfMiniPicture { get; set; }
        public int? RemoteSignup { get; set; }
        public string ShortLink { get; set; }
        public string Badge { get; set; }
        public string ContentWithBadge { get; set; }
        public int? BadgPosition { get; set; }
        public int? UltimateIdType { get; set; }
        public string UltimateText { get; set; }
        public string UltimateLink { get; set; }
        public string UltimatePicture { get; set; }
        public string SocialMediaPrefix { get; set; }
        public string DefaultWelcomeComment { get; set; }
        public string Rssfeed { get; set; }
        public int? Rssinterval { get; set; }
        public DateTime? RsstimeStamp { get; set; }
        public int? ExcludeJoinsInFeed { get; set; }
        public string RssupdateTime { get; set; }
        public string Cvr { get; set; }
    }
}
