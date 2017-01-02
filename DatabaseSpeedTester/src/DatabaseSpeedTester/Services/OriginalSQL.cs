using DatabaseSpeedTester.OndoModels;
using DatabaseSpeedTester.ServiceModels;
using DatabaseSpeedTester.ServiceModels.clubEntities;
using DatabaseSpeedTester.Services.Original;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseSpeedTester.Services
{
    public class OriginalSQL
    {
        /// <summary>
        /// Takes an ondoId and returns an clubEntity object.
        /// 
        /// This method will retrieve data from the ondodeveloper database and calculate the figures needed
        /// in the clubEntity object, then create a new clubEntity and returns it
        /// </summary>
        /// <param name="ondoId"></param>
        /// <returns></returns>
        public ClubEntity getClubData(int ondoId)
        {
            List<TblOndos> ondoClubs = new List<TblOndos>();
            ondoClubs.Add(GetDataFromOndoDb.getClub(ondoId));
            List<TblUserBalanceCard> cards = GetDataFromOndoDb.getUserBalanceCards();
            List<TblClubActivity> activities = GetDataFromOndoDb.getClubActivities();
            List<TblUsers> users = GetDataFromOndoDb.getUsers();
            List<TblSettlements> settlements = GetDataFromOndoDb.getSettlements();

            foreach (TblOndos ondoClub in ondoClubs)
            {
                int prognose = 0;
                int usersInClub = 0;
                int activeUsers = 0;
                int target = 0;
                int appUsers = 0;
                List<TblUserBalanceCard> cardsPointingAtClub = new List<TblUserBalanceCard>();
                List<TblSettlements> settlementsForClub = new List<TblSettlements>();
                foreach (TblClubActivity activity in activities)
                {
                    if (activity.ClubOndoId == ondoClub.OndoId)
                    {
                        target = target + (int)activity.Amount;
                        foreach (TblUserBalanceCard card in cards)
                        {
                            if (card.ActivityId == activity.ActivityId)
                            {
                                cardsPointingAtClub.Add(card);
                                prognose = prognose + (int)card.Balance;
                                usersInClub++;
                                if (card.Balance != 0)
                                {
                                    activeUsers++;
                                }
                                foreach (TblUsers user in users)
                                {
                                    if (card.UserId == user.UserId && user.IsVirtual == 0)
                                    {
                                        appUsers++;
                                    }
                                }
                            }
                        }
                    }
                }
                foreach (TblSettlements settlement in settlements)
                {
                    if (settlement.ClubOndoId == ondoClub.OndoId)
                    {
                        settlementsForClub.Add(settlement);
                    }
                }


                int eightyPercent = calculateEightyPercent(cardsPointingAtClub);

                ClubEntity club = new ClubEntity()
                {
                    OndoId = ondoClub.OndoId,
                    Title = ondoClub.Title,
                    ProfilePicture = ondoClub.ProfilePicture,
                    prognose = prognose,
                    estimatedPrognose = calculateEstimatedPrognose(prognose),
                    eightyPercentPrognose = eightyPercent,
                    eightyPercentEstimatedPrognose = calculateEstimatedPrognose(eightyPercent),
                    percentActiveUsers = calculateNrOfActiveUsers(usersInClub, activeUsers),
                    activeUsers = activeUsers,
                    inactiveUsers = usersInClub - activeUsers,
                    percentAppUsers = calculateNrOfAppUsers(usersInClub, appUsers),
                    appUsers = appUsers,
                    noAppUsers = usersInClub - appUsers,
                    history = getHistoryForClub(settlementsForClub),
                    target = target,
                    daysLeftInQuarter = DateTimeConverter.daysLeftInQuarter(DateTime.Now)
                };

                return club;
            }
            return null;
        }

        /// <summary>
        /// Calculates and returns an int representation of activeusers percentage
        /// </summary>
        /// <param name="users"></param>
        /// <param name="activeUsers"></param>
        /// <returns></returns>
        private int calculateNrOfActiveUsers(int users, int activeUsers)
        {
            int result = 0;
            if (users != 0)
            {
                result = (activeUsers * 100) / users;
            }
            return result;
        }

        /// <summary>
        /// Calculates and returns an int representation of appusers percentage
        /// </summary>
        /// <param name="users"></param>
        /// <param name="appUsers"></param>
        /// <returns></returns>
        private int calculateNrOfAppUsers(int users, int appUsers)
        {
            int result = 0;
            if (users != 0)
            {
                result = (appUsers * 100) / users;
            }
            return result;
        }

        /// <summary>
        /// Calculates the prognose estimate for the current quarter and returns it as an int
        /// </summary>
        /// <param name="prognose"></param>
        /// <returns></returns>
        private int calculateEstimatedPrognose(int prognose)
        {
            int result;
            int datesInQuarter = DateTimeConverter.totalNumberOfDaysInQuarter(DateTime.Now);
            int datesInQuarterSoFar = DateTimeConverter.daysInQuarterSoFar(DateTime.Now);
            int daysLeftInPercentage = (datesInQuarterSoFar * 100) / datesInQuarter;

            if (daysLeftInPercentage != 0)
            {
                result = (prognose * 100) / daysLeftInPercentage;
            }
            else
            {
                result = prognose;
            }
            return result;
        }

        /// <summary>
        /// Calculates the points to the club if 80 percent did as the best 20 percent
        /// </summary>
        /// <param name="cardsPointingAtClub"></param>
        /// <returns></returns>
        private int calculateEightyPercent(List<TblUserBalanceCard> cardsPointingAtClub)
        {
            int result = 0;
            // Sorts the cards in the club so the cards with the highest balance is in the top
            List<TblUserBalanceCard> sortedCards = cardsPointingAtClub.OrderByDescending(o => o.Balance).ToList();
            // Figures out how many iterations the for loop has to do. The count is devided by 5 so it only takes 20%
            int nrOfIterations = cardsPointingAtClub.Count() / 5;

            for (int i = 0; i < nrOfIterations; i++)
            {
                result += (int)sortedCards[i].Balance;
            }
            // result is multiplied with 4 to get the amount if 80% did the same as the top 20%
            result = result * 4;

            return result;
        }

        /// <summary>
        /// Takes a list of settlements for a club and calculates the points for the last four quarters and returns the results
        /// as a ClubQuarterHistory array
        /// </summary>
        /// <param name="settlements"></param>
        /// <returns></returns>
        private ClubQuarterHistory[] getHistoryForClub(List<TblSettlements> settlements)
        {
            int currentYear = DateTime.Now.Year;
            int currentQuarter = DateTimeConverter.getCurrentQuarter(DateTime.Now);
            var quartersArr = new ClubQuarterHistory[4];

            for (int i = 0; i < 4; i++)
            {
                if (currentQuarter == 1)
                {
                    currentQuarter = 4;
                    currentYear--;
                }
                else
                {
                    currentQuarter--;
                }
                quartersArr[i] = new ClubQuarterHistory(currentQuarter + ". Kvartal " + currentYear, 0);
            }

            for (int i = 0; i < settlements.Count(); i++)
            {
                quartersArr[i].points = settlements[i].Points;
            }

            return quartersArr;
        }
    }
}
