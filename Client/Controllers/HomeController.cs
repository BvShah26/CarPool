using DataAcessLayer.Models.Rides;
using DataAcessLayer.ViewModels;
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
    public class HomeController : Controller
    {
        HttpClient httpClient;
        private readonly IConfiguration _config;


        public HomeController(IConfiguration config)
        {
            _config = config;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config.GetValue<string>("proxyUrl"));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(SearchRide searchRide)
        {
            HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("SearchRide", searchRide).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string res = responseMessage.Content.ReadAsStringAsync().Result;
                List<PublishRide> rides = JsonConvert.DeserializeObject<List<PublishRide>>(res);
                //return Ok(rides);
                //return RedirectToAction("Rides");
                return View("Rides", rides);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Rides(string SearchRide)
        {
            List<PublishRide> rides = JsonConvert.DeserializeObject<List<PublishRide>>(SearchRide);
            return View();
            //return View(rides);
        }


    }
}
