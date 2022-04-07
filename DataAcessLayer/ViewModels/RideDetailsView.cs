using DataAcessLayer.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.ViewModels
{
    public class RideDetailsView
    {
        public int Id { get; set; }
        public ClientUsers Publisher { get; set; }
        public int PublisherId { get; set; }

        public string PickUp_LatLong { get; set; }
        public string DropOff_LatLong { get; set; }

        public DateTime JourneyDate { get; set; }

        public DateTime PickUp_Time { get; set; }
        public DateTime DropOff_Time { get; set; }


        public Boolean IsInstant_Approval { get; set; }
        public int Price_Seat { get; set; }

        public string Client_Departure_LatLong { get; set; }
        public string Client_Destination_LatLong { get; set; }

        public double Pickup_DifferDistance { get; set; }
        public double Drop_DifferDistance { get; set; }


    }
}
