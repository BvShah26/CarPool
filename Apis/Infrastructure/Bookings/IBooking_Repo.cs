using DataAcessLayer.Models.Booking;
using DataAcessLayer.ViewModels.Ride;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Infrastructure.Bookings
{
    public interface IBooking_Repo
    {
        Task<List<Book>> GetAllBookings();
        Task<List<Book>> GetUserBookings(int UserId);
        Task<List<RidePartners>> GetRideBookings(int RideId);
        Task<Book> GetBooking(int BookingId);
        Task<Book> AddNewBooking(Book bookingRecord);
        bool isBookingExists(int BookingId);

    }
}
