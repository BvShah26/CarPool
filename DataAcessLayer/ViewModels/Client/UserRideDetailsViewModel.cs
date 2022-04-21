using DataAcessLayer.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.ViewModels.Client
{
    public class UserRideDetailsViewModel
    {
        public int Id { get; set; }
        public int PublisherId { get; set; }
        public int PublisherName { get; set; }
        public string PublisherProfile { get; set; }

        public int VehicleId { get; set; }
        public string VehicleName { get; set; }
        public string VehicleImage { get; set; }

        //Ride
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

        public ICollection<ClientUsers> Partners { get; set; }
    }
}
