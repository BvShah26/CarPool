using System;
using System.Collections.Generic;
using System.Text;
using DataAcessLayer.Models.Booking;
using DataAcessLayer.Models.Users;

namespace DataAcessLayer.Models.Rides
{
    public class PublishRide
    {
        public int Id { get; set; }
        public Users1 Publisher { get; set; }
        public int PublisherId { get; set; }

        public int VehicleId { get; set; }
        public Uservehicle Vehicle { get; set; } //--------------------------

        public DateTime JourneyDate { get; set; }
        public int MaxPassengers { get; set; }
        public string Departure_City { get; set; } //
        public string Destination_City { get; set; } //

        public string PickUp_Location { get; set; }
        public string DropOff_Location { get; set; }


        public DateTime PickUp_Time { get; set; }
        public DateTime DropOff_Time { get; set; }
            

        public Boolean IsInstant_Approval { get; set; }
        public int Price_Seat { get; set; }
        public string Ride_Note { get; set; }
        public DateTime Publishing_Timestamp { get; set; } //DateTime.Now()


        public Boolean IsCompletelyBooked { get; set; }

        public Boolean IsCancelled { get; set; } //Later



   
        //Collections

        public ICollection<Book> Booking { get; set; } = new HashSet<Book>();
        public ICollection<RideApproval> Ride_Approval { get; set; } = new HashSet<RideApproval>(); //used only if isInstantApproval is false



    }
}
