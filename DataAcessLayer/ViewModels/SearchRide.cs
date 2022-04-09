using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.ViewModels
{
    public class SearchRide
    {
        public int Id { get; set; }
            
        public string PickUp_LatLong { get; set; }
        public string DropOff_LatLong { get; set; }
        public string Departure_City { get; set; }
        public string Destination_City { get; set; }

        public DateTime JourneyDate { get; set; }
        public int SeatCount { get; set; }
    }
}
