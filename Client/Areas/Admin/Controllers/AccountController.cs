using DataAcessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Areas.Admin.Controllers
{
    [Area("Admin")]
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

        [HttpPost]
        public IActionResult Login(AdminModel admin)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("Admin/Login", admin).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    AdminModel client = JsonConvert.DeserializeObject<AdminModel>(res);


                    HttpContext.Session.SetInt32("AdminId", client.Id);
                    return RedirectToAction("Index", "Vehicle");


                }
                if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    ViewBag.InvalidCredentials = true;
                }
                return View("Login");

            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
    }
}
