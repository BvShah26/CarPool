using System;
using System.Collections.Generic;
using System.Text;
using DataAcessLayer.Models.Rides;
using DataAcessLayer.Models.Users;

namespace DataAcessLayer.Models.Booking
{
    public class Book
    {
        public int Id { get; set; }
        
        
        public PublishRide Ride{ get; set; }
        public int RideId { get; set; }

        public User User{ get; set; }
        public int UserId { get; set; }

        public int SeatQty { get; set; }
        public int TotalPrice { get; set; }

        public Boolean IsCancelled { get; set; }
    }
}
