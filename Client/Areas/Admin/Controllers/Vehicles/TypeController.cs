using DataAcessLayer.Models.VehicleModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Client.Areas.Admin.Controllers.Vehicles
{
    [Area("Admin")]
    public class TypeController : Controller
    {
        HttpClient httpClient;

        public TypeController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44372/api/");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage responseMessage = httpClient.GetAsync("VehicleTypes").Result;
            if(responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                List<VehicleType> records = JsonConvert.DeserializeObject<List<VehicleType>>(result);
                return View(records);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(VehicleType vehicleType)
        {
            HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("VehicleTypes",vehicleType).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
