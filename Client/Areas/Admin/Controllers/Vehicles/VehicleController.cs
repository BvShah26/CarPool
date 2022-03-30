using DataAcessLayer.Models.VehicleModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Client.Areas.Admin.Controllers.Vehicles
{
    [Area("Admin")]
    public class VehicleController : Controller
    {
        HttpClient httpClient;
        public VehicleController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44372/api/");
        }
        [HttpGet]
        public IActionResult Index()
        {
            HttpResponseMessage responseMessage = httpClient.GetAsync("Vehicles").Result;
            string result = responseMessage.Content.ReadAsStringAsync().Result;
            List<Vehicle> records = JsonConvert.DeserializeObject<List<Vehicle>>(result);
            return View(records);
        }

        [HttpGet]
        public IActionResult Add()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44372/api/");

            HttpResponseMessage responseMessage = httpClient.GetAsync("VehicleBrands").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                List<VehicleBrand> vehicleBrands = JsonConvert.DeserializeObject<List<VehicleBrand>>(result);
                ViewData["Brands"] = new SelectList(vehicleBrands, "Id", "Name");
            }

            responseMessage = httpClient.GetAsync("VehicleTypes").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                List<VehicleType> vehicleTypes = JsonConvert.DeserializeObject<List<VehicleType>>(result);
                ViewData["Types"] = new SelectList(vehicleTypes, "Id", "Title");
            }
            //New Api Here
            return View();
        }

        [HttpPost]
        public IActionResult Add(Vehicle vehicle)
        {
            HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("Vehicles",vehicle).Result;
            if(responseMessage.IsSuccessStatusCode)
            {
                //return to details page
            }
            return RedirectToAction("Index");
        }
    }
}
