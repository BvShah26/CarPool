using DataAcessLayer.Models.Rides;
using DataAcessLayer.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Controllers
{
    //[Authorize]
    public class PublishRideController : Controller
    {
        HttpClient httpClient;
        private readonly IConfiguration _config;

        public PublishRideController(IConfiguration config)
        {
            _config = config;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config.GetValue<string>("proxyUrl"));
        }

        /// <summary>
        /// Departure Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Departure()
        {
            httpClient.GetAsync().Result;
            //check for vehicle here
            return View();
        }

        [HttpPost]
        public IActionResult Departure(string Departure, string PickUp_LatLong, string Departure_City)
        {
            HttpContext.Response.Cookies.Append("Departure", Departure);
            HttpContext.Response.Cookies.Append("DepartureLatLong", PickUp_LatLong);
            HttpContext.Response.Cookies.Append("DepartureCity", Departure_City);
            return RedirectToAction("Destination");
        }




        /// <summary>
        /// Destination Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Destination()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Destination(string Destination, string Destination_LatLong, string Destination_City)
        {
            HttpContext.Response.Cookies.Append("Destination", Destination);
            HttpContext.Response.Cookies.Append("DestinationLatLong", Destination_LatLong);
            HttpContext.Response.Cookies.Append("DestinationCity", Destination_City);

            return RedirectToAction("Date");
        }







        [HttpGet]
        public IActionResult Date()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Date(string JounreyDate)
        {
            HttpContext.Response.Cookies.Append("JounreyDate", JounreyDate);
            return RedirectToAction("DepartureTime");
        }

        [HttpGet]
        public IActionResult DepartureTime()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DepartureTime(string PickupTime)
        {
            HttpContext.Response.Cookies.Append("PickupTime", PickupTime);
            return RedirectToAction("DropoffTime");
        }


        [HttpGet]
        public IActionResult DropoffTime()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DropoffTime(string DropoffTime)
        {
            HttpContext.Response.Cookies.Append("DropoffTime", DropoffTime);
            return RedirectToAction("Seat");
        }

        [HttpGet]
        public IActionResult Seat()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Seat(int seatQty)
        {
            HttpContext.Response.Cookies.Append("Seat", seatQty.ToString());
            return RedirectToAction("Type");
        }



        [HttpGet]
        public IActionResult Type()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Type(bool Type)
        {
            HttpContext.Response.Cookies.Append("Type", Type.ToString());
            return RedirectToAction("Vehicle");
        }

        [HttpGet]
        public IActionResult Vehicle()
        {
            string returnUrl = HttpContext.Request.Path;

            if (IsLogin() == true)
            {
                var Name = HttpContext.Session.GetString("UserName");
                var UserId = HttpContext.Session.GetInt32("UserId");

                ViewBag.Name = Name;
                ViewBag.Id = UserId;

                HttpResponseMessage responseMessage = httpClient.GetAsync($"Uservehicles/{UserId}").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    List<Uservehicle> records = JsonConvert.DeserializeObject<List<Uservehicle>>(res);
                    ViewBag.Vehicles = new SelectList(records, "Id", "Vehicle.Name");
                }

                return View();
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });

        }

        [HttpPost]
        public IActionResult Vehicle(int VehicleId)
        {
            HttpContext.Response.Cookies.Append("VehicleId", VehicleId.ToString());
            return RedirectToAction("Price");
        }

        [HttpGet]
        public IActionResult Price()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Price(int Price)
        {
            HttpContext.Response.Cookies.Append("Price", Price.ToString());
            return RedirectToAction("Note");
        }


        [HttpGet]
        public IActionResult Note()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Note(string rideNote)
        {

            string returnUrl = HttpContext.Request.Path;
            if (IsLogin() == true)
            {
                HttpContext.Response.Cookies.Append("Notes", rideNote);

                PublishRide ride = new PublishRide()
                {
                    
                    Departure_City = Request.Cookies["DepartureCity"],
                    PickUp_LatLong = Request.Cookies["DepartureLatLong"],
                    PickUp_Location = Request.Cookies["Departure"],
                    PickUp_Time = DateTime.Parse(Request.Cookies["PickupTime"]),

                    Destination_City = Request.Cookies["DestinationCity"],
                    DropOff_LatLong = Request.Cookies["DestinationLatLong"],
                    DropOff_Location = Request.Cookies["Destination"],
                    DropOff_Time = DateTime.Parse(Request.Cookies["DropoffTime"]),

                    JourneyDate = DateTime.Parse(Request.Cookies["JounreyDate"]),
                    IsInstant_Approval = Boolean.Parse(Request.Cookies["Type"]),
                    PublisherId = (int)HttpContext.Session.GetInt32("UserId"),

                    MaxPassengers = Int32.Parse(Request.Cookies["Seat"]),
                    VehicleId = Int32.Parse(Request.Cookies["VehicleId"]),
                    Price_Seat = Int32.Parse(Request.Cookies["Price"]),
                    Ride_Note = Request.Cookies["Notes"],
                };

                HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("PublishRides", ride).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;

                    PublishRide data = JsonConvert.DeserializeObject<PublishRide>(res);

                    Response.Cookies.Delete("Departure");
                    Response.Cookies.Delete("DepartureLatLong");
                    Response.Cookies.Delete("DepartureCity");

                    Response.Cookies.Delete("PickupTime");
                    Response.Cookies.Delete("DropoffTime");

                    Response.Cookies.Delete("Destination");
                    Response.Cookies.Delete("DestinationLatLong");
                    Response.Cookies.Delete("DestinationCity");
                    Response.Cookies.Delete("Type");
                    Response.Cookies.Delete("JounreyDate");

                    Response.Cookies.Delete("Notes");
                    Response.Cookies.Delete("Price");
                    Response.Cookies.Delete("VehicleId");
                    Response.Cookies.Delete("Seat");


                    return RedirectToAction("Offer", "Rides", new { RideId = data.Id });
                }
                throw new Exception("Fail Ride Publishing");
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });
        }

























        //Publish Ride
        [HttpGet]
        public IActionResult Index()
        {
            string returnUrl = HttpContext.Request.Path;

            if (IsLogin() == true)
            {
                var Name = HttpContext.Session.GetString("UserName");
                var UserId = HttpContext.Session.GetInt32("UserId");

                ViewBag.Name = Name;
                ViewBag.Id = UserId;

                HttpResponseMessage responseMessage = httpClient.GetAsync($"Uservehicles/{UserId}").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    List<Uservehicle> records = JsonConvert.DeserializeObject<List<Uservehicle>>(res);
                    ViewBag.Vehicles = new SelectList(records, "Id", "Vehicle.Name");
                }

                return View();
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });


        }

        public IActionResult Publish(PublishRide ride)
        {
            if (IsLogin() == true)
            {
                HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("PublishRides", ride).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;

                    PublishRide data = JsonConvert.DeserializeObject<PublishRide>(res);
                    return Ok(data);

                }
                throw new Exception("Fail Ride Publishing");
            }
            return RedirectToAction("Login", "Account");

        }

        //Get All Published Ride of user
        public IActionResult List()
        {
            string returnUrl = HttpContext.Request.Path;

            if (IsLogin() == true)
            {
                var UserId = HttpContext.Session.GetInt32("UserId");
                HttpResponseMessage responseMessage = httpClient.GetAsync($"PublishRides/UserVehicles/{UserId}").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    List<PublishRide> rides = JsonConvert.DeserializeObject<List<PublishRide>>(res);
                    return View(rides);
                }
                return BadRequest();
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });

        }

        //Archived Rides
        public IActionResult History()
        {

            //Here Our Publishion and also Traveled Ride
            //Might Be From Booking Also

            //call api of published ride
            //call api of booking
            //Combine it and distinct it
            //Distinct it
            string returnUrl = HttpContext.Request.Path;

            if (IsLogin() == true)
            {
                var UserId = HttpContext.Session.GetInt32("UserId");
                HttpResponseMessage responseMessage = httpClient.GetAsync($"PublishRides/History/{UserId}").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    List<PublishRide> rides = JsonConvert.DeserializeObject<List<PublishRide>>(res);
                    return View(rides);
                }
                return BadRequest();
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });

        }


        //Details of published ride
        [HttpGet]
        public IActionResult Details(int id)
        {

            //Parameter Validation 
            //Also at api side //this id should be of current user only otherwise Unauthorized()

            //Return View Accordingly Owner And Search

            string returnUrl = HttpContext.Request.Path;

            if (IsLogin() == true)
            {
                HttpResponseMessage responseMessage = httpClient.GetAsync($"PublishRides/{id}").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    PublishRide ride = JsonConvert.DeserializeObject<PublishRide>(res);
                    //return Ok(ride);
                    return View(ride);
                    //return Ok();
                }
                return BadRequest();
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });

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
