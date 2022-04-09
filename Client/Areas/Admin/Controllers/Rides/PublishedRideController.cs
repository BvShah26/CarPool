using DataAcessLayer.Models.Rides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Areas.Admin.Controllers.Rides
{
    [Area("Admin")]
    public class PublishedRideController : Controller
    {
        HttpClient httpClient;
        private readonly IConfiguration _config;

        public PublishedRideController(IConfiguration config)
        {
            _config = config;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config.GetValue<string>("proxyUrl"));

        }
        public IActionResult Index()
        {
             HttpResponseMessage responseMessage = httpClient.GetAsync("PublishRides").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string res = responseMessage.Content.ReadAsStringAsync().Result;
                List<PublishRide> publishedRides = JsonConvert.DeserializeObject<List<PublishRide>>(res);
                return View(publishedRides); ;
            }
            return NotFound();
        }
    }
}
