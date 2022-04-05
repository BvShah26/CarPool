using DataAcessLayer.Models.Users;
using DataAcessLayer.Models.VehicleModels;
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
    public class VehicleController : Controller
    {
        HttpClient httpClient;
        private readonly IConfiguration _config;


        public VehicleController(IConfiguration config)
        {
            _config = config;

            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config.GetValue<string>("proxyUrl"));
        }

        //All Vehicles
        public IActionResult Index()
        {
            if (IsLogin() == true)
            {
                var UserId = HttpContext.Session.GetInt32("UserId");
                HttpResponseMessage responseMessage = httpClient.GetAsync($"UserVehicles/{UserId}").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    List<Uservehicle> vehicles = JsonConvert.DeserializeObject<List<Uservehicle>>(res);
                    return View(vehicles);
                }
                return RedirectToAction("Add");

            }
            return RedirectToAction("Login", "Account");

        }

        //Add Vehicles
        [HttpGet]
        public IActionResult Add()
        {
            HttpResponseMessage responseMessage = httpClient.GetAsync("Vehicles").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string res = responseMessage.Content.ReadAsStringAsync().Result;
                List<Vehicle> vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(res);

                ViewData["Vehicles"] = new SelectList(vehicles, "Id", "Name");

            }
            responseMessage = httpClient.GetAsync("VehicleColors").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string res = responseMessage.Content.ReadAsStringAsync().Result;
                List<VehicleColor> colors = JsonConvert.DeserializeObject<List<VehicleColor>>(res);

                ViewData["Colors"] = new SelectList(colors, "Id", "Color");

            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Uservehicle uservehicle)
        {
            if (IsLogin() == true)
            {
                var UserId = HttpContext.Session.GetInt32("UserId");
                uservehicle.UserOwnerId = (int)UserId;

                HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("Uservehicles", uservehicle).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    // Here Redirect To Details Page
                    // Or User Profile Page
                    return RedirectToAction("Index");
                }
                return View(uservehicle);
            }
            return RedirectToAction("Login", "Account");
        }


        //Update Vehicle
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(int id, Uservehicle uservehicle)
        {
            return View();
        }


        //Delete Vehicle
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View();
        }

        //Vehicle Details
        public IActionResult Details(int id)
        {
            return View();
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
