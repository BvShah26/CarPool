using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.ViewModels.Ride
{
    public class RideOfferViewModel
    {
        public int RideId { get; set; }
        public DateTime JourneyDate { get; set; }
        public string Departure_City { get; set; } //
        public string Destination_City { get; set; } //

        public string PickUp_Location { get; set; }
        public string DropOff_Location { get; set; }


        public DateTime PickUp_Time { get; set; }
        public DateTime DropOff_Time { get; set; }

        public ICollection<RidePartners> Partners { get; set; }
        public ICollection<RideReqests_ViewModel> NewRequests { get; set; }

        
    }
}
