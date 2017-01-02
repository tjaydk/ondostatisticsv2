using OndoStatisticsWebjob_2._0.Entities;
using OndoStatisticsWebjob_2._0.Entities.clubEntities;
using OndoStatisticsWebjob_2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OndoStatisticsWebjob_2._0.Services.CalculateData
{
    class Clubcalculations
    {
        public List<ClubEntity> CalculateClubData()
        {
            List<ClubEntity> clubs = new List<ClubEntity>();
            List<TblOndos> ondoClubs = GetDataFromOndoDb.getClubs();
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
                    ondoId = ondoClub.OndoId,
                    title = ondoClub.Title,
                    profilePicture = ondoClub.ProfilePicture,
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

                clubs.Add(club);
            }
            return clubs;
        }

        private int calculateNrOfActiveUsers(int users, int activeUsers)
        {
            int result = 0;
            if (users != 0)
            {
                result = (activeUsers * 100) / users;
            }
            return result;
        }

        private int calculateNrOfAppUsers(int users, int appUsers)
        {
            int result = 0;
            if (users != 0)
            {
                result = (appUsers * 100) / users;
            }
            return result;
        }

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

        private ClubQuarterHistory[] getHistoryForClub(List<TblSettlements> settlements)
        {
            int currentYear = DateTime.Now.Year;
            int currentQuarter = DateTimeConverter.dateToQuarter(DateTime.Now);
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
