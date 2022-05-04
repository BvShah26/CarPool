using System;
using System.Collections.Generic;
using System.Text;
using DataAcessLayer.Models.Users;

namespace DataAcessLayer.Models.Rides
{
    public class RideApproval
    {
        public RideApproval()
        {

        }
        public int Id { get; set; }
        
        
        public PublishRide Ride { get; set; }
        public int RideId { get; set; }

        public ClientUsers User { get; set; }
        public int UserId { get; set; }

        public int RequestedSeats { get; set; }

        //public Boolean IsApproved { get; set; }

        //public Boolean IsRejected { get; set; }
        public DateTime? RequestTime { get; set; }
        public RequestStaus? Status { get; set; }
    }

    public enum RequestStaus
    {
        Pending = -1,
        Approved = 1,
        Rejected = 0
    }
}
