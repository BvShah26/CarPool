using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.ViewModels.Chat
{
    public class ChatInboxesViewModel
    {
        public int RoomId { get; set; }
        public string UserName { get; set; }
        public string UserProfile { get; set; }
        public string DepartureCity { get; set; }
        public string DestinationCity { get; set; }
        public int RideId { get; set; }
    }
}
