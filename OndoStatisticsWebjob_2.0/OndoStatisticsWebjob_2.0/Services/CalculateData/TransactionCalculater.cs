using OndoStatisticsWebjob_2._0.Entities.shopEntities;
using OndoStatisticsWebjob_2._0.Entities.TradeUnionEntities;
using OndoStatisticsWebjob_2._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OndoStatisticsWebjob_2._0.Services.CalculateData
{
    class TransactionCalculater
    {
        /// <summary>
        /// Returns a List of historyData Objects from a List of transactions
        /// </summary>
        /// <param name="transactions"></param>
        /// <returns></returns>
        public static List<HistoryData> getTransactions(List<TblUserTransactions> transactions)
        {
            transactions = transactions.OrderByDescending(o => o.DateTime).ToList();
            //The List that will hold all HistoryData Objects
            List<HistoryData> historyDataList = new List<HistoryData>();

            // HistoryData object creation. Will be added to historyDataList later
            HistoryData hist = new HistoryData();
            hist.historyWeekDataArray = new List<WeekData>();

            // Quarter number of the most recent transaction made
            int quarterNo = DateTimeConverter.dateToQuarter((DateTime)transactions[0].DateTime);
            // Week number of the most recent transaction made
            int weekNo = DateTimeConverter.dateToWeek((DateTime)transactions[0].DateTime);
            int yearNo = DateTime.Now.Year;

            // Week variables
            int weeklySubscriptions = 0;
            int weeklyTransactions = 0;
            int weeklyActivity = 0;
            int weeklyPoints = 0;

            // Quarter variables
            int quarterlySubscriptions = 0;
            int quarterlyTransactions = 0;
            int quarterlyPoints = 0;

            // Runs through all transactions
            for (int i = 0; i < transactions.Count; i++)
            {
                // Reference to the current transaction
                TblUserTransactions transaction = transactions[i];
                // Quarter number of the transaction
                int transactionQuarter = DateTimeConverter.dateToQuarter((DateTime)transaction.DateTime);
                // Week number of the transaction
                int transactionWeek = DateTimeConverter.dateToWeek((DateTime)transaction.DateTime);

                // If the transaction week number is not the same as the current week number, a WeekData object needs to be added to the HistoryData object
                if (transactionWeek != weekNo)
                {
                    // Adds a WeekData Object to the HistoryData Object
                    hist.historyWeekDataArray.Add(new WeekData("Uge " + weekNo, weeklySubscriptions, weeklyTransactions, weeklyActivity, weeklyPoints));
                    //Resetting WeekCounts
                    weeklySubscriptions = 0;
                    weeklyTransactions = 0;
                    weeklyActivity = 0;
                    weeklyPoints = 0;
                    // If there is no transactions in the previous week an empty WeekData Object is added
                    while (weekNo - 1 != transactionWeek)
                    {
                        // Adds a WeekData Object to the HistoryData Object
                        hist.historyWeekDataArray.Add(new WeekData("Uge " + (weekNo - 1), weeklySubscriptions, weeklyTransactions, weeklyActivity, weeklyPoints));
                        weekNo--;
                    }
                    // Sets the week number to the week the transaction happened
                    weekNo = transactionWeek;

                }
                // If the quarter number is not the same as the current quarter number, a HistoryData object needs to be added to the HistoryData list
                if (transactionQuarter != quarterNo)
                {
                    // Sets values for the HistoryData object
                    hist.quarterLabel = quarterNo + ". Kvartal " + yearNo;
                    hist.subscriptions = quarterlySubscriptions;
                    hist.transactions = quarterlyTransactions;
                    hist.points = quarterlyPoints;
                    // Adds the historyData object to the HistoryData list
                    historyDataList.Add(hist);
                    // Resetting HistoryDataObject
                    hist = new HistoryData();
                    // Resetting the list of WeekData for the HistoryData Object
                    hist.historyWeekDataArray = new List<WeekData>();
                    if (quarterNo == 1)
                    {
                        yearNo--;
                    }
                    // Sets the quarter number to the quarter number of the current transaction
                    quarterNo = transactionQuarter;
                    // Resetting QuarterCounts
                    quarterlySubscriptions = 0;
                    quarterlyTransactions = 0;
                    quarterlyPoints = 0;
                }

                // If the transaction has a point value, a transaction has happened
                if (transaction.Points != 0)
                {
                    // Counts up values for WeekData & HistoryData object
                    weeklyTransactions++;
                    weeklyActivity++;
                    weeklyPoints++;
                    quarterlyTransactions++;
                    quarterlyPoints += (int)transaction.Points;
                }
                // If the transactions does'nt have a point value, a subscription has happened
                else
                {
                    // Counts up values for WeekData & HistoryData object
                    weeklySubscriptions++;
                    weeklyActivity++;
                    quarterlySubscriptions++;
                }

                // If the last transaction in the transaction list the last HistoryData object is added to the HistoryData list
                if (i == transactions.Count -1)
                {
                    hist.historyWeekDataArray.Add(new WeekData("Uge " + weekNo, weeklySubscriptions, weeklyTransactions, weeklyActivity, weeklyPoints));
                    hist.quarterLabel = quarterNo + ". Kvartal " + yearNo;
                    hist.subscriptions = quarterlySubscriptions;
                    hist.transactions = quarterlyTransactions;
                    hist.points = quarterlyPoints;
                    historyDataList.Add(hist);
                }
            }
            return historyDataList;
        }

        /// <summary>
        /// Returns a List of DailyData objects from a List of transactions
        /// </summary>
        /// <param name="transactions"></param>
        /// <returns></returns>
        public static List<DailyData> getDailyData(List<TblUserTransactions> transactions)
        {
            // Create the list of DailyData objects
            List<DailyData> data = new List<DailyData>();
            // Gets the current quarter number
            int quarterNo = DateTimeConverter.dateToQuarter(DateTime.Now);
            // Gets the date of the first transaction from the list of transactions
            DateTime initialDate = (DateTime) transactions[0].DateTime;
            // An int of how many days has passed in the quarter so far
            int daysPassedInQuarter = DateTimeConverter.daysInQuarterSoFar(initialDate) + 1;

            // Values for the DailyData object
            int sub = 0;
            int tra = 0;

            // Runs through the list of transactions
            foreach (TblUserTransactions transaction in transactions)
            {
                // Finds the quarter number of the transaction
                int transactionQuarter = DateTimeConverter.dateToQuarter((DateTime) transaction.DateTime);
                // Finds the Date the transaction happened. 
                DateTime transactionsDate = (DateTime) transaction.DateTime;
                // Makes sure the transaction happened in the current quarter
                if (quarterNo == transactionQuarter)
                {
                    // If the transaction didn't happen on the intial day a DailyData object is added to the list of DailyData objects.
                    if (initialDate.Day != transactionsDate.Day)
                    {
                        // Creates and adds a DailyData object to the list of DailyData objects
                        data.Add(new DailyData(initialDate.Day + "/" + initialDate.Month + "/" + initialDate.Year, sub, tra));
                        
                        // Resetting values for DailyData object
                        sub = 0;
                        tra = 0;

                        // Checks if there is a transaction the previous day. If there is not an empty DailyData object is added to the list of DailyData objects
                        while (initialDate.AddDays(-1).Day != transactionsDate.Day)
                        {
                            // Creates and adds an empty DailyData object to the list of DailyData objects
                            data.Add(new DailyData(initialDate.AddDays(-1).Day + "/" + initialDate.AddDays(-1).Month + "/" + initialDate.AddDays(-1).Year, sub, tra));
                            // Substracts a day from the initial day
                            initialDate = initialDate.AddDays(-1);
                            // Counts down dayspassed in quater
                            daysPassedInQuarter--;
                        }
                        // Sets the initialDate to the date of the transaction
                        initialDate = transactionsDate;
                        // Counts down dayspassed in quater
                        daysPassedInQuarter--;
                    }
                    // If there is no points in the transaction subscriptions value is added up, if there is transaction value is added up.
                    var subscriptionOrTransaction = (transaction.Points != 0) ? tra++ : sub++;
                }
                // If there is no more transactions in the current quarter
                else
                {
                    // Adds the last DailyData object to the list of DailyData objects
                    data.Add(new DailyData(initialDate.Day + "/" + initialDate.Month + "/" + initialDate.Year, sub, tra));
                    // Substracts a day from the initial day
                    initialDate = initialDate.AddDays(-1);
                    // Counts down dayspassed in quater
                    daysPassedInQuarter--;
                    // Resets the values for DailyData objects
                    sub = 0;
                    tra = 0;
                    // While there is still days left in the quarter, but there is no more transactions, Empty DailyData objects will be added to the list of DailyData objects
                    while (daysPassedInQuarter > 0)
                    {
                        // Creates and adds an empty DailyData object to the list of DailyData objects
                        data.Add(new DailyData(initialDate.Day + "/" + initialDate.Month + "/" + initialDate.Year, sub, tra));
                        initialDate = initialDate.AddDays(-1);
                        daysPassedInQuarter--;
                    }
                    return data;
                }
            }
            return data;
        }

        public static List<QuarterHistory> getHistoryForTradeUnion(List<TblOndos> shopsInTradeUnion, List<TblUserTransactions> transactionsInTradeUnion)
        {
            transactionsInTradeUnion = transactionsInTradeUnion.OrderByDescending(o => o.DateTime).ToList();
            DateTime first = (DateTime)transactionsInTradeUnion.Last().DateTime;
            int numberOfQuarters = 0;

            int firstYear = first.Year;
            int firstQuarter = DateTimeConverter.dateToQuarter(first);

            int currentYear = DateTime.Now.Year;
            int curretQuarter = DateTimeConverter.dateToQuarter(DateTime.Now);

            while(firstQuarter < curretQuarter && firstYear <= currentYear)
            {
                numberOfQuarters++;
                if (firstQuarter == 4) { firstQuarter = 1; firstYear++; }
                else { firstQuarter++; }
            }

            QuarterHistory history;
            ShopDTO shop;
            QuarterHistory[] historyList = new QuarterHistory[numberOfQuarters];

            int quarter = DateTimeConverter.dateToQuarter(DateTime.Now);
            int curQuarter = quarter;
            int year = DateTime.Now.Year;
            int week = DateTimeConverter.dateToWeek(DateTime.Now);

            //check which is last quarter
            if (quarter == 1) { quarter = 4; year--; }
            else { quarter--; }

            for (int i = 0; i < historyList.Length; i++)
            {
                history = new QuarterHistory();
                history.quarterLabel = quarter + ". Kvartal " + year;
                history.shops = new List<ShopDTO>();

                foreach (TblOndos ondo in shopsInTradeUnion)
                {
                    shop = new ShopDTO();
                    shop.title = ondo.Title;
                    shop.category = ShopCategory.checkCategory(ondo.MapIcon);
                    shop.weeksArray = new List<WeekData>();

                    String[] weeks = DateTimeConverter.weeksArrayForQuarter(quarter, year);
                    foreach (String w in weeks)
                    {
                        shop.weeksArray.Add(new WeekData(w, 0, 0, 0, 0));
                    }

                    foreach (TblUserTransactions transaction in transactionsInTradeUnion)
                    {
                        DateTime transactionDate = (DateTime)transaction.DateTime;
                        if (transaction.OndoId == ondo.OndoId && DateTimeConverter.dateToQuarter(transactionDate) == quarter && transactionDate.Year == year)
                        {
                            if (transaction.Points != 0)
                            {
                                history.transactions++;
                                shop.transactions++;
                                shop.points += (int)transaction.Points;
                                history.points += (int)transaction.Points;
                            }
                            else
                            {
                                shop.subscriptions++;
                                history.subscriptions++;
                            }
                            String weekLabel = "Uge " + DateTimeConverter.dateToWeek(transactionDate);
                            foreach (WeekData data in shop.weeksArray)
                            {
                                if (data.weekLabel == weekLabel)
                                {
                                    if (transaction.Points != 0)
                                    {
                                        data.transactions++;
                                        data.points += (int)transaction.Points;
                                        data.activity++;
                                    }
                                    else
                                    {
                                        data.subscriptions++;
                                        data.activity++;
                                    }
                                }
                            }
                        }
                    }
                    history.shops.Add(shop);
                }
                if (quarter == 1) { quarter = 4; year--; }
                else { quarter--; }
                historyList[i] = history;
            }

            return historyList.ToList();
        }
    }
}
