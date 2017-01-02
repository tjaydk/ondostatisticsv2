using System.Collections.Generic;
using OndoStatisticsWebjob_2._0.Entities;
using OndoStatisticsWebjob_2._0.Models;
using System;
using OndoStatisticsWebjob_2._0.Entities.shopEntities;
using OndoStatisticsWebjob_2._0.Entities.TradeUnionEntities;
using System.Linq;

namespace OndoStatisticsWebjob_2._0.Services.CalculateData
{
    class TradeUnionCalculations
    {
        public List<TradeUnionEntity> CalculateTradeUnionData()
        {
            List<TradeUnionEntity> data = new List<TradeUnionEntity>();
            List<TblOndos> tradeUnions = GetDataFromOndoDb.getTradeUnisons();
            List<TblOndos> shops = GetDataFromOndoDb.getShops();
            List<TblOndos> clubs = GetDataFromOndoDb.getClubs();
            List<TblUserTransactions> transactions = GetDataFromOndoDb.getTransactions();
            List<TopFiveShopsDTO> bestShops;
            List<TblClubActivity> activities = GetDataFromOndoDb.getClubActivities();
            List<TblUserBalanceCard> userCards = GetDataFromOndoDb.getUserBalanceCards();
            List<TblUsers> users = GetDataFromOndoDb.getUsers();
            List<TblEncryptedCards> encryptedCards = GetDataFromOndoDb.getEncryptedCards();

            foreach (TblOndos tradeUnion in tradeUnions)
            {
                List<TblOndos> shopsInTradeUnion = new List<TblOndos>();
                List<TblOndos> clubsInTradeUnion = new List<TblOndos>();
                bool transactionsCurrentQuarter;
                bool transactionsCurrentWeek;
                int currentQuarter = DateTimeConverter.dateToQuarter(DateTime.Now);
                int currentWeek = DateTimeConverter.dateToWeek(DateTime.Now);

                foreach (TblOndos shop in shops)
                {
                    if (shop.Sponsor.Equals(tradeUnion.OndoId.ToString()))
                    {
                        shopsInTradeUnion.Add(shop);
                    }
                }
                foreach (TblOndos club in clubs)
                {
                    if (club.Sponsor.Equals(tradeUnion.OndoId.ToString()))
                    {
                        clubsInTradeUnion.Add(club);
                    }
                }
                
                List<TblUserTransactions> transactionsInTradeUnion = new List<TblUserTransactions>();
                List<ShopDTO> shopDTOs = new List<ShopDTO>();

                foreach (TblOndos shop in shopsInTradeUnion)
                {
                    List<TblUserTransactions> transactionsForShop = new List<TblUserTransactions>();
                    foreach (TblUserTransactions transaction in transactions)
                    {
                        if (transaction.OndoId == shop.OndoId)
                        {
                            transactionsInTradeUnion.Add(transaction);
                            transactionsForShop.Add(transaction);
                        }
                    }
                    if (transactionsForShop.Count == 0)
                    {
                        continue;
                    }
                    List<HistoryData> historyDataForShop = TransactionCalculater.getTransactions(transactionsForShop);
                    HistoryData HistoryDataForCurrentQuarterShop = historyDataForShop[0];

                    transactionsCurrentQuarter = (HistoryDataForCurrentQuarterShop.quarterLabel.Substring(0, 1).Equals(currentQuarter + "")) ? true : false;
                    transactionsCurrentWeek = (HistoryDataForCurrentQuarterShop.historyWeekDataArray[0].weekLabel.Equals("Uge " + currentWeek)) ? true : false;
                    ShopDTO shopDTO = new ShopDTO()
                    {
                        title = shop.Title,
                        profilePicture = shop.ProfilePicture,
                        category = ShopCategory.checkCategory(shop.MapIcon),
                        transactions = HistoryDataForCurrentQuarterShop.transactions,
                        subscriptions = HistoryDataForCurrentQuarterShop.subscriptions,
                        points = HistoryDataForCurrentQuarterShop.points,
                        weeksArray = HistoryDataForCurrentQuarterShop.historyWeekDataArray
                    };
                    shopDTOs.Add(shopDTO);

                }
                if (transactionsInTradeUnion.Count == 0)
                {
                    continue;
                }


                List<HistoryData> historyData = TransactionCalculater.getTransactions(transactionsInTradeUnion);
                HistoryData dataForCurrentQuarter = historyData[0];
                transactionsCurrentQuarter = (dataForCurrentQuarter.quarterLabel.Substring(0, 1).Equals(currentQuarter + "")) ? true : false;
                transactionsCurrentWeek = (dataForCurrentQuarter.historyWeekDataArray[0].weekLabel.Equals("Uge " + currentWeek)) ? true : false;
                bestShops = findFiveBestShops(shopDTOs);

                List<TblClubActivity> activitiesInTradeUnion = findClubActivitiesInTradeUnion(clubsInTradeUnion, activities);
                List<ClubDTO> clubDTOs = findClubDTOs(activitiesInTradeUnion, clubsInTradeUnion, userCards, users, transactions);
                QuarterClubNumbers clubNumbers = getClubNumbersForQuarter(clubDTOs);
                NoClubDTO noClub = getNoClubNumbers(userCards, encryptedCards, tradeUnion.OndoId);
                
                TradeUnionEntity tue = new TradeUnionEntity()
                {
                    ondoId = tradeUnion.OndoId,
                    title = tradeUnion.Title,
                    profilePicture = tradeUnion.ProfilePicture,
                    transactionsQuarter = (transactionsCurrentQuarter) ? dataForCurrentQuarter.transactions : 0,
                    transactionsWeekShops = (transactionsCurrentWeek) ? dataForCurrentQuarter.historyWeekDataArray[0].transactions : 0,
                    subscriptionsQuarter = (transactionsCurrentQuarter) ? dataForCurrentQuarter.subscriptions + clubNumbers.subscriptions : 0,
                    subscriptionsQuarterShops = (transactionsCurrentQuarter) ? dataForCurrentQuarter.subscriptions : 0,
                    subscriptionsQuarterClubs = clubNumbers.subscriptions,
                    subscriptionsWeekShops = (transactionsCurrentWeek) ? dataForCurrentQuarter.historyWeekDataArray[0].subscriptions : 0,
                    activityQuarter = (transactionsCurrentQuarter) ? dataForCurrentQuarter.transactions + dataForCurrentQuarter.subscriptions + clubNumbers.subscriptions : clubNumbers.subscriptions,
                    activityQuarterShops = (transactionsCurrentQuarter) ? dataForCurrentQuarter.transactions + dataForCurrentQuarter.subscriptions : 0,
                    activityWeekShops = (transactionsCurrentWeek) ? dataForCurrentQuarter.historyWeekDataArray[0].transactions + dataForCurrentQuarter.historyWeekDataArray[0].subscriptions : 0,
                    pointsQuarter = (transactionsCurrentQuarter) ? dataForCurrentQuarter.points : 0,
                    pointsWeekShops = (transactionsCurrentWeek) ? dataForCurrentQuarter.historyWeekDataArray[0].points : 0,
                    topFiveShops = bestShops,
                    weeksArrayQuarter = dataForCurrentQuarter.historyWeekDataArray,
                    cardUsersQuarter = clubNumbers.cardUsers,
                    cardUsersQuarterPercent = MathCalculations.calculatePercentage(clubNumbers.cardUsers, clubNumbers.cardUsers),
                    activeCardUsersQuarter = clubNumbers.activeCardUsers,
                    activeCardUsersQuarterPercent = MathCalculations.calculatePercentage(clubNumbers.activeCardUsers, clubNumbers.cardUsers),
                    inactiveCardUsersQuarter = clubNumbers.inactiveCardUsers,
                    inactiveCardUsersQuarterPercent = MathCalculations.calculatePercentage(clubNumbers.inactiveCardUsers, clubNumbers.cardUsers),
                    appUsersQuarter = clubNumbers.appUsers,
                    appUsersQuarterPercent = MathCalculations.calculatePercentage(clubNumbers.appUsers, clubNumbers.cardUsers),
                    cardUsersWithNoClub = noClub.count,
                    cardUsersWithNoClubPercent = MathCalculations.calculatePercentage(noClub.count, clubNumbers.cardUsers),
                    pointsToClubs = clubNumbers.points,
                    pointsNoClubs = noClub.points,
                    appUsersQuarterInCity = clubNumbers.appUsers,
                    cardUsersQuarterInCity = clubNumbers.cardUsers,
                    activeCardUsersQuarterInCity = clubNumbers.activeCardUsers,
                    inactiveCardUsersQuarterInCity = clubNumbers.inactiveCardUsers,
                    clubsCountWithSubscriptions = clubNumbers.clubCountsubscriptions,
                    shops = shopDTOs,
                    clubs = clubDTOs,
                    history = TransactionCalculater.getHistoryForTradeUnion(shopsInTradeUnion, transactionsInTradeUnion),
                    userHistory = generateUserHistory()
                };
                data.Add(tue);
            }
            return data;
        }

