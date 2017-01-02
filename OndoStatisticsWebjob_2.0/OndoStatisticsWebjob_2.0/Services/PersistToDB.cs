using Newtonsoft.Json.Linq;
using OndoStatisticsWebjob_2._0.Entities;
using OndoStatisticsWebjob_2._0.Services.CalculateData;
using StackExchange.Redis;
using System.Collections.Generic;

namespace OndoStatisticsWebjob_2._0.Services
{
    class PersistToDB
    {

        public void PersistClubData()
        {
            Clubcalculations cc = new Clubcalculations();
            List<ClubEntity> clubs = cc.CalculateClubData();
            var redis = Redis.Connection.GetDatabase();
            foreach (ClubEntity club in clubs)
            {
                RedisKey key = club.ondoId.ToString();
                JObject o = (JObject)JToken.FromObject(club);

                redis.StringSet(key, o.ToString());
            }
        }

        public void PersistShopData()
        {
            ShopCalculations sc = new ShopCalculations();
            List<ShopEntity> shops = sc.CalculateShopData();
            var redis = Redis.Connection.GetDatabase();
            foreach (ShopEntity shop in shops)
            {
                RedisKey key = shop.ondoId.ToString();
                JObject o = (JObject)JToken.FromObject(shop);

                redis.StringSet(key, o.ToString());
            }
        }

        public void PersistTradeUnionData()
        {
            TradeUnionCalculations tc = new TradeUnionCalculations();
            List<TradeUnionEntity> tradeUnions = tc.CalculateTradeUnionData();
            var redis = Redis.Connection.GetDatabase();
            foreach (TradeUnionEntity tradeUnion in tradeUnions)
            {
                RedisKey key = tradeUnion.ondoId.ToString();
                JObject o = (JObject)JToken.FromObject(tradeUnion);

                redis.StringSet(key, o.ToString());
            }
        }
    }
}
