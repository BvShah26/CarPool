using DataAcessLayer.ViewModels.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Controllers.ClientController
{
    public class UserController : Controller
    {
        HttpClient httpClient;
        private readonly IConfiguration _config;
        public UserController(IConfiguration config)
        {
            _config = config;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config.GetValue<string>("proxyUrl"));
        }

        [HttpGet]
        public IActionResult Profile(int id)
        {
            HttpResponseMessage responseProfile = httpClient.GetAsync($"ClientUser/PublicProfile/{id}").Result;
            if (responseProfile.IsSuccessStatusCode)
            {
                string res = responseProfile.Content.ReadAsStringAsync().Result;
                ClientPublicProfile userProfile = JsonConvert.DeserializeObject<ClientPublicProfile>(res);
                return View(userProfile);
            }
            return View();
        }
    }
}
