using DataAcessLayer.Models.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Infrastructure.Chat
{
    public interface IChatMessages_Repo
    {
        Task<List<ChatMessages>> GetRoomMessages(int RoomId);
        Task<ChatMessages> NewMessage(ChatMessages message);
    }
}
