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
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage responseMessage =  httpClient.GetAsync("VehicleBrands").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                List<VehicleBrand> brandDetails = JsonConvert.DeserializeObject<List<VehicleBrand>>(result);
                return View(brandDetails);
            }
            return View();
        }


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
    }
}
