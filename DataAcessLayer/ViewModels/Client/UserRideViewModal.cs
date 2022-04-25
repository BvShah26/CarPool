using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.ViewModels.Client
{
    public class UserRideViewModal
    {
        public int Id { get; set; }
        public int RideId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public string Publisher { get; set; }
        public string PublisherProfile { get; set; }
        public int PublisherId { get; set; }
        public string Status { get; set; }
        public string HasNewRequest { get; set; }
        public string IsRequestPending { get; set; }
    }
}
