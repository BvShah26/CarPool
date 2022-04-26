using DataAcessLayer.Models.Rides;
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
    public class RideRequestController : Controller
    {
        HttpClient httpClient;
        private readonly IConfiguration _config;
        public RideRequestController(IConfiguration config)
        {
            _config = config;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config.GetValue<string>("proxyUrl"));
        }

        [HttpGet]
        public IActionResult Index(int RequestId, Boolean status, int? RideId, int? Seat,int? UserId)
        {
            if (IsLogin() == true)
            {
                HttpResponseMessage responseRideStatus = httpClient.PutAsJsonAsync($"RideApprovals/UpdateStatus/{RequestId}", new { IsApproved = status }).Result;
                if (responseRideStatus.IsSuccessStatusCode)
                {
                    if (status == true)
                    {
                        return RedirectToAction("Confirmed", "Book", new { Id = RideId, SeatQty = Seat, UserId = UserId });
                    }
                }
                return RedirectToAction("Index","Rides");
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
