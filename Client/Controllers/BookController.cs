using DataAcessLayer.Models.Booking;
using DataAcessLayer.Models.Rides;
using DataAcessLayer.ViewModels.Bookings;
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
                int UserId = (int)HttpContext.Session.GetInt32("UserId");
                HttpResponseMessage responseMessage = httpClient.GetAsync($"SearchRide/VerifyRide/{RideId}/{Seat}/{UserId}").Result;
                if(responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    BookigConfirmationViewModel record = JsonConvert.DeserializeObject<BookigConfirmationViewModel>(res);
                    return View(record);
                }
                //if (responseMessage.IsSuccessStatusCode)
                //{
                //    string res = responseMessage.Content.ReadAsStringAsync().Result;


                //    //Change Res Type
                //    PublishRide ride = JsonConvert.DeserializeObject<PublishRide>(res);

                //    ride.Price_Seat = ride.Price_Seat * Seat;

                //    if (ride.IsInstant_Approval == false)
                //    {
                //        var UserId = HttpContext.Session.GetInt32("UserId");
                //        HttpResponseMessage approvalResponse = httpClient.GetAsync($"RideApprovals/GetUserRideRequest/{RideId}/{UserId}").Result;

                //        //Checking For Already Rejection
                //        if (approvalResponse.IsSuccessStatusCode)
                //        {
                //            string responseApproval = approvalResponse.Content.ReadAsStringAsync().Result;
                //            if(responseApproval != "")
                //            {
                //                ViewBag.HasDeclined = true;
                //            }
                //        }
                //    }

                //    ViewBag.Seat = Seat;
                //    return View(ride);
                //}
                return BadRequest();
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });
        }

        [HttpGet]
        public IActionResult Confirmed(int Id, int SeatQty, int? UserId)
        {
            HttpResponseMessage responseRide = httpClient.GetAsync($"SearchRide/GetRateSeats/{Id}").Result;
            if (responseRide.IsSuccessStatusCode)
            {
                //int Price = Int32.Parse(responseRide.Content.ReadAsStringAsync().Result);

                var anonymsDefinition = new { rate = "", MaxSeat = 0, AutoApprove = false };
                string resultRide = responseRide.Content.ReadAsStringAsync().Result;
                var dataRide = JsonConvert.DeserializeAnonymousType(resultRide, anonymsDefinition);

                //No Need to check
                //if (dataRide.AutoApprove == false)
                //{


                //    return Ok();
                //}

                PublishRide publishRide = new PublishRide()
                {
                    MaxPassengers = dataRide.MaxSeat,
                    Id = Id

                    //Clears Id if we don't specify here ( Model Reference )
                };
                Book book = new Book()
                {
                    Publish_RideId = Id,
                    RiderId = (UserId == null) ? (int)HttpContext.Session.GetInt32("UserId") : (int)UserId,
                    SeatQty = SeatQty,
                    TotalPrice = SeatQty * Int32.Parse(dataRide.rate),
                    Publish_Ride = publishRide
                };


                HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("Books", book).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    ViewBag.RideId = Id;
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
        public IActionResult Request(int Id, int SeatQty)
        {

            RideApproval rideApproval = new RideApproval()
            {
                RideId = Id,
                UserId = (int)HttpContext.Session.GetInt32("UserId"),
                RequestedSeats = SeatQty
            };

            HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("RideApprovals/Request", rideApproval).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                //Validations
                ViewBag.RideId = Id;
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
