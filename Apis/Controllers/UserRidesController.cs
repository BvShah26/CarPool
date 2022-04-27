using Apis.Data;
using Apis.Infrastructure.Bookings;
using Apis.Infrastructure.Ratings;
using DataAcessLayer.ViewModels.Client;
using DataAcessLayer.ViewModels.Ride;
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
        private readonly IRatings_Repo _ratingRepo;
        private readonly IBooking_Repo _bookingRepo;

        public UserRidesController(ApplicationDBContext context, IRatings_Repo ratingRepo,IBooking_Repo booking_Repo)
        {
            _context = context;
            _ratingRepo = ratingRepo;
            _bookingRepo = booking_Repo;
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

                .Select(x => new UserRideViewModal
                {
                    RideId = x.Id,
                    Date = x.JourneyDate,
                    Time = x.PickUp_Time,
                    Departure = x.Departure_City,
                    Destination = x.Destination_City,
                    Publisher = x.Publisher.Name,
                    PublisherProfile = x.Publisher.ProfileImage,
                    PublisherId = x.PublisherId,
                    Status = (x.Ride_Approval.Where(rideRequest => rideRequest.UserId == UserId && rideRequest.IsRejected == true).First() != null)
                   ? "You're Rejected" :
                   (x.Booking.Where(booking => booking.Publish_RideId == x.Id && booking.RiderId == UserId && booking.bookingCancellation.BookingId == booking.Id).First() != null ? "Cancelled" : ""),
                    //check booking cancellation

                    //only for publisher
                    HasNewRequest = (x.JourneyDate.Date >= DateTime.Now.Date ? (x.IsInstant_Approval == false && x.Ride_Approval.Any(rideRequest => rideRequest.IsRejected == false && rideRequest.IsApproved == false) == true ? "New booking request" : "") : ""),
                    IsRequestPending = (x.JourneyDate.Date >= DateTime.Now.Date ? (x.IsInstant_Approval == false && x.Ride_Approval.Any(rideRequest => rideRequest.IsRejected == false && rideRequest.IsApproved == false && rideRequest.UserId == UserId) == true ? "Awaiting approval" : "") : "" ),
                    HasRated = (x.JourneyDate.Date > DateTime.Now.Date ?
                   true : ((x.PublisherId == UserId ? _ratingRepo.HasRated_AllPartner(x.Id, UserId) : _ratingRepo.HasRatedPublisher(x.Id, UserId))))

                })
                .OrderByDescending(x => x.Date)
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
                    Status = (x.Ride_Approval.Where(rideRequest => rideRequest.UserId == UserId && rideRequest.IsRejected == true).First() != null)
                    ? "You're Rejected" :
                    (x.Booking.Where(booking => booking.Publish_RideId == x.Id && booking.RiderId == UserId && booking.bookingCancellation.BookingId == booking.Id).First() != null ? "Cancelled" : ""),
                })
                .ToListAsync();
            return Ok(rides);
        }

        [HttpGet("BookingDetails/{RideId}/{UserId}")]
        public async Task<IActionResult> BookingDetails(int RideId,int UserId)
        {
            var rideDetails = await _context.Publish_Rides.Where(item => item.Id == RideId)
                .Include(x => x.Publisher)
                .Include(x => x.Ride_Approval)
                .Include(x => x.Vehicle).ThenInclude(userVehicle => userVehicle.Vehicle).ThenInclude(vehicle => vehicle.VehicleBrand)
                .Include(x => x.Vehicle).ThenInclude(vehicle => vehicle.Color)

                .Select(x => new UserRideDetailsViewModel()
                {
                    Departure_City = x.Departure_City,
                    Destination_City = x.Destination_City,

                    DropOff_Location = x.DropOff_Location,
                    PickUp_Location = x.PickUp_Location,

                    PickUp_Time = x.PickUp_Time,
                    DropOff_Time = x.DropOff_Time,

                    Seat = (x.IsInstant_Approval == true) ? x.Booking.Where(booking => booking.Publish_RideId == x.Id && booking.RiderId == UserId).Select(booking => booking.SeatQty).FirstOrDefault() :
                    x.Ride_Approval.Where(rideRequest => rideRequest.RideId == x.Id && rideRequest.UserId == UserId).Select(request => request.RequestedSeats).FirstOrDefault(),
                    Price_Seat = x.Price_Seat,
                    BookingId = x.Booking.Where(booking => booking.Publish_RideId == x.Id && booking.RiderId == UserId).Select(booking => booking.Id).FirstOrDefault(),


                    PublisherName = x.Publisher.Name,
                    PublisherId = x.PublisherId,
                    PublisherProfile = x.Publisher.ProfileImage,

                    RideId = x.Id,
                    Ride_Note = x.Ride_Note,

                    MaxPassengers = x.MaxPassengers,

                    VehicleImage = x.Vehicle.VehicleImage,
                    VehicleName = x.Vehicle.Vehicle.VehicleBrand.Name + " " + x.Vehicle.Vehicle.Name,
                    VehicleColor = x.Vehicle.Color.Color,

                    JourneyDate = x.JourneyDate,
                    HasRatedPubllisher = (x.JourneyDate.Date > DateTime.Now.Date ? true : _ratingRepo.HasRatedPublisher(x.PublisherId, UserId)),
                    
                    
                    //Allow Cancel
                    AllowCancel = (x.JourneyDate.Date > DateTime.Now.Date) ?
                    (x.Booking.Where(booking => booking.Publish_RideId == x.Id && booking.RiderId == UserId && booking.IsCancelled == false).FirstOrDefault() != null ? true : false)
                     : false ,

                    IsRequestPending = (x.JourneyDate.Date >= DateTime.Now.Date ? (x.IsInstant_Approval == false && x.Ride_Approval.Any(rideRequest => rideRequest.IsRejected == false && rideRequest.IsApproved == false && rideRequest.UserId == UserId) == true ? "Awaiting approval" : "") : ""),
                    Status = (x.Ride_Approval.Where(rideRequest => rideRequest.UserId == UserId && rideRequest.IsRejected == true).First() != null)
                   ? "You're Rejected" :
                   (x.Booking.Where(booking => booking.Publish_RideId == x.Id && booking.RiderId == UserId && booking.bookingCancellation.BookingId == booking.Id).First() != null ? "Cancelled" : ""),


                }).FirstOrDefaultAsync();



            List<RidePartners> partners = await _bookingRepo.GetRideBookings(RideId);
            rideDetails.Partners = partners;


            return Ok(rideDetails);
        }

    }
}
