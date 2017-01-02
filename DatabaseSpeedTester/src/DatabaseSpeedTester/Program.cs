using DatabaseSpeedTester.Services;
using System;
using System.Diagnostics;

namespace DatabaseSpeedTester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Stopwatch watch     = new Stopwatch();
            Double latency      = PingTest.PingTimeAverage("ondodeveloper.database.windows.net", 4);

            int ondoId          = 10302; // ondoId
            int iterations      = 1; // number of iterations each test runs
            long[] results      = new long[4];

            SQL sql             = new SQL();
            Mongo mongo         = new Mongo();
            Redis redis         = new Redis();
            OriginalSQL osql    = new OriginalSQL();

            //Variables for slowest and fastest test
            double sqlSlowest   = 0;
            double sqlFastest   = 1000;

            double mongoSlowest = 0;
            double mongoFastest = 1000;

            double redisSlowest = 0;
            double redisFastest = 1000;

            Console.WriteLine("STARTING TEST");
            Console.WriteLine("\n This test will retrive data for Ondo ID 10302, \n each test will be runned "+ iterations + " times and the \n times are an average in milliseconds.");
            Console.WriteLine("\n Running test on MS SQL Database");

            sql.getClubById(ondoId); // Startup the database by runnning the method once before taking time
            watch.Start();
            for (int i = 0; i < iterations; i++)
            {
                sql.getClubById(ondoId);
                double time = watch.ElapsedMilliseconds / (i+1);
                if (time < sqlFastest) { sqlFastest = time; }
                if (time > sqlSlowest) { sqlSlowest = time; }
            }
            watch.Stop();
            results[0] = watch.ElapsedMilliseconds / iterations;
            watch.Reset();

            Console.WriteLine(" done");

            Console.WriteLine("\n Running test on MongoDB Database");

            mongo.findByProp(ondoId); // Startup the database by runnning the method once before taking time
            watch.Start();
            for (int i = 0; i < iterations; i++)
            {
                mongo.findByProp(ondoId);
                double time = watch.ElapsedMilliseconds / (i+1);
                if (time < mongoFastest) { mongoFastest = time; }
                if (time > mongoSlowest) { mongoSlowest = time; }
            }
            watch.Stop();
            results[1] = watch.ElapsedMilliseconds / iterations;
            watch.Reset();

            Console.WriteLine(" done");

            Console.WriteLine("\n Running test on Redis Database");

            redis.getClubById(ondoId); // Startup the database by runnning the method once before taking time
            watch.Start();
            for (int i = 0; i < iterations; i++)
            {
                redis.getClubById(ondoId);
                double time = watch.ElapsedMilliseconds / (i+1);
                if (time < redisFastest) { redisFastest = time; }
                if (time > redisSlowest) { redisSlowest = time; }
            }
            watch.Stop();
            results[2] = watch.ElapsedMilliseconds / iterations;
            watch.Reset();

            Console.WriteLine(" done");

            Console.WriteLine("\n Running test on Original SQL Database with calculations");

            osql.getClubData(ondoId); // Startup the database by runnning the method once before taking time
            watch.Start();
            for (int i = 0; i < iterations; i++)
            {
                osql.getClubData(ondoId);
            }
            watch.Stop();
            results[3] = watch.ElapsedMilliseconds / iterations;
            watch.Reset();

            Console.WriteLine(" done");


            Console.WriteLine("\n All test concluded. \n Latency: " + latency + "ms \n Results:");
            Console.WriteLine(" MS SQL: " + results[0] + "ms. avg. " + sqlFastest + "ms - " + sqlSlowest + "ms Range: " + (sqlSlowest-sqlFastest));
            Console.WriteLine(" Mongo:  " + results[1] + "ms. avg. " + mongoFastest + "ms - " + mongoSlowest + "ms Range: " + (mongoSlowest - mongoFastest));
            Console.WriteLine(" Redis:  " + results[2] + "ms. avg. " + redisFastest + "ms - " + redisSlowest + "ms Range: " + (redisSlowest - redisFastest));
            Console.WriteLine(" Orig.:  " + results[3] + "ms. avg.");

            while (true)
            {

            }
        }
    }
}
