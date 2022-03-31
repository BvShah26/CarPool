using DataAcessLayer.Models.Preferences;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Client.Areas.Admin.Controllers.Preferences
{
    [Area("Admin")]
    public class Preference_SubTypeController : Controller
    {
        HttpClient httpClient;
        public Preference_SubTypeController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44372/api/");
        }
        // GET: Preference_SubTypeController
        public ActionResult Index()
        {
            HttpResponseMessage responseMessage = httpClient.GetAsync("PreferenceSubType").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                List<TravelPreference> records = JsonConvert.DeserializeObject<List<TravelPreference>>(result);
                return View(records);
            }
            //return NotFound();
            return View(); //validation here 
        }

        // GET: Preference_SubTypeController/Details/5
        public TravelPreference Details(int id)
        {
            TravelPreference preference = null;
            HttpResponseMessage responseMessage = httpClient.GetAsync($"PreferenceSubType/{id}").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                preference = JsonConvert.DeserializeObject<TravelPreference>(result);
            }
            return preference;
        }

        // GET: Preference_SubTypeController/Create
        [HttpGet]
        public ActionResult Create()
        {
            HttpResponseMessage responseMessage = httpClient.GetAsync("PreferenceType").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                List<PreferenceType> records = JsonConvert.DeserializeObject<List<PreferenceType>>(result);
                ViewBag.Types = new SelectList(records, "Id", "Title");
            }
            return View();
        }

        // POST: Preference_SubTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TravelPreference travelPreference)
        {
            try
            {
                HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("PreferenceSubType", travelPreference).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                throw new Exception("Api Bad Result");
            }
            catch
            {
                throw new Exception("Api Calling Failed");

            }
        }

        // GET: Preference_SubTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Preference_SubTypeController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(TravelPreference travelPreference)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Preference_SubTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            HttpResponseMessage responseMessage = httpClient.DeleteAsync($"PreferenceSubType/{id}").Result;
            return RedirectToAction("Index");
        }

        // POST: Preference_SubTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
