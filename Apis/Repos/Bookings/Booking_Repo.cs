using Apis.Data;
using Apis.Infrastructure.Bookings;
using DataAcessLayer.Models.Booking;
using DataAcessLayer.ViewModels.Ride;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Apis.Repos.Bookings
{
    public class Booking_Repo : IBooking_Repo
    {
        private readonly ApplicationDBContext _context;
        private readonly IConfiguration _config;


        public Booking_Repo(ApplicationDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<Book> AddNewBooking(Book bookingRecord)
        {
            int MaxPassenger = bookingRecord.Publish_Ride.MaxPassengers;

            bookingRecord.Publish_Ride = null;
            //As it overrides PublishRideId to 0


            //New Booking Added
            var result = await _context.Bookings.AddAsync(bookingRecord);
            await _context.SaveChangesAsync();
            //SaveChanges here so able to get correct Total_Occuiped


            //Checking For IsCompletlyBooked Status
            int TotalSeat_Occuiped = _context.Bookings.Where(x => x.Publish_RideId == bookingRecord.Publish_RideId).Sum(x => x.SeatQty);

            if (TotalSeat_Occuiped == MaxPassenger)
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(_config.GetValue<string>("proxyUrl"));

                HttpResponseMessage responseMessage = httpClient.GetAsync($"PublishRides/ChangeBookStatus/{bookingRecord.Publish_RideId}").Result;

                if (!responseMessage.IsSuccessStatusCode)
                {
                    throw new Exception("Fail : change Booking status ");
                }
            }
            return result.Entity;
        }

        public async Task<List<Book>> GetAllBookings()
        {
            return await _context.Bookings.Include(x => x.Publish_Ride.Publisher).Include(y => y.Rider).ToListAsync();

            //Just To Make Easy ( JourneyDate >= DateTime.Now )
            //return await _context.Bookings
            //    .Include(x => x.Publish_Ride).ThenInclude(publishRide => publishRide.Publisher)
            //    .Include(y => y.Rider).ToListAsync();
        }

        public async Task<Book> GetBooking(int BookingId)
        {
            var bookingRecord = await _context.Bookings.FindAsync(BookingId);
            return bookingRecord;
        }

        public async Task<List<RidePartners>> GetRideBookings(int RideId)
        {
            var bookingRecord = await _context.Bookings.Where(x => x.Publish_RideId == RideId)
                .Where(x => x.IsCancelled == false)
                .Include(x => x.Rider)
                .Select(x => new RidePartners()
                { 
                    Id = x.RiderId,
                    RiderName =x.Rider.Name,
                    RiderProfile = x.Rider.ProfileImage,
                    SeatQty = x.SeatQty
                    
                })
                .ToListAsync() ;
            return bookingRecord;
        }

        public async Task<List<Book>> GetUserBookings(int UserId)
        {
            var bookingRecord = await _context.Bookings.Where(x => x.RiderId == UserId).Include(x => x.Publish_Ride).ToListAsync();
            return bookingRecord;
        }

        public bool isBookingExists(int BookingId)
        {
            return _context.Bookings.Any(e => e.Id == BookingId);
        }
    }
}
