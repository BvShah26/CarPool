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

        //Publish Ride
        [HttpGet]
        public IActionResult Index()
        {
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
            return RedirectToAction("Login", "Account");

        }

        public IActionResult Publish(PublishRide ride)
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

        //Get All Published Ride of user
        public IActionResult List()
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

        //Archived Rides
        public IActionResult History()
        {

            //Here Our Publishion and also Traveled Ride
            //Might Be From Booking Also

            //call api of published ride
            //call api of booking
            //Combine it and distinct it
            //Distinct it

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


        //Details of published ride
        [HttpGet]
        public IActionResult Details(int id)
        {

            //Parameter Validation 
            //Also at api side //this id should be of current user only otherwise Unauthorized()
            
            //Return View Accordingly Owner And Search


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
