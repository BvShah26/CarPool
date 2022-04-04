using System;
using System.Collections.Generic;
using System.Text;
using DataAcessLayer.Models.Users;

namespace DataAcessLayer.Models.Rides
{
    public class RideApproval
    {
        public int Id { get; set; }
        
        
        public PublishRide Ride { get; set; }
        public int RideId { get; set; }

        public ClientUsers User { get; set; }
        public int UserId { get; set; }

        public Boolean IsApproved { get; set; }



    }
}
