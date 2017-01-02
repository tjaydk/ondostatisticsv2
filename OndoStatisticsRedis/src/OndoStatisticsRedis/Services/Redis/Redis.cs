using StackExchange.Redis;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OndoStatisticsRedis.Services.Redis
{
    public class Redis
    {
        static Redis()
        {
            lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {   
                return ConnectionMultiplexer.Connect(Startup.Configuration.GetConnectionString("RedisConnection"));
            });
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection;

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        public string getOndoById(int ondoID)
        {
            var redis = Redis.Connection.GetDatabase();

            return redis.StringGet(ondoID + "");
        }
    }
}
