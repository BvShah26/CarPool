using DataAcessLayer.Models.Preferences;
using DataAcessLayer.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class Preferences : Controller
    {
        HttpClient httpClient;
        private readonly IConfiguration _config;
        public Preferences(IConfiguration config)
        {
            _config = config;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config.GetValue<string>("proxyUrl"));

        }

        [HttpGet]
        public IActionResult Index()
        {
            //check for own selected
            HttpResponseMessage responsePrefType = httpClient.GetAsync($"PreferenceType").Result;
            if (responsePrefType.IsSuccessStatusCode)
            {
                string res = responsePrefType.Content.ReadAsStringAsync().Result;
                List<PreferenceType> preferenceTypes = JsonConvert.DeserializeObject<List<PreferenceType>>(res);
                return View(preferenceTypes);
            }

            return View();
        }

        [HttpGet]
        public IActionResult Choice(int Id,string Type)
        {
            HttpResponseMessage responsePrefType = httpClient.GetAsync($"PreferenceSubType/Type/{Id}").Result;
            if (responsePrefType.IsSuccessStatusCode)
            {
                string res = responsePrefType.Content.ReadAsStringAsync().Result;
                List<TravelPreference> preferenceTypes = JsonConvert.DeserializeObject<List<TravelPreference>>(res);
                ViewBag.Type = Type;
                return View(preferenceTypes);
            }

            return View();
        }


        /// <summary>
        /// bakiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii
        /// </summary>
        /// <param name="PrefId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SavePreference(int PrefId)
        {
            int UserId = (int)HttpContext.Session.GetInt32("UserId");
            User_TravelPreference preference = new User_TravelPreference()
            {
                Travel_PreferenceId = PrefId,
                UserId = UserId
            };
            HttpResponseMessage responsePreference = httpClient.PostAsJsonAsync("UserPreference", preference).Result;
            if (responsePreference.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
