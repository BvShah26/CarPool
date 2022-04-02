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
        
        
        public PublishRide Publish_Ride{ get; set; }
        public int Publish_RideId { get; set; }

        public Users1 Rider{ get; set; }
        public int RiderId { get; set; }

        public int SeatQty { get; set; }
        public int TotalPrice { get; set; }

        public Boolean IsCancelled { get; set; }
    }
}
