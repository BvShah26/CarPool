using DataAcessLayer.Models.Ratings;
using DataAcessLayer.ViewModels.Ratings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Controllers.Ratigs
{
    public class RateController : Controller
    {
        HttpClient httpClient;
        private readonly IConfiguration _config;
        public RateController(IConfiguration config)
        {
            _config = config;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config.GetValue<string>("proxyUrl"));

        }
        [HttpGet]
        public IActionResult Publisher(int PublisherId,  int RideId)
        {
            ViewBag.PublisherId = PublisherId;
            ViewBag.RideId = RideId;
            return View();
        }

        [HttpPost]
        public IActionResult Publisher(PublisherRating_ViewModel publisherRating, int RideId)
        {
            int UserId = (int)HttpContext.Session.GetInt32("UserId");

            PublisherRatings rating = new PublisherRatings()
            {
                UserId = UserId,
                PublisherId = publisherRating.PublisherId,
                Rating = Enum.Parse<RatingTitle>(publisherRating.Rating)

            };
            publisherRating.UserId = UserId;

            HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("Rating/RatePublisher", rating).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return Ok(RideId);
            }
            throw new Exception("Rating Not Added");
        }

        [HttpGet]
        public IActionResult Partner(int PartnerId, string Rating, int RideId)
        {
            ViewBag.PartnerId = PartnerId;
            ViewBag.RideId = RideId;
            return View();
        }

        [HttpPost]
        public IActionResult Partner(PartnerRating_ViewModel partnerRating, int RideId)
        {
            int UserId = (int)HttpContext.Session.GetInt32("UserId");

            RidePartnerRating rating = new RidePartnerRating()
            {
                UserId = UserId,
                PartnerId = partnerRating.PartnerId,
                Rate = Enum.Parse<PartnerRatingTitle>(partnerRating.Rating)

            };

            HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync("Rating/RatePartner", rating).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                return Ok(RideId);
            }
            throw new Exception("Rating Not Added");
        }
    }
}
