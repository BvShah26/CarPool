using Apis.Data;
using Apis.Infrastructure.Chat;
using DataAcessLayer.Models.Chat;
using DataAcessLayer.ViewModels.Chat;
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

        public async Task<List<ChatInboxesViewModel>> GetUser_ChatRooms(int UserId)
        {
            var rooms = await _context.ChatRoom.Where(x => x.PublisherId == UserId
            || x.RiderId == UserId
            )
                .Include(x => x.Publisher)
                .Include(y => y.Ride)
                .Include(x => x.Rider)
                .Select(x => new ChatInboxesViewModel()
                {
                    RoomId = x.Id,
                    DepartureCity = x.Ride.Departure_City,
                    DestinationCity = x.Ride.Destination_City,
                    RideId = x.RideId,
                    UserName = x.PublisherId == UserId ? x.Rider.Name : x.Publisher.Name,
                    UserProfile = x.PublisherId == UserId ? x.Rider.ProfileImage : x.Publisher.ProfileImage
                })
                .ToListAsync();

            return rooms;
        }
    }
}
