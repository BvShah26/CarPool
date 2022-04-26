﻿using DataAcessLayer.Models.Rides;
using DataAcessLayer.ViewModels.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class RidesController : Controller
    {
        HttpClient httpClient;
        private readonly IConfiguration _config;
        public RidesController(IConfiguration config)
        {
            _config = config;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config.GetValue<string>("proxyUrl"));

        }
        [HttpGet]
        public IActionResult Index()
        {
            string returnUrl = HttpContext.Request.Path;
            if (IsLogin() == true)
            {

                int UserId = (int)HttpContext.Session.GetInt32("UserId");
                HttpResponseMessage responseMessage = httpClient.GetAsync($"UserRides/{UserId}").Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    List<UserRideViewModal> rides = JsonConvert.DeserializeObject<List<UserRideViewModal>>(res);

                    ViewBag.UserId = UserId;

                    return View(rides);
                }
                return View();
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            string returnUrl = HttpContext.Request.Path + "/" + id;

            if (IsLogin() == true)
            {

                HttpResponseMessage responseRide = httpClient.GetAsync($"PublishRides/GetRideDetailsUser/{id}").Result;
                if (responseRide.IsSuccessStatusCode)
                {
                    string res = responseRide.Content.ReadAsStringAsync().Result;
                    PublishRide record = JsonConvert.DeserializeObject<PublishRide>(res);
                    // booking of current user

                    //var bookingId = record.Booking.Where()

                    //var bookingId = 


                    return View(record);
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });


        }

        [HttpGet]
        public IActionResult History()
        {
            string returnUrl = HttpContext.Request.Path;

            if (IsLogin() == true)
            {

                int UserId = (int)HttpContext.Session.GetInt32("UserId");
                HttpResponseMessage responseMessage = httpClient.GetAsync($"UserRides/RideHistory/{UserId}").Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string res = responseMessage.Content.ReadAsStringAsync().Result;
                    List<UserRideViewModal> rides = JsonConvert.DeserializeObject<List<UserRideViewModal>>(res);

                    ViewBag.UserId = UserId;

                    return View(rides);
                }
                return View();
            }
            return RedirectToAction("Login", "Account", new { url = returnUrl });

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
