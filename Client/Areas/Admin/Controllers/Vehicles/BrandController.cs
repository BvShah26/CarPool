using DataAcessLayer.Models.VehicleModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

using System.Threading.Tasks;
using Apis.Helper;

namespace Client.Areas.Admin.Controllers.Vehicles
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        HttpClient httpClient;
        public BrandController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44372/api/");
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage responseMessage = httpClient.GetAsync($"VehicleBrands/Search_Vehicles/{5}/").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = responseMessage.Content.ReadAsStringAsync().Result;
                List<VehicleBrand> obj = JsonConvert.DeserializeObject<List<VehicleBrand>>(data);
                return View(obj);
            }
            return View();
        }

        [HttpGet]
        public async Task<List<VehicleBrand>> Pages(int PageSize, string SearchValue)
        {
            HttpResponseMessage responseMessage;
            responseMessage = httpClient.GetAsync($"VehicleBrands/Search_Vehicles/{PageSize}/{SearchValue}").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                List<VehicleBrand> brandDetails = JsonConvert.DeserializeObject<List<VehicleBrand>>(result);
                return brandDetails;
            }
            return null;
        }

        /// <summary>
        /// Add New Record
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(VehicleBrand brand)
        {
            HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("VehicleBrands", brand).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }


        /// <summary>
        /// Edit record Section
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            HttpResponseMessage responseMessage = httpClient.GetAsync($"VehicleBrands/{id}").Result;
            if(responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                VehicleBrand vehicleBrand = JsonConvert.DeserializeObject<VehicleBrand>(result);
                return View(vehicleBrand);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(VehicleBrand vehicleBrand)
        {
            HttpResponseMessage responseMessage = httpClient.PutAsJsonAsync($"VehicleBrands/{vehicleBrand.Id}",vehicleBrand).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(vehicleBrand);
        }


        


    }
}
