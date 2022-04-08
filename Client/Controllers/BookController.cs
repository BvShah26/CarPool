using DataAcessLayer.Models.Booking;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class BookController : Controller
    {
        HttpClient httpClient;
        private readonly IConfiguration _config;

        public BookController(IConfiguration config)
        {
            _config = config;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config.GetValue<string>("proxyUrl"));
        }


        [HttpGet]
        public IActionResult Index(int RideId)
        {
            string returnUrl = HttpContext.Request.Path;
            if (IsLogin() == true)
            {
                //Confirmation Page Here
                return View();
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });
        }

        [HttpPost]
        public IActionResult Index(Book book)
        {
            return View();
        }

        public Boolean IsLogin()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return false;
            }
            return true;
        }
    }
}
