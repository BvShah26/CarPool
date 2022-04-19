using Apis.Data;
using Apis.Infrastructure.Bookings;
using DataAcessLayer.Models.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Repos.Bookings
{
    public class BookingCancellation_Repo : IBookingCancellation_Repo
    {
        private ApplicationDBContext _context;
        //add PublishRide repo

        public BookingCancellation_Repo(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task CancelBooking(int ReasonId,  Book booking)
        {
            //call rideRepo for updating cancellation Status

            booking.IsCancelled = true;
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();


            BookingCancellation cancellation = new BookingCancellation()
            {
                ReasonId = ReasonId,
                BookingId = booking.Id
            };

            _context.bookingCancellations.Add(cancellation);
            await _context.SaveChangesAsync();
        }

    }
}

