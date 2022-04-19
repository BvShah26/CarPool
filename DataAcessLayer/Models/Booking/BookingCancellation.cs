using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.Models.Booking
{
    public class BookingCancellation
    {
        public int Id { get; set; }


        public CancellationReason Reason { get; set; }
        public int ReasonId { get; set; }


        public Book Booking { get; set; }
        public int BookingId { get; set; }
    }
}
