using Apis.Data;
using Apis.Infrastructure.Chat;
using DataAcessLayer.Models.Chat;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apis.Repos.Chat
{
    public class ChatRoom_Repo : IChatRoom_Repo
    {
        private ApplicationDBContext _context;
        public ChatRoom_Repo(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<ChatRoom> CreateRoom(ChatRoom chatRoom)
        {
            var res = await _context.ChatRoom.AddAsync(chatRoom);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<int> GetRoomIdByRiders(int RideId, int PublisherId, int UserId)
        {
            int RoomId = await _context.ChatRoom.Where(x => x.RideId == RideId
           && x.PublisherId == PublisherId
           && x.RiderId == UserId
           ).Select(x => x.Id).FirstOrDefaultAsync();

            //if (RoomId != 0)
            //{
            //    var Messages = await _context.ChatMessages.Where(x => x.RoomId == RoomId).ToListAsync();
                
            //    return (new { Messages = Messages, RoomId = RoomId });
            //}
            return RoomId;
        }

        public async Task<List<ChatRoom>> GetUser_ChatRooms(int UserId)
        {
            List<ChatRoom> rooms = await _context.ChatRoom.Where(x => x.PublisherId == UserId
            || x.RiderId == UserId
            )
                .Include(x => x.Publisher)
                .Include(y => y.Ride)
                .Include(x => x.Rider)
                .ToListAsync();

            return rooms;
        }
    }
}
