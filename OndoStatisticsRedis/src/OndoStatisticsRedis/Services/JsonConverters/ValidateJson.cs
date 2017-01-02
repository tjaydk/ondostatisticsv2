using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OndoStatisticsRedis.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OndoStatisticsRedis.Services.JsonConverters
{
    public static class ValidateJson
    {
        public static bool ValidateJSON(this string s)
        {
            try
            {
                JToken.Parse(s);
                return true;
            }
            catch (Exception)
            {
                throw new NotValidJsonException("The Json is not in a valid format, " + s);
            }
        }
    }
}
