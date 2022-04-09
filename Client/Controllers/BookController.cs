using DataAcessLayer.Models.Booking;
using DataAcessLayer.Models.Rides;
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
    public class BookController : Controller
    {
        HttpClient httpClient;
        private readonly IConfiguration _config;

        public BookController(IConfiguration config)
        {
            _config = config;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config.GetValue<string>("proxyUrl"));
        }


        [HttpGet]
        public IActionResult Index(int RideId, int Seat)
        {
            string returnUrl = HttpContext.Request.Path;
            if (IsLogin() == true)
            {
                //Confirmation Page Here

                HttpResponseMessage responseMessage = httpClient.GetAsync($"SearchRide/VerifyRide/{RideId}").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    PublishRide ride = JsonConvert.DeserializeObject<PublishRide>(res);

                    ViewBag.Price = ride.Price_Seat;
                    ride.Price_Seat = ride.Price_Seat * Seat;

                    ViewBag.Seat = Seat;
                    return View(ride);
                }
                return BadRequest();
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });
        }

        [HttpPost]
        public IActionResult Confirmed(int Price, int Id, int SeatQty)
        {
            Book book = new Book()
            {
                Publish_RideId = Id,
                RiderId = (int)HttpContext.Session.GetInt32("UserId"),
                SeatQty = SeatQty,
                TotalPrice = SeatQty * Price,
            };

            HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("Books", book).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string res = responseMessage.Content.ReadAsStringAsync().Result;
            }
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Index(Book book)
        //{
        //    return View();
        //}

        public Boolean IsLogin()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return false;
            }
            return true;
        }
    }
}
