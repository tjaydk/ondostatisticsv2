using DatabaseSpeedTester.ServiceModels;
using StackExchange.Redis;
using System;
using Newtonsoft.Json.Linq;

namespace DatabaseSpeedTester.Services
{
    public class Redis
    {
        //Connection string to specific redis db
        static string connectionString = "";
        private static Lazy<ConnectionMultiplexer> lazyConnection;

        /// <summary>
        /// Static constructor that returns a connection to redis database
        /// </summary>
        static Redis()
        {
            lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(connectionString);
            });
        }
        
        /// <summary>
        /// Returns value of connection
        /// </summary>
        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }

        /// <summary>
        /// Returns a club by ondoId as ClubEntity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClubEntity getClubById(int id)
        {
            var redis = Redis.Connection.GetDatabase();
            var value = redis.StringGet(id + "");

            JObject obj = JObject.Parse(value);
            return obj.ToObject<ClubEntity>();
        }
    }
}
