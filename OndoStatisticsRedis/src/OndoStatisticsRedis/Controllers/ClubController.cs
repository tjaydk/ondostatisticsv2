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
    public class ClubController : Controller
    {
        // GET api/club/ondoId
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Redis redis = new Redis();
                string token = "";
                Request.Cookies.TryGetValue("apiCookie", out token); //kommentar
                Authorize.checkAuth(token, "club");

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
