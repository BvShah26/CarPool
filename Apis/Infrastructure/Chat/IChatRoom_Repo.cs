using DataAcessLayer.Models.Chat;
using DataAcessLayer.ViewModels.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Infrastructure.Chat
{
    public interface IChatRoom_Repo
    {
        Task<ChatRoom> CreateRoom(ChatRoom chatRoom);
        Task<List<ChatInboxesViewModel>> GetUser_ChatRooms(int UserId);

        Task<int> GetRoomIdByRiders(int RideId, int PublisherId, int UserId);
    }
}
