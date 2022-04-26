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


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(ClientUsers clientUsers)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (clientUsers != null)
                    {
                        HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("ClientUser", clientUsers).Result;
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            string res = responseMessage.Content.ReadAsStringAsync().Result;
                            ClientUsers client = JsonConvert.DeserializeObject<ClientUsers>(res);
                            HttpContext.Session.SetString("UserName", client.Name);
                            HttpContext.Session.SetInt32("UserId", client.Id);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Registration Failed");
                }
            }
            return View(clientUsers);

        }

        [HttpGet]
        public IActionResult Login(string? url)
        {
            ViewBag.ReturnURL = url;
            return View();
        }

        [HttpPost]
        public IActionResult Login(ClientLoginView clientLogin)
        {
            if (clientLogin != null)
            {
                HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("ClientUser/Login", clientLogin).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    ClientUsers client = JsonConvert.DeserializeObject<ClientUsers>(res);

                    HttpContext.Session.SetString("UserName", client.Name);
                    HttpContext.Session.SetInt32("UserId", client.Id);
                    if (String.IsNullOrEmpty(clientLogin.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return LocalRedirect(clientLogin.ReturnUrl);
                }
                if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    ViewBag.InvalidCredentials = true;
                }
                return View(clientLogin);

            }
            return View();
        }

        //Login Identity

    }
}
