using DataAcessLayer.Models.Preferences;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Client.Areas.Admin.Controllers.Preferences
{
    [Area("Admin")]
    public class PreferenceTypeController : Controller
    {
        HttpClient httpClient;
        public PreferenceTypeController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44372/api/");
        }
        // GET: PreferenceTypeController
        public ActionResult Index()
        {
            var data = GetAllPreference();
            return View(data);
        }

        public List<PreferenceType> GetAllPreference()
        {
            HttpResponseMessage responseMessage = httpClient.GetAsync("PreferenceType").Result;
            List<PreferenceType> records = null;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                records = JsonConvert.DeserializeObject<List<PreferenceType>>(result);
            }
            return records;
        }

        // GET: PreferenceTypeController/Details/5
        public PreferenceType Details(int id)
        {
            PreferenceType record = null;
            HttpResponseMessage responseMessage = httpClient.GetAsync($"PreferenceType/{id}").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                record = JsonConvert.DeserializeObject<PreferenceType>(result);
            }
            return record;
        }

        // GET: PreferenceTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PreferenceTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PreferenceType preferenceType)
        {
            try
            {
                HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("PreferenceType", preferenceType).Result;
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

        // POST: PreferenceTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TravelPreference preference)
        {
            try
            {
                HttpResponseMessage responseMessage = httpClient.PutAsJsonAsync($"PreferenceType/{id}", preference).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: PreferenceTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            HttpResponseMessage responseMessage = httpClient.DeleteAsync($"PreferenceType/{id}").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            return BadRequest();
        }
    }
}
