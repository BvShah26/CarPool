using DataAcessLayer;
using DataAcessLayer.Models.Chat;
using DataAcessLayer.ViewModels.Chat;
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
        public IActionResult Index(int RideId, int publisherId, int? PartnerId)
        {

            string returnUrl = HttpContext.Request.Path + "?RideId=" + RideId + "&publisherId=" + publisherId+ "&PartnerId="+ PartnerId;
            if (IsLogin() == true)
            {


                int UserId = (int)((int)HttpContext.Session.GetInt32("UserId") == publisherId ? PartnerId : publisherId);

                List<ChatMessages> list = null;

                HttpResponseMessage responseMessage = httpClient.GetAsync($"ChatRoom?RideId={RideId}&PublisherId={publisherId}&UserId={UserId}").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    List<ChatMessages> messages = new List<ChatMessages>();
                    ViewBag.PublisherId = publisherId;
                    if (res != "0")
                    {
                        int RoomId = JsonConvert.DeserializeObject<int>(res);
                        ViewBag.RoomId = RoomId;
                        return RedirectToAction("Details", new { RoomId = RoomId });
                    }

                    ViewBag.RideId = RideId;
                    ViewBag.PublisherId = publisherId;

                    if(PartnerId != null)
                    {
                        ViewBag.Partner = PartnerId;
                    }

                    return View("Details");

                }
                return View();
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });

        }


        [HttpPost]
        public IActionResult PostMessage(string Message, int? RoomId, int? PublisherId,int? PartnerId, int? RideId)
        {
            if (IsLogin() == true)
            {
                if (RoomId == null)
                {
                    if(PublisherId == HttpContext.Session.GetInt32("UserId"))
                    {
                        RoomId = (CreateRoom((int)PublisherId, (int)RideId, (int)PartnerId) );

                    }
                    else
                    {

                    RoomId = (CreateRoom((int)PublisherId, (int)RideId));
                    }
                }
                if (RoomId != -1)
                {
                    var UserId = HttpContext.Session.GetInt32("UserId");

                    ChatMessages message = new ChatMessages()
                    {
                        Message = Message,
                        RoomId = (int)RoomId,
                        SenderId = (int)UserId
                    };
                    HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("ChatMessage", message).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Details", new { RoomId = RoomId });
                    }
                }
                return BadRequest();
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public int CreateRoom(int PublisherId, int RideId)
        {
            if (IsLogin() == true)
            {
                var UserId = HttpContext.Session.GetInt32("UserId");
                ChatRoom room = new ChatRoom()
                {

                    PublisherId = (int)PublisherId,
                    RideId = (int)RideId,
                    RiderId = (int)UserId,

                };
                HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("ChatRoom/CreateRoom", room).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    ChatRoom record = JsonConvert.DeserializeObject<ChatRoom>(res);
                    return record.Id;
                }
                return -1;
            }
            return -1;

        }

        [HttpGet]
        public int CreateRoom(int PublisherId, int RideId,int PartnerId)
        {
            if (IsLogin() == true)
            {
                ChatRoom room = new ChatRoom()
                {

                    PublisherId = (int)PublisherId,
                    RideId = (int)RideId,
                    RiderId = (int)PartnerId,

                };
                HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("ChatRoom/CreateRoom", room).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    ChatRoom record = JsonConvert.DeserializeObject<ChatRoom>(res);
                    return record.Id;
                }
                return -1;
            }
            return -1;

        }


        public IActionResult Rooms()
        {
            string returnUrl = HttpContext.Request.Path;
            if (IsLogin() == true)
            {

                var UserId = HttpContext.Session.GetInt32("UserId");
                HttpResponseMessage responseMessage = httpClient.GetAsync($"ChatRoom/UserRooms/{UserId}").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    List<ChatInboxesViewModel> UserRooms = JsonConvert.DeserializeObject<List<ChatInboxesViewModel>>(res);
                    return View(UserRooms);
                }
                throw new Exception("Server Not Responding");
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });

        }


        public IActionResult Details(int RoomId)
        {
            string returnUrl = HttpContext.Request.Path + "?RoomId=" + RoomId;

            if (IsLogin() == true)
            {
                HttpResponseMessage responseMessage = httpClient.GetAsync($"ChatMessage/GetRoomMessages/{RoomId}").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    List<ChatMessages> messages = JsonConvert.DeserializeObject<List<ChatMessages>>(res);

                    ViewBag.CurrentUser = HttpContext.Session.GetInt32("UserId");
                    //Get Details of Ride
                    ViewBag.RoomId = RoomId;
                    return View(messages);
                }
                return View();
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });

        }


        public Boolean IsLogin()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return false;
            }
            return true;
        }
    }
}
