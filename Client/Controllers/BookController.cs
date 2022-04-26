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
            string returnUrl = HttpContext.Request.Path + "?RideId=" + RideId + "&Seat=" + Seat;
            if (IsLogin() == true)
            {
                //Confirmation Page Here
                int UserId = (int)HttpContext.Session.GetInt32("UserId");
                ViewBag.UserId = UserId;
                try
                {
                    HttpResponseMessage responseMessage = httpClient.GetAsync($"SearchRide/VerifyRide/{RideId}/{Seat}/{UserId}").Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        string res = responseMessage.Content.ReadAsStringAsync().Result;
                        BookigConfirmationViewModel record = JsonConvert.DeserializeObject<BookigConfirmationViewModel>(res);
                        return View(record);
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Api Not Called");
                }
                return BadRequest();
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });
        }

        [HttpGet]
        public IActionResult Confirmed(int Id, int SeatQty, int? UserId)
        {
            if (IsLogin() == true)
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
            return RedirectToAction("Login", "Account");


        }

        [HttpGet]
        public IActionResult Request(int Id, int SeatQty)
        {
            if (IsLogin() == true)
            {
                RideApproval rideApproval = new RideApproval()
                {
                    RideId = Id,
                    UserId = (int)HttpContext.Session.GetInt32("UserId"),
                    RequestedSeats = SeatQty
                };

                try
                {
                    HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("RideApprovals/Request", rideApproval).Result;
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        //Validations
                        ViewBag.RideId = Id;
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Ride Request Failed");
                }
                return View();
            }
            return RedirectToAction("Login", "Account");

        }


        [HttpGet]
        public IActionResult CancelReason(int BookingId)
        {
            HttpResponseMessage responseMessage = httpClient.GetAsync("Books/CancelReason").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string res = responseMessage.Content.ReadAsStringAsync().Result;
                List<CancellationReason> reasons = JsonConvert.DeserializeObject<List<CancellationReason>>(res);
                ViewBag.BookingId = BookingId;
                return View(reasons);
            }
            return View();
        }


        [HttpGet]
        public IActionResult CancelBooking(int ReasonId, int BookingId)
        {
            HttpResponseMessage responseMessage = httpClient.GetAsync($"books/Cancel/{ReasonId}/{BookingId}").Result;
            if(responseMessage.IsSuccessStatusCode)
            {
                //can be redirect to that specific ride Details
            }


            return RedirectToAction("Index","Rides");
        }


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
