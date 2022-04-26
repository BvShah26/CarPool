using DataAcessLayer.Models.Users;
using DataAcessLayer.ViewModels.Ride;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.ViewModels.Client
{
    public class UserRideDetailsViewModel
    {
        public int Id { get; set; }
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public string PublisherProfile { get; set; }

        public string VehicleName { get; set; }
        public string VehicleImage { get; set; }
        public string VehicleColor { get; set; }

        //Ride
        public DateTime JourneyDate { get; set; }
        public int RideId { get; set; }
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

        public ICollection<RidePartners> Partners { get; set; }

        //For Booking
        public bool HasRatedPubllisher { get; set; }
        public int Seat { get; set; }
        public bool AllowCancel { get; set; }
        public string IsRequestPending { get; set; }
        public string Status { get; set; }
        public int BookingId { get; set; }

    }
}
