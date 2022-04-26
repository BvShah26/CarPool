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
            string returnUrl = HttpContext.Request.Path;

            if (IsLogin() == true)
            {
                //check for own selected
                HttpResponseMessage responsePrefType = httpClient.GetAsync($"PreferenceType").Result;
                if (responsePrefType.IsSuccessStatusCode)
                {
                    string res = responsePrefType.Content.ReadAsStringAsync().Result;
                    List<PreferenceType> preferenceTypes = JsonConvert.DeserializeObject<List<PreferenceType>>(res);
                    return View(preferenceTypes);
                }
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });


            return View();
        }

        [HttpGet]
        public IActionResult Choice(int TypeId, string ?Type)
        {
            string returnUrl = HttpContext.Request.Path + "?TypeId=" + TypeId + "&Type=" + Type;

            if (IsLogin() == true)
            {

                int UserId = (int)HttpContext.Session.GetInt32("UserId");
                HttpResponseMessage responsePrefType = httpClient.GetAsync($"UserPreference/GetSelectedPreferences/{TypeId}/{UserId}").Result;
                if (responsePrefType.IsSuccessStatusCode)
                {
                    List<TravelPreference> preferences = null;
                    var responseDefinition = new { selectedPreference = 0, preferences = preferences };


                    string res = responsePrefType.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeAnonymousType(res, responseDefinition);
                    ViewBag.Type = Type;

                    ViewBag.SelectedPrefId = data.selectedPreference;
                    preferences = data.preferences;
                    return View(preferences);
                }

                return View();
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });

        }

        [HttpPost]
        public IActionResult SavePreference(int PrefId)
        {
            if (IsLogin() == true)
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
            return RedirectToAction("Login", "Account");
        }

        public Boolean IsLogin()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return false;
            }
            return true;
        }
    }
}