        private List<TopFiveShopsDTO> findFiveBestShops(List<ShopDTO> shops)
        {
            //Scoren bliver udregnet ved: Point: 50%, Oprettelser: 300% og transaktioner: 150%
            List<TopFiveShopsDTO> bestShops = new List<TopFiveShopsDTO>();

            foreach (ShopDTO shop in shops)
            {
                int score = ((shop.points * 50) / 100) + ((shop.subscriptions * 300) / 100) + ((shop.transactions * 150) / 100);
                bestShops.Add(new TopFiveShopsDTO(shop.title, shop.profilePicture, score));
            }

            bestShops = bestShops.OrderByDescending(o => o.score)
                .Take(5)
                .ToList();

            return bestShops;
        }

        private int getSubscriptionsForClubs(TblOndos club, List<TblUserTransactions> transactions)
        {
            int sub = 0;
            int currentQuarter = DateTimeConverter.dateToQuarter(DateTime.Now);

            foreach (TblUserTransactions transaction in transactions)
            {
                int transactionQuarter = DateTimeConverter.dateToQuarter((DateTime)transaction.DateTime);
                if (club.OndoId == transaction.OndoId && transaction.Points == 0 && currentQuarter == transactionQuarter) { sub++; }
            }
            return sub;
        }

        private List<TblClubActivity> findClubActivitiesInTradeUnion(List<TblOndos> clubsInTradeUnion, List<TblClubActivity> activities)
        {
            List<TblClubActivity> activitiesInTradeUnion = new List<TblClubActivity>();
            foreach (TblOndos club in clubsInTradeUnion)
            {
                foreach (TblClubActivity activity in activities)
                {
                    if (activity.ClubOndoId == club.OndoId)
                    {
                        activitiesInTradeUnion.Add(activity);
                    }
                }
            }
            return activitiesInTradeUnion;
        }

