using DatabaseSpeedTester.OndoModels;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseSpeedTester.Services.Original
{
    class GetDataFromOndoDb
    {
        /// <summary>
        /// Retrieves and returns a list of all clubs from the ondo database
        /// </summary>
        /// <returns></returns>
        public static List<TblOndos> getClubs()
        {
            using (var context = new OndoDB_2013_3_4_8_15_2016_11_7_18_26Context())
            {
                return context.TblOndos
                    //If MapIcon is 06 the ondo is a club
                    .Where(o => o.AppId == 5 && o.MapIcon != null && o.MapIcon.Length > 2 && o.MapIcon.Substring(0, 2).Equals("06"))
                    .ToList();
            }
        }

        /// <summary>
        /// Retrieves and returns a TblOndos from a specific club ondoId
        /// </summary>
        /// <param name="ondoId"></param>
        /// <returns></returns>
        public static TblOndos getClub(int ondoId)
        {
            using (var context = new OndoDB_2013_3_4_8_15_2016_11_7_18_26Context())
            {
                return context.TblOndos.Where(o => o.OndoId == ondoId).FirstOrDefault();
            }
        }

        /// <summary>
        /// Returns list of all shops as TblOndos
        /// </summary>
        /// <returns></returns>
        public static List<TblOndos> getShops()
        {
            using (var context = new OndoDB_2013_3_4_8_15_2016_11_7_18_26Context())
            {
                return context.TblOndos
                    //If MapIcon is not 01 or 06 the ondo is a shop
                    .Where(o => o.AppId == 5 && o.MapIcon != null && o.MapIcon.Length > 2 && !o.MapIcon.Substring(0, 2).Equals("01") && !o.MapIcon.Substring(0, 2).Equals("06"))
                    .ToList();
            }
        }

        /// <summary>
        /// Returns list of all tradeunions as TblOndos
        /// </summary>
        /// <returns></returns>
        public static List<TblOndos> getTradeUnisons()
        {
            using (var context = new OndoDB_2013_3_4_8_15_2016_11_7_18_26Context())
            {
                return context.TblOndos
                    //If MapIcon is 01 the ondo is a tradeunion
                    .Where(o => o.AppId == 5 && o.MapIcon != null && o.MapIcon.Length > 2 && o.MapIcon.Substring(0, 2).Equals("01"))
                    .ToList();
            }
        }

        /// <summary>
        /// Returns a list of all club activities as TblClubActivity objects
        /// </summary>
        /// <returns></returns>
        public static List<TblClubActivity> getClubActivities()
        {
            using (var context = new OndoDB_2013_3_4_8_15_2016_11_7_18_26Context())
            {
                return context.TblClubActivity.ToList();
            }
        }

        /// <summary>
        /// Returns a list of users balance cards as TblUserBalanceCard
        /// </summary>
        /// <returns></returns>
        public static List<TblUserBalanceCard> getUserBalanceCards()
        {
            using (var context = new OndoDB_2013_3_4_8_15_2016_11_7_18_26Context())
            {
                return context.TblUserBalanceCard
                    //BenefitId 14341 is LokalFordele
                    .Where(o => o.BenefitId == 14341)
                    .ToList();
            }
        }

        /// <summary>
        /// Returns list of users as TblUsers
        /// </summary>
        /// <returns></returns>
        public static List<TblUsers> getUsers()
        {
            using (var context = new OndoDB_2013_3_4_8_15_2016_11_7_18_26Context())
            {
                // TblConfigurationMembers is joined with TblUsers to check if the appId is 5, and thereby LokalFordele
                List<TblUsers> userList = context.TblConfigurationMembers
                    .Where(o => o.AppId == 5)
                    .Join(context.TblUsers,
                    o => o.UserId,
                    ok => ok.UserId,
                    (o, ok) => new TblUsers
                    {
                        UserId = ok.UserId,
                        Login = ok.Login,
                        Password = ok.Password,
                        FirstName = ok.FirstName,
                        LastName = ok.LastName,
                        Picture = ok.Picture,
                        Gender = ok.Gender,
                        Auth = ok.Auth,
                        BirthYear = ok.BirthYear,
                        PushUri = ok.PushUri,
                        UserName = ok.UserName,
                        CreateTime = ok.CreateTime,
                        InformationLevel = ok.InformationLevel,
                        AccessToken = ok.AccessToken,
                        Subscription = ok.Subscription,
                        Phone = ok.Phone,
                        Culture = ok.Culture,
                        IsVirtual = ok.IsVirtual,
                        ActionByte = ok.ActionByte,
                        PhonePrefix = ok.PhonePrefix
                    }).Distinct().ToList(); // use distinct to remove duplicates
                return userList;
            }
        }

        /// <summary>
        /// Returns list of settlements as TblSettlements
        /// </summary>
        /// <returns></returns>
        public static List<TblSettlements> getSettlements()
        {
            using (var context = new OndoDB_2013_3_4_8_15_2016_11_7_18_26Context())
            {
                return context.TblSettlements
                    .ToList();
            }
        }
    }
}
