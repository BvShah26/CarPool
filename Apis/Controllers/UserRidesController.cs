using Apis.Data;
using DataAcessLayer.ViewModels.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRidesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public UserRidesController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet("{UserId}")]
        public async Task<ActionResult> GetRides(int UserId)
        {
            var rides = await _context.Publish_Rides.Include(x => x.Booking).Include(x => x.Ride_Approval)
                .Where(x => x.PublisherId == UserId ||
                    x.Booking.Any(booking => booking.RiderId == UserId) ||
                    x.Ride_Approval.Any(rideRequest => rideRequest.UserId == UserId)
                    )
                .Where(x => x.JourneyDate >= DateTime.Now
                || DateTime.Now.Day - x.JourneyDate.Day <= 7) //Rides of last 7 days

                //View Modal Herw
                .Select(x => new UserRideViewModal
                {
                    RideId = x.Id,
                    Date = x.JourneyDate,
                    Time = x.PickUp_Time,
                    Departure = x.Departure_City,
                    Destination = x.Destination_City,
                    Publisher = x.Publisher.Name, // Check for session at client
                    PublisherProfile = x.Publisher.ProfileImage,
                    PublisherId = x.PublisherId,
                    Status = (x.Ride_Approval.Where(rideRequest => rideRequest.UserId == UserId && rideRequest.IsRejected == true).First() != null) ? "You're Rejected" : ""
                })
                .ToListAsync();
            return Ok(rides);
        }



        [HttpGet("RideHistory/{UserId}")]
        public async Task<ActionResult> RideHistory(int UserId)
        {
            //Might Needed Validation for JourneyDate Time i.e 00:00

            var rides = await _context.Publish_Rides.Include(x => x.Booking).Include(x => x.Ride_Approval)
                .Where(x => x.PublisherId == UserId ||
                    x.Booking.Any(booking => booking.RiderId == UserId) ||
                    x.Ride_Approval.Any(rideRequest => rideRequest.UserId == UserId)
                    )
                .Where(x => (DateTime.Now.Day - x.JourneyDate.Day) > 7) //Rides excpet last 7 days //
                .Select(x => new UserRideViewModal
                {
                    RideId = x.Id,
                    Date = x.JourneyDate,
                    Time = x.PickUp_Time,
                    Departure = x.Departure_City,
                    Destination = x.Destination_City,
                    Publisher = x.Publisher.Name, // Check for session at client
                    PublisherProfile = x.Publisher.ProfileImage,
                    PublisherId = x.PublisherId,
                    Status = (x.Ride_Approval.Where(rideRequest => rideRequest.UserId == UserId && rideRequest.IsRejected == true).First() != null) ? "You're Rejected" : ""
                })
                .ToListAsync();
            return Ok(rides);
        }

    }
}
