using DataAcessLayer.Models.Users;
using DataAcessLayer.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
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
            httpClient.BaseAddress = new Uri(_config.GetValue<string>("proxyUrl"));
        }

        public IActionResult Index() //Delete this and view
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
        public IActionResult Login(string url)
        {
            ViewBag.ReturnURL = url;
            return View();
        }

        [HttpPost]
        public IActionResult Login(ClientLoginView clientLogin)
        {
            HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("ClientUser/Login",clientLogin).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string res = responseMessage.Content.ReadAsStringAsync().Result;
                ClientUsers client = JsonConvert.DeserializeObject<ClientUsers>(res);

                HttpContext.Session.SetString("UserName", client.Name);
                HttpContext.Session.SetInt32("UserId", client.Id);
                return LocalRedirect(clientLogin.ReturnUrl);
                //return RedirectToAction("Index");
            }
            return View(clientLogin);
        }

        //Login Identity

    }
}
