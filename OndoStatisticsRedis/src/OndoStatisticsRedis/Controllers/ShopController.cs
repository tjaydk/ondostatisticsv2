using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OndoStatisticsRedis.Services.Redis;
using OndoStatisticsRedis.Exceptions;
using OndoStatisticsRedis.Services.Security;
using System.Text;
using Newtonsoft.Json;
using OndoStatisticsRedis.Models.LoginModels;
using OndoStatisticsRedis.Services.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO.Compression;
using System.IO;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OndoStatisticsRedis.Controllers
{
    [Route("api/[controller]")]
    public class ShopController : Controller
    {
        // GET api/shop/ondoId
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Redis redis = new Redis();
                string token = "";
                Request.Cookies.TryGetValue("apiCookie", out token); //kommentar
                Authorize.checkAuth(token, "shop");

                //string jsonTest = "{\"ondoId\":10112,\"title\":\"MØN TILBUD\",\"profilePicture\":\"https://ondodotsservice.blob.core.windows.net/profile/dd71876a-15a3-448f-9901-811909059418.jpg\",\"transactionsCurrentQuarterShop\": 200,\"transactionsCurrentWeekShop\": 2,\"subscriptionsCurrentQuarterShop\": 23,\"subscriptionsCurrentWeekShop\": 1,\"transactionsCurrentQuarterCity\": 1230,\"transactionsCurrentWeekCity\": 54,\"subscriptionsCurrentQuarterCity\": 120,\"subscriptionsCurrentWeekCity\": 32,\"subsriptionsQuarterCityAvg\": 20,\"transactionsQuarterCityAvg\": 25,\"pointsCurrentQuarter\": 5400,\"weekNo\": 42,\"weekDataArray\": [{\"weekLabel\": \"Uge 30\",\"subscriptions\": 23,\"transactions\": 12,\"activity\": 35},{\"weekLabel\": \"Uge 31\",\"subscriptions\": 40,\"transactions\": 5,\"activity\": 45},{\"weekLabel\": \"Uge 32\",\"subscriptions\": 7,\"transactions\": 25,\"activity\": 32}],\"dailyDataArray\": [{\"date\": \"1-10-2016\",\"subscriptions\": 2,\"transactions\": 0},{\"date\": \"2-10-2016\",\"subscriptions\": 0,\"transactions\": 3},{\"date\": \"3-10-2016\",\"subscriptions\": 5,\"transactions\": 6},{\"date\": \"4-10-2016\",\"subscriptions\": 0,\"transactions\": 0}],\"historyDataArray\": [{\"quarterLabel\": \"3. Kvartal 2016\",\"subscriptions\": 34,\"transactions\": 430,\"points\": 3420,\"historyWeekDataArray\": [{\"weekLabel\": \"Uge 30\",\"subscriptions\": 23,\"transactions\": 12,\"activity\": 35},{\"weekLabel\": \"Uge 31\",\"subscriptions\": 40,\"transactions\": 5,\"activity\": 45},{\"weekLabel\": \"Uge 32\",\"subscriptions\": 7,\"transactions\": 25,\"activity\": 32}]}]}";

                return Ok(redis.getOndoById(id));
            }
            catch (DivideByZeroException)
            {
                return StatusCode(500, "{\"Message\": \"Server tried to divide by 0\"}");
            }
            catch (LoginException e)
            {
                return StatusCode(403, "{\"Message\": \"" + e.Message + "\"}");
            }
            catch (TypeAccessException e)
            {
                return StatusCode(403, "{\"Message\": \"" + e.Message + "\"}");
            }
            catch (Exception)
            {
                return StatusCode(500, "{\"Message\": \"Something went wrong\"}");
            }
        }
    }
}
