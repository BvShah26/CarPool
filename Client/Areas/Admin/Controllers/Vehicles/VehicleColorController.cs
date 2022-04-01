using DataAcessLayer.Models.VehicleModels;
using Microsoft.AspNetCore.Http;
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
    public class VehicleColorController : Controller
    {
        HttpClient httpClient;

        public VehicleColorController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44372/api/");
        }

        // GET: VehicleColorController
        public ActionResult Index()
        {
            HttpResponseMessage responseMessage = httpClient.GetAsync("VehicleColors").Result;
            List<VehicleColor> records = null;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                records = JsonConvert.DeserializeObject<List<VehicleColor>>(result);
            }
            return View(records);
        }

        // GET: VehicleColorController/Details/5
        public VehicleColor Details(int id)
        {
            VehicleColor record = null;
            HttpResponseMessage responseMessage = httpClient.GetAsync($"VehicleColors/{id}").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                record = JsonConvert.DeserializeObject<VehicleColor>(result);
            }
            return record;
        }


        // POST: VehicleColorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VehicleColor vehicleColor)
        {
            try
            {
                HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("VehicleColors", vehicleColor).Result;
                if (responseMessage.IsSuccessStatusCode)
                {

                    return RedirectToAction(nameof(Index));
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }



        // POST: VehicleColorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VehicleColor vehicleColor)
        {
            try
            {
                HttpResponseMessage responseMessage = httpClient.PutAsJsonAsync($"VehicleColors/{id}", vehicleColor).Result;
                if (responseMessage.IsSuccessStatusCode)
                {

                    return RedirectToAction(nameof(Index));
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: VehicleColorController/Delete/5
        public ActionResult Delete(int id)
        {
            HttpResponseMessage responseMessage = httpClient.DeleteAsync($"VehicleColors/{id}").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();
        }


    }
}
