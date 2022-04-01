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


        public List<VehicleBrand> GetAllBrands()
        {
            //HttpResponseMessage responseMessage = httpClient.GetAsync($"VehicleBrands/Search_Vehicles/{5}/").Result;

            List<VehicleBrand> obj = null;

            HttpResponseMessage responseMessage = httpClient.GetAsync($"VehicleBrands").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = responseMessage.Content.ReadAsStringAsync().Result;
                obj = JsonConvert.DeserializeObject<List<VehicleBrand>>(data);
            }
            return obj;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(GetAllBrands());
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



        [HttpGet]
        public VehicleBrand Details(int id)
        {
            VehicleBrand record = null;
            HttpResponseMessage responseMessage = httpClient.GetAsync($"VehicleBrands/{id}").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                record = JsonConvert.DeserializeObject<VehicleBrand>(result);
            }
            return record;
        }

        [HttpPost]
        public IActionResult Edit(VehicleBrand vehicleBrand)
        {
            HttpResponseMessage responseMessage = httpClient.PutAsJsonAsync($"VehicleBrands/{vehicleBrand.Id}", vehicleBrand).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(vehicleBrand);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            HttpResponseMessage responseMessage = httpClient.DeleteAsync($"VehicleBrands/{id}").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return BadRequest();
        }





    }
}
