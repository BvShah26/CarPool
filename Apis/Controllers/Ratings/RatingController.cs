using Apis.Infrastructure.Ratings;
using DataAcessLayer.Models.Ratings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Controllers.Ratings
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatings_Repo _ratingRepo;
        public RatingController(IRatings_Repo ratingRepo)
        {
            _ratingRepo = ratingRepo;
        }

        [HttpGet("Publisher/{PublisherId}")]
        public async Task<IActionResult> PublisherRating(int PublisherId)
        {
            double avgRatings = _ratingRepo.GetPublisherRating(PublisherId);
            return Ok(avgRatings);
        }

        [HttpGet("Partner/{PartnerId}")]
        public async Task<IActionResult> Partner(int PartnerId)
        {
            double avgRatings = _ratingRepo.GetPatrtnerRatings(PartnerId);
            return Ok(avgRatings);
        }

        [HttpPost("RatePartner")]
        public async Task<IActionResult> RatePartner(RidePartnerRating rating)
        {
            await _ratingRepo.AddPartnerRating(rating);
            return Ok();
        }

        [HttpPost("RatePublisher")]
        public async Task<IActionResult> RatePublisher(PublisherRatings rating)
        {
            await _ratingRepo.AddPublisherRating(rating);
            return Ok();
        }


        //api for has already rated or not 
        //add Rating in Public Profile ViewModel and display in ClientSide View

    }
}
