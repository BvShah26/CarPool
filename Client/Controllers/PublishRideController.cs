using DataAcessLayer.Models.Rides;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize]
    public class PublishRideController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Publish(PublishRide ride)
        {
            return View();
        }


        public IActionResult List()
        {
            return View();


        }
    }
}
