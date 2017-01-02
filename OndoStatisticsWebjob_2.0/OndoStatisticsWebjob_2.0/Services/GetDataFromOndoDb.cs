using OndoStatisticsWebjob_2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OndoStatisticsWebjob_2._0.Services
{
    class GetDataFromOndoDb
    {
        public static List<TblOndos> getClubs()
        {
            using (var context = new OndoDB_2013_3_4_8_15Context())
            {
                return context.TblOndos
                    //If MapIcon is 06 the ondo is a club
                    .Where(o => o.AppId == 5 && o.MapIcon != null && o.MapIcon.Length > 2 && o.MapIcon.Substring(0, 2).Equals("06"))
                    .ToList();
            }
        }

        public static List<TblUserTransactions> getTransactions()
        {
            DateTime now = DateTime.Now;
            //Finds how many days there has been in the current quarter
            int datesInCurrentQuarterSoFar = DateTimeConverter.daysInQuarterSoFar(now);
            //Subtracts 3 years from current date.
            DateTime ThreeYearsAgo = now.AddYears(-3);
            //Subtracts days in quarter so far, to make sure that data for full quaters are added
            DateTime transactionPeriod = ThreeYearsAgo.AddDays(-datesInCurrentQuarterSoFar);
            using (var context = new OndoDB_2013_3_4_8_15Context())
            {
                return context.TblUserTransactions
                    .Where(o => o.BenefitId == 14341 && o.DateTime > transactionPeriod)
                    .OrderByDescending(o => o.DateTime)
                    .ToList();
            }
        }

        internal static List<TblEncryptedCards> getEncryptedCards()
        {
            using (var context = new OndoDB_2013_3_4_8_15Context())
            {
                return context.TblEncryptedCards
                    .ToList();
            }
        }

        public static List<TblOndos> getShops()
        {
            using (var context = new OndoDB_2013_3_4_8_15Context())
            {
                return context.TblOndos
                    //If MapIcon is not 01 or 06 the ondo is a shop
                    .Where(o => o.AppId == 5 && o.MapIcon != null && o.MapIcon.Length > 2 && !o.MapIcon.Substring(0, 2).Equals("01") && !o.MapIcon.Substring(0, 2).Equals("06"))
                    .ToList();
            }
        }
        public static List<TblOndos> getTradeUnisons()
        {
            using (var context = new OndoDB_2013_3_4_8_15Context())
            {
                return context.TblOndos
                    //If MapIcon is 01 the ondo is a tradeunion
                    .Where(o => o.AppId == 5 && o.MapIcon != null && o.MapIcon.Length > 2 && o.MapIcon.Substring(0, 2).Equals("01"))
                    .ToList();
            }
        }

        public static List<TblClubActivity> getClubActivities()
        {
            using (var context = new OndoDB_2013_3_4_8_15Context())
            {
                return context.TblClubActivity.ToList();
            }
        }

        public static List<TblUserBalanceCard> getUserBalanceCards()
        {
            using (var context = new OndoDB_2013_3_4_8_15Context())
            {
                return context.TblUserBalanceCard
                    //BenefitId 14341 is LokalFordele
                    .Where(o => o.BenefitId == 14341)
                    .ToList();
            }
        }

        public static List<TblUsers> getUsers()
        {
            using (var context = new OndoDB_2013_3_4_8_15Context())
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

        public static List<TblSettlements> getSettlements()
        {
            using (var context = new OndoDB_2013_3_4_8_15Context())
            {
                return context.TblSettlements
                    .ToList();
            }
        }
    }
}
