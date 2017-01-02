using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using OndoStatisticsRedis.Services;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using OndoStatisticsRedis.Exceptions;
using OndoStatisticsRedis.Services.Security;
using Microsoft.AspNetCore.Http;
using System.Text;
using OndoStatisticsRedis.Models.LoginModels;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OndoStatisticsRedis.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginForm form)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    //Hashes the password
                    LoginForm newForm = new LoginForm()
                    {
                        Login = form.Login,
                        Password = Hashing.CalculateSHA256Hash(form.Password),
                        IsTest = form.IsTest
                    };

                    //adds base address and headers for future calls
                    client.BaseAddress = new Uri("http://lfservice.cloudapp.net/LFService.svc/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Turns data into json which can then be posted via a body
                    StringContent content = new StringContent(JsonConvert.SerializeObject(newForm), Encoding.UTF8, "application/json");

                    //Posts the content on basepath + LFLogin and waits for the response
                    HttpResponseMessage response = await client.PostAsync("LFLogin", content);
                    if (response.IsSuccessStatusCode)
                    {
                        //Reads the response
                        string data = await response.Content.ReadAsStringAsync();
                        //Creates a tokenForm Object from the response
                        TokenForm tokenForm = JsonConvert.DeserializeObject<TokenForm>(data);
                        //Turns the object into json which can then be posted via a body
                        StringContent tokenContent = new StringContent(JsonConvert.SerializeObject(tokenForm), Encoding.UTF8, "application/json");
                        //Posts the content on basepath + LFGetOndons and waits for the response
                        response = await client.PostAsync("LFGetOndos", tokenContent);
                        //authorization entity
                        Auth auth = new Auth();


                        List<OndoIdForm> ondos = new List<OndoIdForm>();

                        if (response.IsSuccessStatusCode)
                        {
                            //Reads the response
                            data = await response.Content.ReadAsStringAsync();
                            //Creates a cookie used to authenticate apis
                            DateTime expireTime = DateConverter.getTimeInDK();
                            ListOndoIdForm listOfOndos = JsonConvert.DeserializeObject<ListOndoIdForm>(data);
                            //If the response don't include any ondos the login is not valid
                            if (listOfOndos.ListOfOndos == null)
                            {
                                throw new LoginException(listOfOndos.ResultString);
                            } else
                            {
                                foreach (OndoIdForm of in listOfOndos.ListOfOndos)
                                {
                                    of.Type = MapIconChecker.checkType(of.MapIcon);
                                    if (of.Type == 1) { auth.type1 = true; }
                                    if (of.Type == 2) { auth.type2 = true; }
                                    if (of.Type == 3) { auth.type3 = true; }
                                }
                            }
                            //Creates a token
                            var tokenBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(auth));
                            var hashedToken = Convert.ToBase64String(tokenBytes);
                            Response.Cookies.Append(
                                "apiCookie",
                                hashedToken,
                                new CookieOptions()
                                {
                                    Path = "/",
                                    Expires = expireTime,
                                    HttpOnly = false,
                                    Secure = false
                                }
                            );
                            return Ok(listOfOndos.ListOfOndos);
                        }
                        else
                        {
                            throw new LoginException("There is a problem with authentication");
                        }
                    }
                    else
                    {
                        throw new LoginException("Wrong username or password");
                    }
                }
            }
            catch (LoginException e)
            {
                return NotFound("{\"Message\": \"" + e.Message + "\"}");
            }
            catch (NotValidJsonException e)
            {
                return BadRequest("{\"Message\": \"" + e.Message + "\"}");
            }
            catch (UnauthorizedAccessException e)
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
