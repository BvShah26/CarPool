using Apis.Data;
using Apis.Infrastructure.Bookings;
using DataAcessLayer.Models.Rides;
using DataAcessLayer.Models.Users;
using DataAcessLayer.ViewModels;
using DataAcessLayer.ViewModels.Bookings;
using DataAcessLayer.ViewModels.Client;
using DataAcessLayer.ViewModels.Ride;
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
        private readonly IBooking_Repo _bookingRepo;
        
        public SearchRideController(ApplicationDBContext context, IBooking_Repo bookingRepo)
        {
            _context = context;
            _bookingRepo = bookingRepo;
        }
        [HttpPost]
        public async Task<IActionResult> GetRides(SearchRide search)
        {
            //After Checking Booking Data (Working)
            {
                //List<PublishRide> rec = await _context.Publish_Rides
                //    .Where(item => item.JourneyDate == search.JourneyDate
                //              && item.Departure_City == search.Departure_City
                //              && item.Destination_City == search.Destination_City
                //              && item.MaxPassengers >= search.SeatCount
                //              && item.IsCompletelyBooked == false
                //              && item.IsCancelled == false
                //            )
                //    .Include(item => item.Publisher)
                //    .Include(x => x.Booking)
                //    .Where(x => (x.Booking.Count == 0 ||
                //        ((x.Booking.Sum(y => y.SeatQty) < x.MaxPassengers) && (x.MaxPassengers - x.Booking.Sum(y => y.SeatQty)) >= search.SeatCount)))
                //    .ToListAsync();
            }

            //Here Data of RideApproved i.e on search it will count only approved ride as blablacar.com

            //List<PublishRide> rec = await _context.Publish_Rides
            //    .Where(item => item.JourneyDate == search.JourneyDate
            //              && item.Departure_City == search.Departure_City
            //              && item.Destination_City == search.Destination_City
            //              && item.MaxPassengers >= search.SeatCount
            //              && item.IsCompletelyBooked == false
            //              && item.IsCancelled == false
            //            )
            //    .Include(item => item.Publisher)
            //    .Include(x => x.Booking)
            //    .Where(x => (x.Booking.Count == 0 ||
            //        ((x.Booking.Sum(y => y.SeatQty) < x.MaxPassengers) && (x.MaxPassengers - x.Booking.Sum(y => y.SeatQty)) >= search.SeatCount)))
            //    .Include(x => x.Ride_Approval)
            //    .Where(x => x.Ride_Approval.Count == 0 ||
            //    (x.Ride_Approval.Where(rideApproval => rideApproval.IsApproved == true).Sum(y => y.RequestedSeats) >= search.SeatCount)
            //    )
            //    .ToListAsync();

            List<PublishRide> rec = await _context.Publish_Rides
                .Where(item => item.JourneyDate == search.JourneyDate
                          && item.Departure_City == search.Departure_City
                          && item.Destination_City == search.Destination_City
                          && item.MaxPassengers >= search.SeatCount
                          && item.IsCompletelyBooked == false
                          && item.IsCancelled == false
                        )
                .Include(item => item.Publisher)
                .Include(x => x.Booking)
                .Where(x => (x.Booking.Count == 0 ||
                    ((x.Booking.Sum(y => y.SeatQty) < x.MaxPassengers) && (x.MaxPassengers - x.Booking.Sum(y => y.SeatQty)) >= search.SeatCount)))

                .Include(x => x.Ride_Approval)
                .Where(x =>  x.Ride_Approval.Count == 0 ||
                    (x.Ride_Approval.Where(rideApproval => rideApproval.IsRejected == false)
                        .Sum(y =>  y.RequestedSeats) - x.MaxPassengers <= (- search.SeatCount))
                )
                .ToListAsync();


            //var data = rec.Where(x => (x.Booking.Count == 0 ||
            //        ((x.Booking.Sum(y => y.SeatQty) < x.MaxPassengers) && (x.MaxPassengers - x.Booking.Sum(y => y.SeatQty)) >= search.SeatCount))).ToList();

            // 3 < 4 && 4-3 > 1

            return Ok(rec);
        }

        [HttpGet("RideDetails/{id}")]
        public async Task<IActionResult> RideDetails(int id)
        {
            var rideDetails = await _context.Publish_Rides.Where(item => item.Id == id)
                .Include(x => x.Publisher)
                .Include(x => x.Vehicle).ThenInclude(userVehicle => userVehicle.Vehicle).ThenInclude(vehicle => vehicle.VehicleBrand)
                .Include(x => x.Vehicle).ThenInclude(vehicle => vehicle.Color)
                
                .Select(x => new UserRideDetailsViewModel()
                {
                    IsInstant_Approval = x.IsInstant_Approval,
                    Departure_City = x.Departure_City,
                    Destination_City = x.Destination_City,

                    DropOff_Location = x.DropOff_Location,
                    PickUp_Location = x.PickUp_Location,

                    PickUp_Time = x.PickUp_Time,
                    DropOff_Time = x.DropOff_Time,

                    Price_Seat = x.Price_Seat,


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

                }).FirstOrDefaultAsync();

            List<RidePartners> partners = await _bookingRepo.GetRideBookings(id);
            rideDetails.Partners = partners;



            return Ok(rideDetails);
        }

        [HttpGet("GetRateSeats/{id}")]
        public IActionResult GetRideRate(int id)
        {
            //int rate = _context.Publish_Rides.Where(x => x.Id == id).Select(x => x.Price_Seat).First();
            //return Ok(rate);

            //returning seats and price to controller and it will use it to check isCompleBooking
            var data = _context.Publish_Rides.Where(x => x.Id == id).Select(x => new { rate = x.Price_Seat, MaxSeat = x.MaxPassengers }).First();
            return Ok(data);
        }

        [HttpGet("VerifyRide/{RideId}/{SeatQty}/{UserId}")]
        public async Task<IActionResult> VerifyRide(int RideId,int SeatQty,int UserId)
        {
            var ride = await _context.Publish_Rides.Where(item => item.Id == RideId)
                .Include(x => x.Publisher).Select(x => new BookigConfirmationViewModel()
                {
                    Id = x.Id,
                    FromLocation = x.PickUp_Location,
                    ToLocation = x.DropOff_Location,
                    JourneyDate = x.JourneyDate,
                    IsInstantApproval = x.IsInstant_Approval,
                    PickupTime = x.PickUp_Time.ToShortTimeString(),
                    DropTime = x.DropOff_Time.ToShortTimeString(),
                    Price = x.Price_Seat * SeatQty,
                    SeatQty = SeatQty,
                    Publisher = x.Publisher.Name,
                    PublisherProfile = x.Publisher.ProfileImage,
                    PublisherId = x.PublisherId,
                    //status = (x.Ride_Approval.Where(rideRequest => rideRequest.UserId == UserId ).First() != null) ? "You're Rejected" : ""
                    status = (x.Booking.Where(booking => booking.RiderId == UserId ).First() != null)
                    ? "You can't book again" : (x.Ride_Approval.Where(rideRequest => rideRequest.UserId == UserId ).First() != null) ? "You can't request again" : ""
                    

                }).FirstOrDefaultAsync();
            return Ok(ride);
        }

    }
}
