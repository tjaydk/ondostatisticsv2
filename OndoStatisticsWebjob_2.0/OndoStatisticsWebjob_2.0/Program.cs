﻿using Microsoft.Azure.WebJobs;
using OndoStatisticsWebjob_2._0.Services;

namespace OndoStatisticsWebjob_2._0
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    public class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        public static void Main()
        {
            var host = new JobHost();
            host.Call(typeof(Program).GetMethod("persist"));
        }

        [NoAutomaticTrigger]
        public static void persist()
        {
            PersistToDB persist = new PersistToDB();
            persist.PersistClubData();
            persist.PersistShopData();
            persist.PersistTradeUnionData();
        }
    }
}
