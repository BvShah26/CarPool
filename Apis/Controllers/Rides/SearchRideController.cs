using Apis.Data;
using DataAcessLayer.Models.Rides;
using DataAcessLayer.Models.Users;
using DataAcessLayer.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Controllers.Rides
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchRideController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public SearchRideController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> GetRides(SearchRide search)
        {
            List<PublishRide> rec = await _context.Publish_Rides.Where(item => item.JourneyDate == search.JourneyDate
                          && item.Departure_City == search.Departure_City
                          && item.Destination_City == search.Destination_City
                          && item.MaxPassengers >= search.SeatCount
                          && item.IsCompletelyBooked == false
                          && item.IsCancelled == false
                        ).Include(item => item.Publisher)
                        .Include(x => x.Booking).
                        Where(x => (x.Booking.Count == 0 || x.Booking.Sum(y => y.SeatQty) >= search.SeatCount)).ToListAsync();

            //Booking >= Search
            //2 >= 3
            // So Condition Fail

            return Ok(rec);
        }

        [HttpGet("RideDetails/{id}")]
        public async Task<PublishRide> RideDetails(int id)
        {
            PublishRide ride = await _context.Publish_Rides.Where(item => item.Id == id)
                .Include(x => x.Publisher).Include(x => x.Vehicle).ThenInclude(x => x.Vehicle)
                .FirstOrDefaultAsync();
            return ride;
        }

        [HttpGet("GetRateSeats/{id}")]
        public IActionResult GetRideRate(int id)
        {
            //int rate = _context.Publish_Rides.Where(x => x.Id == id).Select(x => x.Price_Seat).First();
            //return Ok(rate);

            //returning seats and price to controller and it will use it to check isCompleBooking
            var data = _context.Publish_Rides.Where(x => x.Id == id).Select(x => new { rate = x.Price_Seat,MaxSeat = x.MaxPassengers }).First();
            return Ok(data);
        }

        [HttpGet("VerifyRide/{RideId}")]
        public async Task<PublishRide> VerifyRide(int RideId)
        {
            PublishRide ride = await _context.Publish_Rides.Where(item => item.Id == RideId)
                .Include(x => x.Publisher).FirstOrDefaultAsync();
            return ride;
        }

    }
}
