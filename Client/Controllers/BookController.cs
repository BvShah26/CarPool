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

                    ride.Price_Seat = ride.Price_Seat * Seat;

                    ViewBag.Seat = Seat;
                    return View(ride);
                }
                return BadRequest();
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });
        }

        [HttpPost]
        public IActionResult Confirmed(int Id, int SeatQty)
        {
            HttpResponseMessage responseRide = httpClient.GetAsync($"SearchRide/GetRateSeats/{Id}").Result;
            if (responseRide.IsSuccessStatusCode)
            {
                //int Price = Int32.Parse(responseRide.Content.ReadAsStringAsync().Result);
                var anonymsDefinition = new { rate = "", MaxSeat = 0 };
                string resultRide = responseRide.Content.ReadAsStringAsync().Result;
                var dataRide = JsonConvert.DeserializeAnonymousType(resultRide, anonymsDefinition);

                PublishRide publishRide = new PublishRide() {
                    MaxPassengers = dataRide.MaxSeat,
                    Id = Id

                    //Clears Id if we don't specify here ( Model Reference )
                };
                Book book = new Book()
                {
                    Publish_RideId = Id,
                    RiderId = (int)HttpContext.Session.GetInt32("UserId"),
                    SeatQty = SeatQty,
                    TotalPrice = SeatQty * Int32.Parse(dataRide.rate),
                    Publish_Ride = publishRide
                };


                HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("Books", book).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                }
            }
            else
            {
                throw new Exception("Api Not Caled");
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
