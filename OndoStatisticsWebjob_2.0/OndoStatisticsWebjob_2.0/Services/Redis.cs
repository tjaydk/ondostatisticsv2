using StackExchange.Redis;
using System;

namespace OndoStatisticsWebjob_2._0.Services
{
    class Redis
    {
        static Redis()
        {
            lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect("ondoRedis2.redis.cache.windows.net:6380,password=tvsHKzEAgiMNHekqiZtwXQZLWFszsW9C/zWFBlN0ZLk=,ssl=True,abortConnect=False");
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
    }
}
