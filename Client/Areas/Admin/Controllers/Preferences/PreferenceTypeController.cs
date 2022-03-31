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
            return View();
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
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PreferenceTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PreferenceTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: PreferenceTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PreferenceTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: PreferenceTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PreferenceTypeController/Delete/5
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
