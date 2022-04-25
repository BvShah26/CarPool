using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.ViewModels.Bookings
{
    public class BookigConfirmationViewModel
    {
        public int Id { get; set; }
        public DateTime JourneyDate { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string PickupTime { get; set; }
        public string DropTime { get; set; }
        public string Publisher { get; set; }
        public string  PublisherProfile { get; set; }
        public int Price { get; set; }
        public int SeatQty { get; set; }
        public bool IsInstantApproval { get; set; }
        public string status { get; set; }
    }
}
