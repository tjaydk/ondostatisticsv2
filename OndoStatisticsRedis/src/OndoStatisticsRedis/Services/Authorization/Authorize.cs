using Newtonsoft.Json;
using OndoStatisticsRedis.Exceptions;
using OndoStatisticsRedis.Models.LoginModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OndoStatisticsRedis.Services.Authorization
{
    public class Authorize
    {
        internal static void checkAuth(string token, string type)
        {
            Byte[] tokenArr = Convert.FromBase64String(token);
            String json = Encoding.UTF8.GetString(tokenArr);
            Auth auth = JsonConvert.DeserializeObject<Auth>(json);

            if (token == null)
            {
                throw new LoginException("You are not logged in");
            }

            switch(type)
            {
                case "tradeunion":
                    if (!auth.type1) { throw new TypeAccessException("You are not autorized to see this data"); }
                    break;
                case "shop":
                    if (!auth.type2) { throw new TypeAccessException("You are not autorized to see this data"); }
                    break;
                case "club":
                    if (!auth.type3) { throw new TypeAccessException("You are not autorized to see this data"); }
                    break;
                default:
                    throw new TypeAccessException("You are not autorized to see this data");
            }
        }
    }
}
