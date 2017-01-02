using System.Collections.Generic;
using OndoStatisticsWebjob_2._0.Entities;
using OndoStatisticsWebjob_2._0.Models;
using System;
using OndoStatisticsWebjob_2._0.Entities.shopEntities;

namespace OndoStatisticsWebjob_2._0.Services.CalculateData
{
    class ShopCalculations
    {
        /// <summary>
        /// Calculates data for all ondo shops
        /// </summary>
        /// <returns></returns>
        public List<ShopEntity> CalculateShopData()
        {
            List<ShopEntity> shops = new List<ShopEntity>();
            //List of shops from OndoDots database
            List<TblOndos> ondoShops = GetDataFromOndoDb.getShops();
            //List of all transactions from OndoDots database
            List<TblUserTransactions> transactions = GetDataFromOndoDb.getTransactions();
            // Booleans to check if transactions happened in current quarter or in current week for both city and shop
            bool transactionsCurrentQuarterShop;
            bool transactionsCurrentWeekShop;
            bool transactionsCurrentQuarterCity;
            bool transactionsCurrentWeekCity;

            // Runs through all shops
            foreach (TblOndos ondoShop in ondoShops)
            {
                // Sets current quarter and current week
                int currentQuarter = DateTimeConverter.dateToQuarter(DateTime.Now);
                int currentWeek = DateTimeConverter.dateToWeek(DateTime.Now);

                // Creates an empty list that will be populated with shops in the same city as the current shop
                List<TblOndos> shopsInCity = new List<TblOndos>();
                // Creates two empty list that will be populated with transactions for the current shop, and for all the shops in the city
                List<TblUserTransactions> transactionsForShop = new List<TblUserTransactions>();
                List<TblUserTransactions> transactionsForCity = new List<TblUserTransactions>();

                // Adds shops to the shop list if they are in the same city
                foreach (TblOndos ondo in ondoShops)
                {
                    if (ondo.Sponsor.Equals(ondoShop.Sponsor))
                    {
                        shopsInCity.Add(ondo);
                    }
                }

                // Runs through all transactions
                foreach (TblUserTransactions transaction in transactions)
                {
                    // If the transaction is for the current shop its added to the list of transactions for shop
                    if (transaction.OndoId == ondoShop.OndoId)
                    {
                        // Adds the transaction to the list
                        transactionsForShop.Add(transaction);
                    }
                    // Runs through all the shops in the same city as the current shop
                    foreach (TblOndos ondo in shopsInCity)
                    {
                        // if the transaction is for a shop in the same city as the current shop its added to the list of transactions for city
                        if (transaction.OndoId == ondo.OndoId)
                        {
                            transactionsForCity.Add(transaction);
                        }
                    }
                }
                // If there is no transactions for the shop no enity is added
                if (transactionsForShop.Count == 0)
                {
                    continue;
                }

                // Gets a list of HistoryData for the current shop
                List<HistoryData> shopData = TransactionCalculater.getTransactions(transactionsForShop);
                // Adds a reference to this quarters data
                HistoryData thisQuarterShop = shopData[0];
                // Sets booleans for if there is transactions this quarter and week for the shop
                transactionsCurrentQuarterShop = (thisQuarterShop.quarterLabel.Substring(0, 1).Equals(currentQuarter + "")) ? true : false;
                transactionsCurrentWeekShop = (thisQuarterShop.historyWeekDataArray[0].weekLabel.Equals("Uge " + currentWeek)) ? true : false;
                // If there is transactions this quarter the data is removed from the list
                if (transactionsCurrentQuarterShop)
                {
                    // Removes the data from the list
                    shopData.RemoveAt(0);
                }

                // Gets a list of HistoryData for the city 
                List<HistoryData> cityData = TransactionCalculater.getTransactions(transactionsForCity);
                // Gets the data for this quarter
                HistoryData thisQuarterCity = cityData[0];
                // Sets booleans for if there is transactions this quarter and week for the city
                transactionsCurrentQuarterCity = (thisQuarterCity.quarterLabel.Substring(0, 1).Equals(currentQuarter + "")) ? true : false;
                transactionsCurrentWeekCity = (thisQuarterCity.historyWeekDataArray[0].weekLabel.Equals("Uge " + currentWeek)) ? true : false;

                //Creates the shopEntity and fills in it's values
                ShopEntity shop = new ShopEntity()
                {
                    ondoId = ondoShop.OndoId,
                    title = ondoShop.Title,
                    profilePicture = ondoShop.ProfilePicture,
                    transactionsCurrentQuarterShop = (transactionsCurrentQuarterShop) ? thisQuarterShop.transactions : 0,
                    transactionsCurrentWeekShop = (transactionsCurrentWeekShop) ? thisQuarterShop.historyWeekDataArray[0].transactions : 0,
                    subscriptionsCurrentQuarterShop = (transactionsCurrentQuarterShop) ? thisQuarterShop.subscriptions : 0,
                    subscriptionsCurrentWeekShop = (transactionsCurrentWeekShop) ? thisQuarterShop.historyWeekDataArray[0].subscriptions : 0,
                    transactionsCurrentQuarterCity = (transactionsCurrentQuarterCity) ? thisQuarterCity.transactions : 0,
                    transactionsCurrentWeekCity = (transactionsCurrentWeekCity) ? thisQuarterCity.historyWeekDataArray[0].transactions : 0,
                    subscriptionsCurrentQuarterCity = (transactionsCurrentQuarterCity) ? thisQuarterCity.subscriptions : 0,
                    subscriptionsCurrentWeekCity = (transactionsCurrentWeekCity) ? thisQuarterCity.historyWeekDataArray[0].subscriptions : 0,
                    subsriptionsQuarterCityAvg = (transactionsCurrentQuarterCity) ? MathCalculations.calculateAverage(shopsInCity.Count, thisQuarterCity.transactions) : 0,
                    transactionsQuarterCityAvg = (transactionsCurrentQuarterCity) ? MathCalculations.calculateAverage(shopsInCity.Count, thisQuarterCity.subscriptions) : 0,
                    pointsCurrentQuarter = thisQuarterShop.points,
                    weekNo = currentWeek,
                    weekDataArray = (transactionsCurrentQuarterShop) ? thisQuarterShop.historyWeekDataArray : null,
                    dailyDataArray = TransactionCalculater.getDailyData(transactionsForShop),
                    historyDataArray = shopData
                };
                // Adds the shop to the list of shops
                shops.Add(shop);
            }
            return shops;
        }
    }
}
