using DataAcessLayer.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class AccountController : Controller
    {
        HttpClient httpClient;
        private readonly IConfiguration _config;

        public AccountController(IConfiguration config)
        {
            _config = config;
            httpClient = new HttpClient();
            //httpClient.BaseAddress = new Uri(_config.GetValue<string>("proxyUrl"));
            httpClient.BaseAddress = new Uri("https://localhost:44372/api/");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(ClientUsers clientUsers)
        {
             HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("ClientUser",clientUsers).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(ClientUsers clientUsers)
        {
            return RedirectToAction();
        }
    }
}
