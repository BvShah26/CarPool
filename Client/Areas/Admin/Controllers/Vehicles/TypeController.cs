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

        public List<VehicleType> GetVehicleTypes()
        {
            List<VehicleType> records = null;
            HttpResponseMessage responseMessage = httpClient.GetAsync("VehicleTypes").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                records = JsonConvert.DeserializeObject<List<VehicleType>>(result);
            }
            return records;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            return View(GetVehicleTypes());
        }

        [HttpGet]
        public VehicleType Details(int id)
        {
            VehicleType record = null;

            HttpResponseMessage responseMessage = httpClient.GetAsync($"VehicleTypes/{id}").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                record = JsonConvert.DeserializeObject<VehicleType>(result);
            }
            return record;
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            HttpResponseMessage responseMessage = httpClient.DeleteAsync($"VehicleTypes/{id}").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleType vehicleType)
        {
            HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("VehicleTypes", vehicleType).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, VehicleType vehicleType)
        {
            HttpResponseMessage responseMessage = httpClient.PutAsJsonAsync($"VehicleTypes/{id}", vehicleType).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();
        }

    }
}