        private List<ClubDTO> findClubDTOs(List<TblClubActivity> activitiesInTradeUnion, List<TblOndos> clubsInTradeUnion, List<TblUserBalanceCard> cards, List<TblUsers> users, List<TblUserTransactions> transactions)
        {
            List<ClubDTO> clubs = new List<ClubDTO>();

            foreach (TblOndos club in clubsInTradeUnion)
            {
                ClubDTO dto = new ClubDTO();
                dto.title = club.Title;
                dto.subscriptions = getSubscriptionsForClubs(club, transactions);
                foreach (TblClubActivity activity in activitiesInTradeUnion)
                {
                    if (activity.ClubOndoId == club.OndoId)
                    {
                        foreach (TblUserBalanceCard card in cards)
                        {
                            if (card.ActivityId == activity.ActivityId)
                            {
                                dto.points += (int)card.Balance;
                                dto.cardUsers++;

                                var activeOrInactive = (card.Balance != 0) ? dto.activeCardUsers++ : dto.inactiveCardUsers++;

                                foreach (TblUsers user in users)
                                {
                                    if (card.UserId == user.UserId && user.IsVirtual == 0)
                                    {
                                        dto.appUsers++;
                                    }
                                }
                            }
                        }
                    }
                }
                clubs.Add(dto);
            }
            return clubs;
        }

        private QuarterClubNumbers getClubNumbersForQuarter(List<ClubDTO> clubDTOs)
        {
            QuarterClubNumbers num = new QuarterClubNumbers();

            foreach (ClubDTO club in clubDTOs)
            {
                num.points += club.points;
                num.appUsers += club.appUsers;
                num.cardUsers += club.cardUsers;
                num.activeCardUsers += club.activeCardUsers;
                num.inactiveCardUsers += club.inactiveCardUsers;
                if (club.subscriptions != 0)
                {
                    num.subscriptions += club.subscriptions;
                    num.clubCountsubscriptions++;
                }
            }
            return num;
        }

        private NoClubDTO getNoClubNumbers(List<TblUserBalanceCard> cards, List<TblEncryptedCards> encryptedCards, int TradeUnionOndoId)
        {
            NoClubDTO noCLub = new NoClubDTO();
            foreach (TblUserBalanceCard card in cards)
            {
                foreach (TblEncryptedCards encryptedCard in encryptedCards)
                {
                    if (encryptedCard.CityCode.Equals(ShopCategory.getCityCode(TradeUnionOndoId)) && card.CardNr.Equals(encryptedCard.CardNr ) && card.ActivityId == 0) {
                        noCLub.points += (int)card.Balance;
                        noCLub.count++;
                    }
                }
            }
            return noCLub;
        }

        private List<UserHistory> generateUserHistory()
        {
            List<UserHistory> userhistory = new List<UserHistory>();

            UserHistory uh1 = new UserHistory()
            {
                quarterLabel = "3. Kvartal 2016",
                cardUsers = 1148,
                cardUsersPercent = MathCalculations.calculatePercentage(1148, 1148),
                activeCardUsers = 324,
                activeCardUsersPercent = MathCalculations.calculatePercentage(324, 1148),
                inactiveCardUsers = 824,
                inactiveCardUsersPercent = MathCalculations.calculatePercentage(824,1148),
                appUsers = 479,
                appUsersPercent = MathCalculations.calculatePercentage(479,1148),
                cardUsersWithNoClub = 232,
                cardUsersWithNoClubPercent = MathCalculations.calculatePercentage(232,1148)
            };
            UserHistory uh2 = new UserHistory()
            {
                quarterLabel = "2. Kvartal 2016",
                cardUsers = 1052,
                cardUsersPercent = MathCalculations.calculatePercentage(1052, 1052),
                activeCardUsers = 104,
                activeCardUsersPercent = MathCalculations.calculatePercentage(104, 1052),
                inactiveCardUsers = 948,
                inactiveCardUsersPercent = MathCalculations.calculatePercentage(948, 1052),
                appUsers = 154,
                appUsersPercent = MathCalculations.calculatePercentage(154, 1148),
                cardUsersWithNoClub = 402,
                cardUsersWithNoClubPercent = MathCalculations.calculatePercentage(402, 1052)
            };

            userhistory.Add(uh1);
            userhistory.Add(uh2);

            return userhistory;
        }
    }
}
