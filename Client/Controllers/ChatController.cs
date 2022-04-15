﻿using DataAcessLayer.Models.Chat;
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
                ViewBag.RideId = RideId;
                //else
                //{
                //    return View();
                //}
                return View(messages);

            }
            return View();
        }


        [HttpPost]
        public IActionResult PostMessage(string Message, int? RoomId, int? PublisherId, int? RideId)
        {

            if (RoomId == null)
            {
                RoomId = (CreateRoom((int)PublisherId, (int)RideId));
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

                }
            }
            return View();
        }

        [HttpGet]
        public int CreateRoom(int PublisherId, int RideId)
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


        public IActionResult Rooms()
        {
            var UserId = HttpContext.Session.GetInt32("UserId");
            HttpResponseMessage responseMessage = httpClient.GetAsync($"ChatRoom/UserRooms/{UserId}").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string res = responseMessage.Content.ReadAsStringAsync().Result;
                List<ChatRoom> UserRooms = JsonConvert.DeserializeObject<List<ChatRoom>>(res);
                return View(UserRooms);
            }
            return BadRequest();
        }


        public IActionResult Details(int RoomId)
        {
            HttpResponseMessage responseMessage = httpClient.GetAsync($"ChatMessage/GetRoomMessages/{RoomId}").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string res = responseMessage.Content.ReadAsStringAsync().Result;
                List<ChatMessages> messages = JsonConvert.DeserializeObject<List<ChatMessages>>(res);
                return View(messages);
            }
            return View();
        }
    }
}