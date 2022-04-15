using DataAcessLayer.Models.Chat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class ChatController : Controller
    {

        HttpClient httpClient;
        private readonly IConfiguration _config;
        public ChatController(IConfiguration config)
        {
            _config = config;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config.GetValue<string>("proxyUrl"));

        }
        [HttpGet]
        public IActionResult Index(int RideId, int publisherId)
        {

            var UserId = HttpContext.Session.GetInt32("UserId");
            List<ChatMessages> list = null;
            var AnonymsDefinition = new { Messages = list, RoomId = 0 };

            HttpResponseMessage responseMessage = httpClient.GetAsync($"ChatRoom?RideId={RideId}&PublisherId={publisherId}&UserId={UserId}").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string res = responseMessage.Content.ReadAsStringAsync().Result;
                List<ChatMessages> messages = new List<ChatMessages>();
                    ViewBag.PublisherId = publisherId;
                if (res != "0")
                {
                    //messages = JsonConvert.DeserializeObject<List<ChatMessages>>(res);
                    var data = JsonConvert.DeserializeAnonymousType(res, AnonymsDefinition);
                    messages = data.Messages;
                    ViewBag.RoomId = data.RoomId;
                }
                //else
                //{
                //    return View();
                //}
                return View(messages);

            }
            return View();
        }


        [HttpPost]
        public IActionResult PostMessage(string Message,int? RoomId,int? PublisherId,int? RideId)
        {
            if(RoomId == null)
            {
                var UserId = HttpContext.Session.GetInt32("UserId");
                ChatRoom room = new ChatRoom()
                {

                    PublisherId = (int)PublisherId,
                    RideId = (int)RideId,
                    RiderId = UserId;
                    
                };

                //call create room api
                //StartMessaging()
            }
            return View();
        }


        [HttpPost]
        public IActionResult StartMessaging(string Message,int PublisherId,int RideId)
        {

            return Ok();
        }
    }
}
