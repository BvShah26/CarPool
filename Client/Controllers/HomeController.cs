using DataAcessLayer.Models.Rides;
using DataAcessLayer.ViewModels;
using DataAcessLayer.ViewModels.Client;
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
    public class HomeController : Controller
    {
        HttpClient httpClient;
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config.GetValue<string>("proxyUrl"));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult sortRecords(IEnumerable<RideDetailsView> rides,int Seat,string field)
        {
            
            ViewBag.Seat = Seat;
            
            if(field == "PickUp_Time")
            {
                TimeSpan time = TimeSpan.Parse("07:35");
                List<RideDetailsView> rec = rides.OrderBy(x => TimeSpan.Parse(x.PickUp_Time.ToShortTimeString())).ToList();
                return PartialView("_RidePartial", rec);
            }
            System.Reflection.PropertyInfo prop = typeof(RideDetailsView).GetProperty(field);
            List<RideDetailsView> records = rides.OrderBy(x => prop.GetValue(x, null)).ToList();

            return PartialView("_RidePartial", records);
        }

        [HttpPost]
        public IActionResult Index(SearchRide searchRide)
        {
            HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("SearchRide", searchRide).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string res = responseMessage.Content.ReadAsStringAsync().Result;
                List<PublishRide> data = JsonConvert.DeserializeObject<List<PublishRide>>(res);
                List<RideDetailsView> rides = new List<RideDetailsView>();
                foreach (var item in data)
                {
                    var PickupDiff = Math.Round(GetDistance(item.PickUp_LatLong, searchRide.PickUp_LatLong));
                    var DropDiff = Math.Round(GetDistance(item.DropOff_LatLong, searchRide.DropOff_LatLong));


                    RideDetailsView rideDetails = new RideDetailsView()
                    {
                        Id = item.Id,
                        PublisherId = item.PublisherId,
                        PublisherName = item.Publisher.Name,
                        Publisher_Profile = item.Publisher.ProfileImage,

                        Price_Seat = item.Price_Seat * searchRide.SeatCount,

                        PickUp_Time = item.PickUp_Time,
                        DropOff_Time = item.DropOff_Time,

                        DropOff_LatLong = item.DropOff_LatLong,
                        PickUp_LatLong = item.PickUp_LatLong,

                        Pickup_DifferDistance = PickupDiff,
                        Drop_DifferDistance = DropDiff,

                        IsInstant_Approval = item.IsInstant_Approval,

                        Client_Departure_LatLong = searchRide.Departure_City,
                        Client_Destination_LatLong = searchRide.DropOff_LatLong,
                        JourneyDate = item.JourneyDate,

                        DepartureCity = item.Departure_City,
                        DestinationCity = item.Destination_City,

                       
                    };
                    rides.Add(rideDetails);

                }
                
                ViewBag.Seat = searchRide.SeatCount;
                ViewBag.Pickup = searchRide.PickUp_LatLong;
                ViewBag.Destination= searchRide.DropOff_LatLong;

                //FOR NO RIDE
                ViewBag.FromCity = searchRide.Departure_City;
                ViewBag.ToCity = searchRide.Destination_City;
                return View("Rides", rides);
            }
            return View();
        }


        [HttpGet]
        public IActionResult Details(int RideId, int Pickup_dist, int Drop_dist, int Seat,int Publisher)
        {
            //Edit Ride

            if(Publisher == HttpContext.Session.GetInt32("UserId"))
            {
                //Edit Published Ride 
                ViewBag.IsPublisher = true;

                //return to publish ride details .. so can accept decline
            }
            HttpResponseMessage responseMessage = httpClient.GetAsync($"SearchRide/RideDetails/{RideId}").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string res = responseMessage.Content.ReadAsStringAsync().Result;
                UserRideDetailsViewModel rideDetail = JsonConvert.DeserializeObject<UserRideDetailsViewModel>(res);
                rideDetail.Price_Seat = Seat * rideDetail.Price_Seat;
                ViewBag.Pickup_LatLong = Pickup_dist;
                ViewBag.Drop_LatLong = Drop_dist;
                ViewBag.Seats = Seat;

                return View(rideDetail);
            }
            return View();
        }



        public double GetDistance(string p1, string p2)
        {
            var R = 6371; // Radius of the earth in km

            string[] Departure = p1.Split(",");
            double lat1 = double.Parse(Departure[0]);
            double lon1 = double.Parse(Departure[1]);

            string[] Destination = p2.Split(",");
            double lat2 = double.Parse(Destination[0]);
            double lon2 = double.Parse(Destination[1]);


            var dLat = deg2rad(lat2 - lat1);  // deg2rad below
            var dLon = deg2rad(lon2 - lon1);


            var a =
              Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; // Distance in km
            return d;
        }

        public double deg2rad(double deg)
        {
            return deg * (Math.PI / 180);
        }

    }


}
