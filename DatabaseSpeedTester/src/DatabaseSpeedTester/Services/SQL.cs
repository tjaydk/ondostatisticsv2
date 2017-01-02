using DatabaseSpeedTester.Models;
using DatabaseSpeedTester.ServiceModels;
using DatabaseSpeedTester.ServiceModels.clubEntities;
using System.Linq;

namespace DatabaseSpeedTester.Services
{
    public class SQL
    {
        /// <summary>
        /// Returns a club by ondoId as ClubEntity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClubEntity getClubById(int id)
        {
            using (calculatedFiguresContext context = new calculatedFiguresContext())
            {
                ClubEntity club                         = new ClubEntity();
                TblClubData tblclub                     = context.TblClubData.Where(c => c.OndoId == id).FirstOrDefault();

                club.activeUsers                        = tblclub.ActiveUsers;
                club.appUsers                           = tblclub.AppUsers;
                club.daysLeftInQuarter                  = tblclub.DaysLeftInQuarter;
                club.eightyPercentEstimatedPrognose     = tblclub.EightyPercentEstimatedPrognose;
                club.eightyPercentPrognose              = tblclub.EightyPercentPrognose;
                club.estimatedPrognose                  = tblclub.EstimatedPrognose;
                club.inactiveUsers                      = tblclub.InactiveUsers;
                club.noAppUsers                         = tblclub.NoAppUsers;
                club.OndoId                             = tblclub.OndoId;
                club.percentActiveUsers                 = tblclub.PercentActiveUsers;
                club.percentAppUsers                    = tblclub.PercentAppUsers;
                club.ProfilePicture                     = tblclub.ProfilePicture;
                club.prognose                           = tblclub.Prognose;
                club.target                             = tblclub.Target;
                club.Title                              = tblclub.Title;
                club.history                            = new ClubQuarterHistory[]{
                    new ClubQuarterHistory(tblclub.Q1label, tblclub.Q1),
                    new ClubQuarterHistory(tblclub.Q2label, tblclub.Q2),
                    new ClubQuarterHistory(tblclub.Q3label, tblclub.Q3),
                    new ClubQuarterHistory(tblclub.Q4label, tblclub.Q4)
                };

                return club;
            }
        }
    }
}
