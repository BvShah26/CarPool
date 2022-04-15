﻿using DataAcessLayer.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAcessLayer.Models.Chat
{
    public class ChatMessages
    {
        public int Id { get; set; }

        public ChatRoom Room { get; set; }
        public int RoomId { get; set; }

        public string Message { get; set; }


        public int SenderId { get; set; }
        public ClientUsers Sender { get; set; }
    }
}