using DataAcessLayer.Models.Rides;
using DataAcessLayer.ViewModels.Client;
using DataAcessLayer.ViewModels.Ride;
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
    public class RidesController : Controller
    {
        HttpClient httpClient;
        private readonly IConfiguration _config;
        public RidesController(IConfiguration config)
        {
            _config = config;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config.GetValue<string>("proxyUrl"));

        }
        [HttpGet]
        public IActionResult Index()
        {
            string returnUrl = HttpContext.Request.Path;
            if (IsLogin() == true)
            {

                int UserId = (int)HttpContext.Session.GetInt32("UserId");
                HttpResponseMessage responseMessage = httpClient.GetAsync($"UserRides/{UserId}").Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    List<UserRideViewModal> rides = JsonConvert.DeserializeObject<List<UserRideViewModal>>(res);

                    ViewBag.UserId = UserId;

                    return View(rides);
                }
                return View();
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });
        }


        [HttpGet]
        public IActionResult Details(int RideId,int PublisherId)
        {
            string returnUrl = HttpContext.Request.Path + "/" + RideId;
            if (IsLogin() == true)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UserId");
                if(PublisherId == UserId)
                {
                    return RedirectToAction("Offer",new { RideId});
                }
                else
                {
                    return RedirectToAction("Bookings",new { RideId });
                }
                //HttpResponseMessage responseRide = httpClient.GetAsync($"PublishRides/GetRideDetailsUser/{RideId}").Result;
                //if (responseRide.IsSuccessStatusCode)
                //{
                //    string res = responseRide.Content.ReadAsStringAsync().Result;
                //    PublishRide record = JsonConvert.DeserializeObject<PublishRide>(res);
                //    // booking of current user

                //    //var bookingId = record.Booking.Where()

                //    //var bookingId = 


                //    return View(record);
                //}
                //return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });


        }


        [HttpGet]
        public IActionResult Bookings(int RideId)
        {
            string returnUrl = HttpContext.Request.Path+"?RideId="+RideId;

            if (IsLogin() == true)
            {

                int UserId = (int)HttpContext.Session.GetInt32("UserId");

                HttpResponseMessage responseMessage = httpClient.GetAsync($"UserRides/BookingDetails/{RideId}/{UserId}").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    UserRideDetailsViewModel rideDetail = JsonConvert.DeserializeObject<UserRideDetailsViewModel>(res);
                    rideDetail.Price_Seat = rideDetail.Price_Seat * rideDetail.Seat;
                    ViewBag.UserId = UserId;
                    return View(rideDetail);
                }
                return View();
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });

        }


        [HttpGet]
        public IActionResult Offer(int RideId)
        {
            string returnUrl = HttpContext.Request.Path + "?RideId=" + RideId;

            if (IsLogin() == true)
            {

                int UserId = (int)HttpContext.Session.GetInt32("UserId");

                HttpResponseMessage responseMessage = httpClient.GetAsync($"PublishRides/GetOffer/{RideId}").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    RideOfferViewModel rideDetail = JsonConvert.DeserializeObject<RideOfferViewModel>(res);
                    ViewBag.UserId = UserId;
                    return View(rideDetail);
                }
                return View();
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });
        }


        [HttpGet]
        public IActionResult Request(int RequestId)
        {
            string returnUrl = HttpContext.Request.Path + "?RequestId=" + RequestId;

            if (IsLogin() == true)
            {

                int UserId = (int)HttpContext.Session.GetInt32("UserId");

                HttpResponseMessage responseMessage = httpClient.GetAsync($"RideApprovals/{RequestId}").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    RideApproval rideRequest = JsonConvert.DeserializeObject<RideApproval>(res);
                    ViewBag.UserId = UserId;
                    return View(rideRequest);
                }
                return View();
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });
        }



        [HttpGet]
        public IActionResult History()
        {
            string returnUrl = HttpContext.Request.Path;

            if (IsLogin() == true)
            {

                int UserId = (int)HttpContext.Session.GetInt32("UserId");
                HttpResponseMessage responseMessage = httpClient.GetAsync($"UserRides/RideHistory/{UserId}").Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    List<UserRideViewModal> rides = JsonConvert.DeserializeObject<List<UserRideViewModal>>(res);

                    ViewBag.UserId = UserId;

                    return View(rides);
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
