using DataAcessLayer.Models.Rides;
using DataAcessLayer.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.Models.Chat
{
    public class ChatRoom
    {
        public int Id { get; set; }
        public int RiderId { get; set; }
        public ClientUsers Rider { get; set; }

        public int PublisherId { get; set; }
        public ClientUsers Publisher { get; set; }

        public int RideId { get; set; }
        public PublishRide Ride { get; set; }

        //public ICollection<ClientUsers> ClientUsers { get; set; }

        public ICollection<ChatMessages> ChatMessages { get; set; } = new HashSet<ChatMessages>();

    }
}
