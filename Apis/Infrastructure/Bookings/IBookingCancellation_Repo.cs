using DataAcessLayer.Models.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Infrastructure.Bookings
{
    public interface IBookingCancellation_Repo
    {
        Task CancelBooking(int ReasonId, Book booking);

        Task<List<CancellationReason>> GetCancellationReason();
    }
}
