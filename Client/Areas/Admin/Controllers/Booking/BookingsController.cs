using DataAcessLayer.Models.Booking;
using DataAcessLayer.Models.Rides;
using DataAcessLayer.ViewModels.Ride;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Areas.Admin.Controllers.Booking
{
    [Area("Admin")]
    public class BookingsController : Controller
    {
        HttpClient httpClient;
        private readonly IConfiguration _config;

        public BookingsController(IConfiguration config)
        {
            _config = config;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config.GetValue<string>("proxyUrl"));
        }
        public IActionResult Index()
        {
            HttpResponseMessage responseMessage = httpClient.GetAsync("Books").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string res = responseMessage.Content.ReadAsStringAsync().Result;
                List<Book> bookings = JsonConvert.DeserializeObject<List<Book>>(res);
                return View(bookings); ;
            }
            return NotFound();
        }


        // Ride Partner View Model
        // Publish Ride Index

        [HttpGet]
        public List<RidePartners> GetBookingById(int Id)
        {
            HttpResponseMessage responseMessage = httpClient.GetAsync($"Books/GetBookByRide/{Id}").Result;
            List<RidePartners> booking = null;
            if (responseMessage.IsSuccessStatusCode)
            {
                string res = responseMessage.Content.ReadAsStringAsync().Result;
                booking = JsonConvert.DeserializeObject<List<RidePartners>>(res);
            }
            return booking;
        }
    }
}
